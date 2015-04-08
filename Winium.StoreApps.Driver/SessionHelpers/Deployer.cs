namespace Winium.StoreApps.Driver.SessionHelpers
{
    #region

    using System.Diagnostics;

    #endregion

    public class Deployer
    {
        #region Constants

        private const string CodedUiTestDllPath = @"..\..\..\Winium.StoreApps.CodedUITestProject\bin\Debug\CodedUITestProject1.dll";

        private const string PathToVsTestConsole =
            @"C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe";

        private const string SettingsPath = @"..\..\..\target.runsettings";

        #endregion

        #region Fields

        private Process codedUiTestProcess;

        #endregion

        #region Public Methods and Operators

        public void Close()
        {
            this.codedUiTestProcess.CloseMainWindow();
            this.codedUiTestProcess.Close();
        }

        public void DeployCodedUiTestServer()
        {
            // TODO We should generate run settings to specify device/emulator
            var codedUiTestLoopPsi = new ProcessStartInfo
                                         {
                                             FileName = PathToVsTestConsole, 
                                             Arguments =
                                                 string.Format(
                                                     "\"{0}\" /Settings:\"{1}\"", 
                                                     CodedUiTestDllPath, 
                                                     SettingsPath)
                                         };
            this.codedUiTestProcess = Process.Start(codedUiTestLoopPsi);
        }

        public void Install()
        {
        }

        #endregion

        // public void Launch() { }
        // public void ReciveFiles(Dictionary<string, string> files) { }
        // public void SendFiles(Dictionary<string, string> files) { }
        // public void Terminate() { }
        // public void Uninstall() { }
    }
}
