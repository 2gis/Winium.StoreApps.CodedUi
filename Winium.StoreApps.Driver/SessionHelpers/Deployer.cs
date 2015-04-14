namespace Winium.StoreApps.Driver.SessionHelpers
{
    #region

    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;

    using Microsoft.SmartDevice.MultiTargeting.Connectivity;

    using Winium.StoreApps.Common;

    #endregion

    public class Deployer
    {
        #region Constants

        private const string CodedUiTestDllPath =
            @"..\..\..\Winium.StoreApps.CodedUITestProject\bin\Debug\CodedUITestProject1.dll";

        private const string SettingsPath = @"..\..\..\target.runsettings";

        #endregion

        #region Fields

        private Process codedUiTestProcess;

        #endregion

        #region Constructors and Destructors

        public Deployer(string deviceName, string ipAddress = null)
        {
            this.IpAddress = ipAddress;
            this.DeviceName = deviceName;

            this.Connect();
        }

        #endregion

        #region Public Properties

        public string DeviceName { get; private set; }

        public string IpAddress { get; private set; }

        #endregion

        #region Public Methods and Operators

        public void Close()
        {
            this.codedUiTestProcess.CloseMainWindow();
            this.codedUiTestProcess.Close();
        }

        public void DeployCodedUiTestServer()
        {
            var pathToVsTestconsole = System.Configuration.ConfigurationManager.AppSettings["VsTestConsolePath"];

            // TODO We should generate run settings to specify device/emulator
            var codedUiTestLoopPsi = new ProcessStartInfo
                                         {
                                             FileName = pathToVsTestconsole, 
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

        #region Methods

        private void Connect()
        {
            var connectivity = new MultiTargetingConnectivity(CultureInfo.CurrentUICulture.LCID);

            var connectableDevices = connectivity.GetConnectableDevices();

            var matchingDevice = connectableDevices.FirstOrDefault(x => x.Name.Equals(this.DeviceName));

            if (matchingDevice == null)
            {
                throw new AutomationException("No devices or emulators found with name {}", this.DeviceName);
            }

            var device = matchingDevice.Connect();

            if (this.IpAddress == null)
            {
                
                string sourceIp;
                string destinationIp;
                int destinationPort;
                device.GetEndPoints(9998, out sourceIp, out destinationIp, out destinationPort);
                this.IpAddress = destinationIp;
            }

        }

        #endregion

        // public void Launch() { }
        // public void ReciveFiles(Dictionary<string, string> files) { }
        // public void SendFiles(Dictionary<string, string> files) { }
        // public void Terminate() { }
        // public void Uninstall() { }
    }
}