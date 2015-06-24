namespace Winium.StoreApps.Driver.Helpers
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;

    #endregion

    internal class VsTestConsoleWrapper : IDisposable
    {
        #region Constants

        private const string CodedUiTestDllPath =
            @"..\..\..\Winium.StoreApps.CodedUITestProject\bin\Debug\Winium.StoreApps.CodedUITestProject.dll";

        #endregion

        #region Fields

        private readonly string deviceName;

        private Process codedUiTestProcess;

        #endregion

        #region Constructors and Destructors

        public VsTestConsoleWrapper(string deviceName)
        {
            this.deviceName = deviceName;
        }

        #endregion

        #region Public Methods and Operators

        public void DeployCodedUiTestServer(bool captureCodedUiLogs)
        {
            var pathToVsTestconsole = ConfigurationManager.AppSettings["VsTestConsolePath"];

            var runSettingsDoc = new RunSettings(this.deviceName);
            var tempFilePath = Path.GetTempFileName();

            runSettingsDoc.XmlDoc.Save(tempFilePath);

            var arguments = new List<string>
                                {
                                    string.Format(CultureInfo.InvariantCulture, "\"{0}\"", CodedUiTestDllPath),
                                    string.Format(CultureInfo.InvariantCulture, "/Settings:\"{0}\"", tempFilePath),
                                    "/InIsolation"
                                };
            if (captureCodedUiLogs)
            {
                arguments.Add("/logger:trx");
            }

            // TODO We should generate run settings to specify device/emulator
            var codedUiTestLoopPsi = new ProcessStartInfo
                                         {
                                             UseShellExecute = false, 
                                             RedirectStandardError = true, 
                                             RedirectStandardOutput = true, 
                                             FileName = pathToVsTestconsole, 
                                             Arguments = string.Join(" ", arguments)
                                         };

            this.codedUiTestProcess = new Process { StartInfo = codedUiTestLoopPsi };
            this.codedUiTestProcess.OutputDataReceived += CaptureOutput;
            this.codedUiTestProcess.ErrorDataReceived += CaptureOutput;
            this.codedUiTestProcess.Start();
            this.codedUiTestProcess.BeginOutputReadLine();
        }

        public void Dispose()
        {
            Logger.Debug(string.Format("Closing {0}", this.codedUiTestProcess.ProcessName));
            this.codedUiTestProcess.CloseMainWindow();
            this.codedUiTestProcess.Close();
        }

        #endregion

        #region Methods

        private static void CaptureOutput(object sender, DataReceivedEventArgs args)
        {
            Logger.Debug("VSTEST: {0}", args.Data);
        }

        #endregion
    }
}