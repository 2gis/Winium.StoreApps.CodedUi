namespace Winium.StoreApps.Driver.Helpers
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Reflection;

    #endregion

    internal class VsTestConsoleWrapper : IDisposable
    {
        #region Fields

        private readonly string codedUiTestDllPath;

        private readonly string deviceName;

        private Process codedUiTestProcess;

        #endregion

        #region Constructors and Destructors

        public VsTestConsoleWrapper(string deviceName)
        {
            this.deviceName = deviceName;

            var assembly = Assembly.GetExecutingAssembly();
            var assemblyDir = Path.GetDirectoryName(assembly.Location);

            if (assemblyDir == null)
            {
                throw new InvalidOperationException(
                    "Assembly directory is null, unable to get full path for Winium.StoreApps.CodedUITestProject.dll");
            }

            this.codedUiTestDllPath = Path.Combine(assemblyDir, "Winium.StoreApps.CodedUITestProject.dll");
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
                                    string.Format(CultureInfo.InvariantCulture, "\"{0}\"", this.codedUiTestDllPath), 
                                    string.Format(CultureInfo.InvariantCulture, "/Settings:\"{0}\"", tempFilePath), 
                                    "/InIsolation"
                                };
            if (captureCodedUiLogs)
            {
                arguments.Add("/logger:trx");
            }

            var codedUiTestLoopPsi = new ProcessStartInfo
                                         {
                                             FileName = pathToVsTestconsole, 
                                             Arguments = string.Join(" ", arguments), 
                                             UseShellExecute = false, 
                                             RedirectStandardError = true, 
                                             RedirectStandardOutput = true, 
                                         };

            Logger.Info(
                "Starting CodedUi Test Server using {0} {1}", 
                codedUiTestLoopPsi.FileName, 
                codedUiTestLoopPsi.Arguments);

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