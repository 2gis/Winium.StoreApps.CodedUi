namespace Winium.StoreApps.Driver.Helpers
{
    #region

    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    using Microsoft.Phone.Tools.Deploy;
    using Microsoft.SmartDevice.Connectivity.Interface;
    using Microsoft.SmartDevice.MultiTargeting.Connectivity;

    using Winium.StoreApps.Common;

    #endregion

    public class Deployer
    {
        #region Constants

        private const string CodedUiTestDllPath =
            @"..\..\..\Winium.StoreApps.CodedUITestProject\bin\Debug\Winium.StoreApps.CodedUITestProject.dll";

        #endregion

        #region Fields

        private Process codedUiTestProcess;

        private IDevice device;

        private ConnectableDevice connectableDevice;

        #endregion

        #region Constructors and Destructors

        public Deployer(string desiredDeviceName, string ipAddress, string localeTag)
        {
            this.IpAddress = ipAddress;

            this.Culture = new CultureInfo(localeTag);

            this.Connect(desiredDeviceName);
        }

        #endregion

        #region Public Properties

        public CultureInfo Culture { get; private set; }

        public string DeviceName
        {
            get
            {
                return this.connectableDevice == null ? null : this.connectableDevice.Name;
            }
        }

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

        public void Install(string appxPath)
        {
            var appManifestInfo = Utils.ReadAppManifestInfoFromPackage(appxPath);
            var devices = Utils.GetDevices();
            var deviceInfo = devices.First(x => x.ToString().Equals(this.DeviceName));

            GlobalOptions.LaunchAfterInstall = true;
            Utils.InstallApplication(deviceInfo, appManifestInfo, DeploymentOptions.None, appxPath);

            Logger.Info("Successfully deployed using Microsoft.Phone.Tools.Deploy");
        }

        #endregion

        #region Methods

        private void Connect(string desiredDeviceName)
        {
            var connectivity = new MultiTargetingConnectivity(this.Culture.LCID);

            var connectableDevices = connectivity.GetConnectableDevices();

            var matchingDevice = connectableDevices.FirstOrDefault(x => x.Name.StartsWith(desiredDeviceName));

            if (matchingDevice == null)
            {
                throw new AutomationException("No devices or emulators found with name {0}", desiredDeviceName);
            }

            this.connectableDevice = matchingDevice;

            Logger.Info("Connecting to {0}...", this.DeviceName);
            
            this.device = matchingDevice.Connect();

            this.GetIpAddress(matchingDevice.IsEmulator());
        }

        private void GetIpAddress(bool isEmulator)
        {
            // "deviceIpAddress" capability is ignored for emulators
            if (this.IpAddress == null || isEmulator)
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