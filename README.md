# Winium.StoreApps.CodedUi

Winium.StoreApps.CodedUi is an prototype test automation tool for Windows Store apps, tested on emulators and devices.

It is based on https://github.com/2gis/Winium.StoreApps

## How does it work

There are two parts: Winium.StoreApps.Driver and Winium.StoreApps.CodedUITestProject

### Winium.StoreApps.CodedUITestProject
- Looped CodedUI test that gets deployed to selected emulator or device
- Runs Socketserver that listens for automation commands (those commands correspond to JsonWireProtocol commands)

### Winium.StoreApps.Driver
- Selenium Remote WebDriver implementation
- Listens for your test commands
- Handles NewSession command
 - Start emulator or device and deploys your app
 - Deploys Winium.StoreApps.CodedUITestProject
-  Handles Quit command
 - Stops Winium.StoreApps.CodedUITestProject server
- Proxies all commands (except NewSession and Quit) to Winium.StoreApps.CodedUITestProject

### Winium.StoreApps vs Winium.StoreApps.CodedUi
[Winium.StoreApps vs Winium.StoreApps.CodedUi](https://github.com/2gis/Winium/wiki/Winium.StoreApps-vs-Winium.StoreApps.CodedUi)

## How to run test

1. Build solution
2. If you have Visual Studio different from 2013 or in non standard path, then replace `VsTestConsolePath` value in App.config with actual path to `vstest.console.exe`. Note: different versions of `vs.test.console.exe` might require different command line options, we tested prototype only against VS 2013.
3. Optionally. Build and create store package (`appx`) for `Winium.StoreApps.TestApp` (if you want to run test against it)
4. Run `Winium.StoreApps.Driver`
5. Run tests (solution comes with [samples](TestExamples/test_sample.py))
 
*Note:* to run on devices you will need to setup your system as described in https://github.com/2gis/Winium.StoreApps.CodedUi/issues/1#issuecomment-94719621 and use `deviceIpAddress` capability set to `localhost`.

We already support some basic commands:
- NewSession
- FindElement
- FindChildElement
- FindElements
- FindChildElements
- GetElementText
- ClickElement
- SendKeysToElement
- SwitchToWindow
- Close
