namespace Winium.StoreApps.Driver.SessionHelpers
{
    #region

    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    using Microsoft.SmartDevice.Connectivity.Interface;
    using Microsoft.SmartDevice.MultiTargeting.Connectivity;

    using Winium.StoreApps.Common;

    #endregion

    public class Deployer
    {
        #region Constants

        private const string CodedUiTestDllPath =
            @"..\..\..\Winium.StoreApps.CodedUITestProject\bin\Debug\CodedUITestProject1.dll";

        #endregion

        #region Fields

        private Process codedUiTestProcess;

        private IDevice device;

        #endregion

        #region Constructors and Destructors

        public Deployer(string deviceName, string ipAddress, string localeTag)
        {
            this.IpAddress = ipAddress;
            this.DeviceName = deviceName;

            this.Culture = new CultureInfo(localeTag);

            this.Connect();
        }

        #endregion

        #region Public Properties

        public CultureInfo Culture { get; private set; }

        public string DeviceName { get; private set; }

        public string IpAddress { get; private set; }

        #endregion

        #region Public Methods and Operators

        public void Close()
        {
            this.codedUiTestProcess.CloseMainWindow();
            this.codedUiTestProcess.Close();

            this.device.Disconnect();
        }

        public void DeployCodedUiTestServer()
        {
            var pathToVsTestconsole = System.Configuration.ConfigurationManager.AppSettings["VsTestConsolePath"];

            var runSettingsDoc = new RunSettings(this.DeviceName);
            var tempFilePath = Path.GetTempFileName();

            runSettingsDoc.XmlDoc.Save(tempFilePath);

            // TODO We should generate run settings to specify device/emulator
            var codedUiTestLoopPsi = new ProcessStartInfo
                                         {
                                             FileName = pathToVsTestconsole, 
                                             Arguments =
                                                 string.Format(
                                                     "\"{0}\" /Settings:\"{1}\"", 
                                                     CodedUiTestDllPath, 
                                                     tempFilePath)
                                         };
            this.codedUiTestProcess = Process.Start(codedUiTestLoopPsi);
        }

        public void Install()
        {
        }

        #endregion

        #region Methods

        private void Connect()
        {
            var connectivity = new MultiTargetingConnectivity(this.Culture.LCID);

            var connectableDevices = connectivity.GetConnectableDevices();

            var matchingDevice = connectableDevices.FirstOrDefault(x => x.Name.StartsWith(this.DeviceName));

            if (matchingDevice == null)
            {
                throw new AutomationException("No devices or emulators found with name {0}", this.DeviceName);
            }

            this.DeviceName = matchingDevice.Name;
            Logger.Info("Connecting to {0}...", this.DeviceName);

            this.device = matchingDevice.Connect();

            // "deviceIpAddress" capability is ignored for emulators
            if (this.IpAddress == null || matchingDevice.IsEmulator())
            {
                string sourceIp;
                string destinationIp;
                int destinationPort;
                this.device.GetEndPoints(9998, out sourceIp, out destinationIp, out destinationPort);
                this.IpAddress = destinationIp;
            }

            Logger.Info("CodedUI Test server IP address is {0}", this.IpAddress);
        }

        #endregion

        // public void Launch() { }
        // public void ReciveFiles(Dictionary<string, string> files) { }
        // public void SendFiles(Dictionary<string, string> files) { }
        // public void Terminate() { }
        // public void Uninstall() { }
    }
}