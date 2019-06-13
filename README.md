# cshrix-bot [![#cshrix-bot:sharparam.com][matrix-badge]][matrix-cshrix-bot]

[![MPL 2.0 License][mpl-badge]][mpl]
[![Latest GitHub release][ghreleasebadge]][ghrelease]
[![Docker image][docker-badge]][docker]

| [Master][master] | [![Build status][appveyor-master-badge]][appveyor-master-status] | [![TravisCI Status][travis-master-badge]][travis-master-status] | [![Test status][test-master-badge]][test-master-status] | [![Coverage][coveralls-master-badge]][coveralls-master] | [![Codecov coverage][codecov-master-badge]][codecov-master] | [![CodeFactor][codefactor-master-badge]][codefactor-master] |
|-|-|-|-|-|-|-|
| [**Develop**][develop] | [![Build status][appveyor-develop-badge]][appveyor-develop-status] | [![TravisCI Status][travis-develop-badge]][travis-develop-status] | [![Test status][test-develop-badge]][test-develop-status] | [![Coverage][coveralls-develop-badge]][coveralls-develop] | [![Codecov coverage][codecov-develop-badge]][codecov-develop] | [![CodeFactor][codefactor-develop-badge]][codefactor-develop] |

Matrix bot using the cshrix library.

This bot is a work in progress, when it is more finalized, the core library to
interact with Matrix will be extracted into its own repository.

## Acknowledgements

Big thanks to the almighty [@expectocode][expectocode] for
coming up with a name for this project.

## License

Copyright © 2019 by [Adam Hellberg][sharparam].

This Source Code Form is subject to the terms of the [Mozilla Public
License, v. 2.0][mpl]. If a copy of the MPL was not distributed with this
file, You can obtain one at http://mozilla.org/MPL/2.0/.

See the file [LICENSE][] for more details.

## Code of Conduct

This project is covered under a [Code of Conduct][coc], please remember to
adhere by this when interacting with the project.

## Contributing

Please see the [CONTRIBUTING][] document for our contribution guidelines.

## End-to-end (E2E) encryption

In order for E2EE features to work, [libolm][] must be available on the system
`PATH` or located alongside the executable
(cshrix calls into it using P/Interop).

### Linux

Check if your distro's package manager has [libolm][] available.
It usually goes under the name `libolm` and/or `libolm-dev`.

#### Debian/Ubuntu

Debian: buster/sid and later.

Ubuntu: 18.04 (bionic) and later.

```
# apt install libolm-dev
```

#### Arch

[`libolm` is available in the AUR][libolm-aur] and as a
[git version][libolm-git-aur].


### Windows

As of right now (2019-04-23), no official Windows releases of [libolm][] exist.
Build the library manually and place it either somewhere on the `PATH` or
alongside the executable.

### macOS

Unknown. If someone has information about [libolm][] on macOS, please make
a [PR][] and update this section.

[cshrix]: https://github.com/Sharparam/cshrix

[mpl]: https://mozilla.org/MPL/2.0/
[mpl-osi]: https://opensource.org/licenses/MPL-2.0
[mpl-badge]: https://img.shields.io/badge/license-MPL%202.0-blue.svg

[license]: LICENSE
[coc]: CODE_OF_CONDUCT.md
[contributing]: CONTRIBUTING.md

[sharparam]: https://github.com/Sharparam
[expectocode]: https://github.com/expectocode

[master]: https://github.com/Sharparam/cshrix-bot/tree/master
[develop]: https://github.com/Sharparam/cshrix-bot/tree/develop
[pr]: https://github.com/Sharparam/cshrix-bit/pulls

[libolm]: https://gitlab.matrix.org/matrix-org/olm
[libolm-aur]: https://aur.archlinux.org/packages/libolm/
[libolm-git-aur]: https://aur.archlinux.org/packages/libolm-git/

[matrix-sharparam]: https://matrix.to/#/@sharparam:sharparam.com
[matrix-cshrix-bot]: https://matrix.to/#/!RRgORzEtLTHVHtwEVi:sharparam.com?via=sharparam.com
[matrix-badge]: https://img.shields.io/matrix/cshrix-bot:sharparam.com.svg?label=%23cshrix-bot%3Asharparam.com&logo=matrix&server_fqdn=sharparam.com

[ghrelease]: https://github.com/Sharparam/cshrix-bot/releases
[ghreleasebadge]: https://img.shields.io/github/release/Sharparam/cshrix-bot.svg?logo=github

[docker]: https://hub.docker.com/r/sharparam/cshrix-bot
[docker-badge]: https://img.shields.io/badge/docker-sharparam%2Fcshrix--bot-blue.svg

[appveyor-develop-status]: https://ci.appveyor.com/project/Sharparam/cshrix-bot/branch/develop
[appveyor-develop-badge]: https://ci.appveyor.com/api/projects/status/e331me30fi95jgf1/branch/develop?svg=true
[travis-develop-status]: https://travis-ci.com/Sharparam/cshrix-bot
[travis-develop-badge]: https://travis-ci.com/Sharparam/cshrix-bot.svg?branch=develop
[test-develop-status]: https://ci.appveyor.com/project/Sharparam/cshrix-bot/branch/develop/tests
[test-develop-badge]: https://img.shields.io/appveyor/tests/Sharparam/cshrix-bot/develop.svg
[coveralls-develop]: https://coveralls.io/github/Sharparam/cshrix-bot?branch=develop
[coveralls-develop-badge]: https://coveralls.io/repos/github/Sharparam/cshrix-bot/badge.svg?branch=develop
[codecov-develop]: https://codecov.io/gh/Sharparam/cshrix-bot/branch/develop
[codecov-develop-badge]: https://codecov.io/gh/Sharparam/cshrix-bot/branch/develop/graph/badge.svg
[codefactor-develop]: https://www.codefactor.io/repository/github/sharparam/cshrix-bot/overview/develop
[codefactor-develop-badge]: https://www.codefactor.io/repository/github/sharparam/cshrix-bot/badge/develop

[appveyor-master-status]: https://ci.appveyor.com/project/Sharparam/cshrix-bot/branch/master
[appveyor-master-badge]: https://ci.appveyor.com/api/projects/status/e331me30fi95jgf1/branch/master?svg=true
[travis-master-status]: https://travis-ci.com/Sharparam/cshrix-bot
[travis-master-badge]: https://travis-ci.com/Sharparam/cshrix-bot.svg?branch=master
[test-master-status]: https://ci.appveyor.com/project/Sharparam/cshrix-bot/branch/master/tests
[test-master-badge]: https://img.shields.io/appveyor/tests/Sharparam/cshrix-bot/master.svg
[coveralls-master]: https://coveralls.io/github/Sharparam/cshrix-bot
[coveralls-master-badge]: https://coveralls.io/repos/github/Sharparam/cshrix-bot/badge.svg
[codecov-master]: https://codecov.io/gh/Sharparam/cshrix-bot
[codecov-master-badge]: https://codecov.io/gh/Sharparam/cshrix-bot/branch/master/graph/badge.svg
[codefactor-master]: https://www.codefactor.io/repository/github/sharparam/cshrix-bot/overview/master
[codefactor-master-badge]: https://www.codefactor.io/repository/github/sharparam/cshrix-bot/badge/master
