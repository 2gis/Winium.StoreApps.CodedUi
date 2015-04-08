# Winium.StoreApps.CodedUi

Winium.StoreApps.CodedUi is an prototype test automation tool for Windows Store apps, tested on emulators and devices.

It is based on https://github.com/2gis/Winium.StoreApps

## How does it work

There are two parts: Winium.StoreApps.Driver and Winium.StoreApps.CodedUITestProject

### Winium.StoreApps.CodedUITestProject
- Looped CodedUI test that gots deployed to emulator or device (currently emulator is hardcoded in runsettings for vs.test.console, but it can be generated at runtime).
- Runs Socketserver that listens for automation commands (those commands correspond to JsonWireProtocol commands)

### Winium.StoreApps.Driver
- Selenium Remote WebDriver implementation
- Listens for your test commands
- Handles NewSession command
 - Start emulator or device and deploys your app (currently not implemented in protype, but implemented in parent repository)
 - Deploys Winium.StoreApps.CodedUITestProject
-  Handles Close command
 - Stops Winium.StoreApps.CodedUITestProject server
- Proxies all commands (except NewSession and Close) to Winium.StoreApps.CodedUITestProject


## How to run test

This is prototype only. What emulator to use and emulator's ip are hardcoded (but it can be done programmatically, see parent repository).

1. You will need to launch emulator and deploy your app under test using Visual Studio or AppDeployer (solution comes with included `Winium.StoreApps.TestApp`)
2. Open emulator `Additional Tools` (`>>` button in top right toolbar of emulator)
3. Locate `Emulator Adapter #1:` `Network addresses:` and copy `Preferred` IP address
4. Open solution and navigate to `NewSessionExecutor.cs` file in `Winium.StoreApps.Driver` under `CommandExecutors`
5. Replace value of `const string InnerIp` with IP adress you have copied (we could have put it in some app config, but it would be done automatically very soon anyways)
6. If you have Visual Studio different from 2013 or in non standard path, then replace `PathToVsTestConsole` value with actual path to `vstest.console.exe`. Note: different versions of z`vs.test.console.exe` might require different command line options, we tested prototype only against VS 2013.
7. Build solution
8. Run tests (solution comes with `sample_test.py`)

This is only a prototype. Deployment and ip address resolution will be made programmatically.
We already support some basic commands:
- NewSession
- FindElement
- FindChildElement
- GetElementText
- ClickElement
- Close

