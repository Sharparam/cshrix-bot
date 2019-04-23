// <copyright file="build.cake">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

#addin nuget:?package=Cake.Docker&version=0.9.9
#tool "nuget:?package=GitVersion.CommandLine&version=4.0.0"

var isAppVeyor = AppVeyor.IsRunningOnAppVeyor;
var isTravis = TravisCI.IsRunningOnTravisCI;
var isCi = isAppVeyor || isTravis;

var target = Argument("target", "Default");

var configuration = HasArgument("Configuration")
    ? Argument<string>("Configuration")
    : EnvironmentVariable("CONFIGURATION") != null
        ? EnvironmentVariable("CONFIGURATION")
        : "Release";

var buildNumber = HasArgument("BuildNumber")
    ? Argument<int>("BuildNumber")
    : isAppVeyor
        ? AppVeyor.Environment.Build.Number
        : isTravis
            ? TravisCI.Environment.Build.BuildNumber
            : EnvironmentVariable("BUILD_NUMBER") != null
                ? int.Parse(EnvironmentVariable("BUILD_NUMBER"))
                : 0;

var testFailed = false;
var solutionDir = System.IO.Directory.GetCurrentDirectory();

var testResultDir = Argument("testResultDir", System.IO.Path.Combine(solutionDir, "test-results"));
var artifactDir = Argument("artifactDir", "./artifacts");
var dockerRegistry = Argument("dockerRegistry", "local");
var slnName = Argument("slnName", "Cshrix.Bot");

var solutionFile = System.IO.Path.Combine(solutionDir, $"{slnName}.sln");

GitVersion version = null;

GitVersion GetGitVersion(bool buildServerOutput = false)
{
    var settings = new GitVersionSettings
    {
        RepositoryPath = ".",
        OutputType = buildServerOutput ? GitVersionOutput.BuildServer : GitVersionOutput.Json
    };

    return GitVersion(settings);
}

DotNetCoreMSBuildSettings GetMSBuildSettings(GitVersion version)
{
    return new DotNetCoreMSBuildSettings()
        .SetVersion(version.NuGetVersion)
        .SetFileVersion(version.AssemblySemFileVer)
        .SetInformationalVersion(version.InformationalVersion)
        .WithProperty("AssemblyVersion", version.AssemblySemVer);
}

DotNetCoreBuildSettings GetBuildSettings()
{
    return new DotNetCoreBuildSettings
    {
        Configuration = configuration,
        MSBuildSettings = GetMSBuildSettings(version)
    };
}

Setup(ctx =>
{
    Information("PATH is {0}", EnvironmentVariable("PATH"));

    if (isCi)
    {
        GetGitVersion(true);
    }

    version = GetGitVersion();

    Information("Version: {0} on {1}", version.InformationalVersion, version.CommitDate);
});

Task("Clean")
    .Does(() =>
    {
        var settings = new DeleteDirectorySettings {
            Recursive = true,
            Force = true
        };

        if (DirectoryExists(testResultDir))
        {
            CleanDirectory(testResultDir);
            DeleteDirectory(testResultDir, settings);
        }

        if (DirectoryExists(artifactDir))
        {
            CleanDirectory(artifactDir);
            DeleteDirectory(artifactDir, settings);
        }

        var binDirs = GetDirectories("./src/**/bin");
        var objDirs = GetDirectories("./src/**/obj");
        var testResDirs = GetDirectories("./**/TestResults");

        CleanDirectories(binDirs);
        CleanDirectories(objDirs);
        CleanDirectories(testResDirs);

        DeleteDirectories(binDirs, settings);
        DeleteDirectories(objDirs, settings);
        DeleteDirectories(testResDirs, settings);
    });

Task("PrepareDirectories")
    .Does(() =>
    {
        EnsureDirectoryExists(testResultDir);
        EnsureDirectoryExists(artifactDir);
    });

Task("Restore")
    .Does(() =>
    {
        DotNetCoreRestore();
    });

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("PrepareDirectories")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        Information("Build solution: {0}", solutionFile);
        var settings = GetBuildSettings();
        DotNetCoreBuild(solutionFile, settings);
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var settings = new DotNetCoreTestSettings
        {
            Configuration = configuration,
            NoBuild = true
        };

        if (isAppVeyor)
        {
            settings.ArgumentCustomization = args => args.Append("--logger:AppVeyor");
        }
        else
        {
            settings.ArgumentCustomization = args => args.Append("--logger:trx");
        }

        DotNetCoreTest(solutionFile, settings);

        var tmpTestResultFiles = GetFiles("./**/TestResults/*.*");
        CopyFiles(tmpTestResultFiles, testResultDir);
    });

Task("Pack")
    .IsDependentOn("Test")
    .Does(() =>
    {
        if (testFailed)
        {
            Information("Do not pack because tests failed");
            return;
        }

        var projects = GetSrcProjectFiles();
        var settings = new DotNetCorePackSettings
        {
            Configuration = configuration,
            OutputDirectory = artifactDir,
            MSBuildSettings = GetMSBuildSettings(version)
        };

        foreach (var project in projects)
        {
            Information("Pack {0}", project.FullPath);
            DotNetCorePack(project.FullPath, settings);
        }
    });

Task("Publish")
    .IsDependentOn("Test")
    .DoesForEach(GetSrcProjectFiles(), project =>
    {
        var projectDir = System.IO.Path.GetDirectoryName(project.FullPath);
        var projectName = new System.IO.DirectoryInfo(projectDir).Name;
        var outputDir = System.IO.Path.Combine(artifactDir, projectName);
        EnsureDirectoryExists(outputDir);

        Information("Publish {0} to {1}", projectName, outputDir);

        var settings = new DotNetCorePublishSettings
        {
            OutputDirectory = outputDir,
            Configuration = configuration,
            MSBuildSettings = GetMSBuildSettings(version)
        };

        DotNetCorePublish(project.FullPath, settings);
    });

Task("Build-Container")
    .IsDependentOn("Publish")
    .Does(() => {
        var imageName = GetImageName();
        var tagVersion = $"{imageName}:{version.FullSemVer}".ToLower();
        var tagLatest = $"{imageName}:latest".ToLower();

        Information($"Build docker image {tagVersion}");
        Information($"Build docker image {tagLatest}");

        var buildArgs = new DockerImageBuildSettings
        {
            Tag = new[] { tagVersion, tagLatest }
        };

        DockerBuild(buildArgs, solutionDir);
    });

Task("Push-Container")
    .IsDependentOn("Build-Container")
    .Does(() => {
        var imageName = GetImageName().ToLower();
        DockerPush(imageName);
    });

Task("Default")
    .IsDependentOn("Test")
    .Does(() =>
    {
        Information("Build and test the whole solution.");
        Information("To pack (nuget) the application use the cake build argument: --target Pack");
        Information("To publish (to run it somewhere else) the application use the cake build argument: --target Publish");
        Information("To build a Docker container with the application use the cake build argument: --target Build-Container");
        Information("To push the Docker container into an Docker registry use the cake build argument: --target Push-Container -dockerRegistry=\"yourregistry\"");
    });

Task("AppVeyor").IsDependentOn("Test");

Task("Travis").IsDependentOn("Test");

FilePathCollection GetSrcProjectFiles()
{
    return GetFiles("./src/**/*.csproj");
}

FilePathCollection GetTestProjectFiles()
{
    return GetFiles("./test/**/*.Tests/*.csproj");
}

FilePath GetSlnFile()
{
    return GetFiles("./**/*.sln").First();
}

FilePath GetMainProjectFile()
{
    foreach(var project in GetSrcProjectFiles())
    {
        if(project.FullPath.EndsWith($"{slnName}.Console.csproj"))
        {
            return project;
        }
    }

    Error("Cannot find main project file");
    return null;
}

string GetImageName()
{
    return $"{dockerRegistry}/{slnName}";
}

RunTarget(target);
