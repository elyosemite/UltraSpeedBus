## [1.5.0](https://github.com/elyosemite/UltraSpeedBus/compare/v1.4.0...v1.5.0) (2025-11-28)


### Features

* add EditorConfig for coding standards and update Directory.Build.props to treat warnings as errors ([af19968](https://github.com/elyosemite/UltraSpeedBus/commit/af19968a5ad72332a69ce612dbe373d083f17ccb))
* add IEventProcessor interface and improve CommandContext formatting ([438dd45](https://github.com/elyosemite/UltraSpeedBus/commit/438dd45dc2f735befc76623f2b138201a61c01e1))
* add newlines for improved readability in IConsumerRegister interface ([528538e](https://github.com/elyosemite/UltraSpeedBus/commit/528538e20eda5694ab631b6c5d73f6b0d079e8b2))
* enhance EditorConfig settings, fix method signature, and implement DynamicHandler for event handling ([af9641c](https://github.com/elyosemite/UltraSpeedBus/commit/af9641cae7e7f41c663184f1f65031085ffa79f0))
* implement command, query, and event handling in UltraMediator and update README ([e6445ee](https://github.com/elyosemite/UltraSpeedBus/commit/e6445eef68a553d98eb83320b51149a213fe1e2f))
* implement CreateOrder command handler and GetOrder query handler ([0121eb6](https://github.com/elyosemite/UltraSpeedBus/commit/0121eb658cc3039c594b3aa8b0bc28d524e556c5))
* refactor CommandContext to use primary constructor for command initialization ([aa3f79d](https://github.com/elyosemite/UltraSpeedBus/commit/aa3f79d213f66ba63703a54098e36eb62fcb81f5))
* refactor ConsumeContext and EventContext to use primary constructor for message initialization ([19d7c8f](https://github.com/elyosemite/UltraSpeedBus/commit/19d7c8fec929833487605fbd7cab616009eedfb1))
* refactor QueryContext to use primary constructor for query initialization ([56a7c66](https://github.com/elyosemite/UltraSpeedBus/commit/56a7c66bb472f4e35f8b917c02f44c2e4383c2dd))
* remove unnecessary whitespace in IMediator interface ([8ebd544](https://github.com/elyosemite/UltraSpeedBus/commit/8ebd544bede023d3cea5b97e4b8651eea3049b83))
* standardize property naming to lowercase for consistency across command and query records ([5518e4f](https://github.com/elyosemite/UltraSpeedBus/commit/5518e4fe3154b02f53e81fbe7075663da534e54e))
* update .editorconfig to disable SA1200 and SA1516 diagnostics ([1d8b535](https://github.com/elyosemite/UltraSpeedBus/commit/1d8b535d172e741aca59febf92ed732ae3cbad6f))
* update .editorconfig with new line preferences and adjust Visual Basic modifier order ([9fcffd5](https://github.com/elyosemite/UltraSpeedBus/commit/9fcffd523a0e8a9027142f49a8f91817f5852c99))
* update event handling for order creation and inventory addition ([9bb9a84](https://github.com/elyosemite/UltraSpeedBus/commit/9bb9a84f5d56b17c3bfa1c417b92419eb4de019d))
* update event handling interfaces and improve code analysis settings ([2280f6d](https://github.com/elyosemite/UltraSpeedBus/commit/2280f6d659621778a2e6beba0d5fee8899288c22))
* update README with additional configuration details and remove development environment checks ([e7dcbfc](https://github.com/elyosemite/UltraSpeedBus/commit/e7dcbfcf50a16aee9e23972034c78172d6e4b12a))

## [1.4.0](https://github.com/elyosemite/UltraSpeedBus/compare/v1.3.0...v1.4.0) (2025-11-26)


### Features

* add build task and define IPublish and ISend interfaces; remove obsolete message interfaces and handlers ([f06eb09](https://github.com/elyosemite/UltraSpeedBus/commit/f06eb09f6f4e57059159cf1113d18c41d884b076))
* add command and query handler examples to README ([b41b00e](https://github.com/elyosemite/UltraSpeedBus/commit/b41b00e7e683d777e22b168ab02975a1e1cf1a0d))
* add command, query, and event context classes and mediator interfaces ([9fd8585](https://github.com/elyosemite/UltraSpeedBus/commit/9fd85857f6797c41e18cdcc452704b97b1f7fb68))
* add CommandContext class for encapsulating command data ([5c824f5](https://github.com/elyosemite/UltraSpeedBus/commit/5c824f5681e5949be0dc19badd4b265609b98087))
* add documentation for UltraSpeedBus.Extensions.DependencyInjection package ([c7f0d69](https://github.com/elyosemite/UltraSpeedBus/commit/c7f0d69501a57630f0340eceadc0a669fce67787))
* add EventContext class for encapsulating event data ([417dd55](https://github.com/elyosemite/UltraSpeedBus/commit/417dd55a2de1f154cd5905eef658d42dcc04da7f))
* add IConsumerConnector interface for connecting message handlers ([06ecf68](https://github.com/elyosemite/UltraSpeedBus/commit/06ecf68d0246e123af057dd28f2c46d7394ca128))
* add IConsumerRegister interface for registering command, query, and event handlers ([2af1de5](https://github.com/elyosemite/UltraSpeedBus/commit/2af1de5dfff1dfb43d275698d0d5a7ec8203b11a))
* add IMediator interface for handling message sending and publishing ([e0404af](https://github.com/elyosemite/UltraSpeedBus/commit/e0404af7c309344ac63cb112fcd3f64e83bc5d8a))
* add initial implementation of UltraSpeedBus with command, query, and event handling ([e46db99](https://github.com/elyosemite/UltraSpeedBus/commit/e46db99b8861eadf3783652d15571c2d91796da0))
* add package metadata for UltraSpeedBus.Extensions.DependencyInjection ([9aafbf9](https://github.com/elyosemite/UltraSpeedBus/commit/9aafbf9fbdef59523d716b99d63f3e5dea5f6d7d))
* add QueryContext class for handling query data ([66c3907](https://github.com/elyosemite/UltraSpeedBus/commit/66c3907a16a8e61bc9f2acf8e60ab9aea8539a07))
* define IHandlerHandle and IDynamicHandler interfaces for message handling ([2759ae3](https://github.com/elyosemite/UltraSpeedBus/commit/2759ae31a6da97d7001b77ded5e409ed9ca2e8fb))
* implement ConsumeContext class for message handling ([e84146c](https://github.com/elyosemite/UltraSpeedBus/commit/e84146cd61b436b8bb3d1291cb65e7280c9ff002))
* implement UltraMediator class for handling commands, queries, and events ([c091e6e](https://github.com/elyosemite/UltraSpeedBus/commit/c091e6ee66ddd316d792bd1de4c2c11faa579db2))
* initialize project structure with Mediator pattern and basic command/event/query handling ([1442830](https://github.com/elyosemite/UltraSpeedBus/commit/1442830ce863582ae30fb3a88bb7cf33de955bc2))
* remove obsolete context and mediator classes and interfaces ([a9b0943](https://github.com/elyosemite/UltraSpeedBus/commit/a9b09432a589d896ab5e4a3b8482ca50f687a022))
* update README with command handler example and package installation instructions ([f4d84e2](https://github.com/elyosemite/UltraSpeedBus/commit/f4d84e2b2c58c6289d9114da118b12618aba87a9))

## [1.3.0](https://github.com/elyosemite/UltraSpeedBus/compare/v1.2.5...v1.3.0) (2025-11-18)


### Features

* add ICommandHandler, IEventHandler, IQueryHandler, ITransport, ITransportConsumer, and ITransportProducer interfaces ([7cf31ad](https://github.com/elyosemite/UltraSpeedBus/commit/7cf31ad59283d1d3205cd3dc8560668ce226d501))
* add pull_request trigger for CI workflow on main branch ([b634ca1](https://github.com/elyosemite/UltraSpeedBus/commit/b634ca1915e6ea0ce32635e949d58d05c9daeeb6))


### Bug Fixes

* restrict CI workflow branches to only main ([6deeac7](https://github.com/elyosemite/UltraSpeedBus/commit/6deeac73058f4d24ab071ee6489a861fb72a61c9))

## [1.2.5](https://github.com/elyosemite/UltraSpeedBus/compare/v1.2.4...v1.2.5) (2025-11-16)


### Bug Fixes

* correct typo in .gitignore for object files ([fa48794](https://github.com/elyosemite/UltraSpeedBus/commit/fa48794abea1eff93a0cf5bcfab4547737ab7484))

## [1.2.4](https://github.com/elyosemite/UltraSpeedBus/compare/v1.2.3...v1.2.4) (2025-11-16)


### Bug Fixes

* update PackageTags for clarity in project configuration ([ffea188](https://github.com/elyosemite/UltraSpeedBus/commit/ffea1881d99cfd809c2b3cd2da36e2a8914666d9))

## [1.2.3](https://github.com/elyosemite/UltraSpeedBus/compare/v1.2.2...v1.2.3) (2025-11-16)


### Bug Fixes

* remove unused PackageIcon property from project configuration ([f650d6f](https://github.com/elyosemite/UltraSpeedBus/commit/f650d6fc6f0a498efa2d5580d2b92e2349805195))

## [1.2.2](https://github.com/elyosemite/UltraSpeedBus/compare/v1.2.1...v1.2.2) (2025-11-16)


### Bug Fixes

* rename step ID for clarity in release workflow ([f72c4b5](https://github.com/elyosemite/UltraSpeedBus/commit/f72c4b562571e4300984d273d52af36ef5a92a43))

## [1.2.1](https://github.com/elyosemite/UltraSpeedBus/compare/v1.2.0...v1.2.1) (2025-11-16)


### Bug Fixes

* update release workflow to check for version file and improve NuGet package handling ([bba1ad2](https://github.com/elyosemite/UltraSpeedBus/commit/bba1ad27d7696e7669df3039c39f5499dfb7987d))

## [1.2.0](https://github.com/elyosemite/UltraSpeedBus/compare/v1.1.1...v1.2.0) (2025-11-16)


### Features

* add saga interfaces and update project structure for .NET 8.0 compatibility ([3dd0fe1](https://github.com/elyosemite/UltraSpeedBus/commit/3dd0fe1a7407709d05931ecd91e725e905ac8b3b))

## [1.1.1](https://github.com/elyosemite/UltraSpeedBus/compare/v1.1.0...v1.1.1) (2025-11-16)


### Bug Fixes

* update semantic release workflow to correctly handle versioning and release conditions ([3698d34](https://github.com/elyosemite/UltraSpeedBus/commit/3698d34511c275ba9828ed8499ab030e32998343))

## [1.1.0](https://github.com/elyosemite/UltraSpeedBus/compare/v1.0.0...v1.1.0) (2025-11-16)


### Features

* **docs:** add Git Semantic Commit Agent guidelines ([08bb010](https://github.com/elyosemite/UltraSpeedBus/commit/08bb010cc8bb6c6acf774c2ad27b295b54c99d4f))

## 1.0.0 (2025-11-16)


### Features

* add CI/CD workflows and semantic release configuration ([7f72a32](https://github.com/elyosemite/UltraSpeedBus/commit/7f72a32d5b01752eabae44c2af899bd761dc5fca))
