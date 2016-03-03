using System.IO;

namespace Winium.StoreApps.Driver.Helpers
{
    #region

    using System;
    using System.Globalization;
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.Phone.Tools.Deploy;
    using Microsoft.SmartDevice.Connectivity.Interface;
    using Microsoft.SmartDevice.MultiTargeting.Connectivity;

    using Winium.StoreApps.Common;

    #endregion

    public class Deployer : IDisposable
    {
        #region Fields

        private readonly bool captureCodedUiLogs;

        private ConnectableDevice connectableDevice;

        private IDevice device;

        private Guid productId;

        private bool disposed;

        private VsTestConsoleWrapper testConsole;

        #endregion

        #region Constructors and Destructors

        public Deployer(string desiredDeviceName, string ipAddress, string localeTag, bool captureCodedUiLogs)
        {
            this.IpAddress = ipAddress;

            this.Culture = new CultureInfo(localeTag);

            this.Connect(desiredDeviceName);

            this.captureCodedUiLogs = captureCodedUiLogs;
        }

        #endregion

        #region Public Properties

        public string IpAddress { get; private set; }

        #endregion

        #region Properties

        private CultureInfo Culture { get; set; }

        private string DeviceName
        {
            get
            {
                return this.connectableDevice == null ? null : this.connectableDevice.Name;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void DeployCodedUiTestServer()
        {
            this.testConsole = new VsTestConsoleWrapper(this.DeviceName);
            this.testConsole.DeployCodedUiTestServer(this.captureCodedUiLogs);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Install(string appxPath)
        {
            GlobalOptions.LaunchAfterInstall = false;
            var appManifestInfo = Utils.ReadAppManifestInfoFromPackage(appxPath);
            this.productId = appManifestInfo.ProductId;
            InstallApplicationPackage(appxPath);

            Logger.Info("Successfully deployed using Microsoft.Phone.Tools.Deploy");
        }

        public void InstallDependencies(List<string> appPaths)
        {
            foreach (string appxPaxkage in appPaths)
            {
                InstallApplicationPackage(appxPaxkage);
            }
        }

        private void InstallApplicationPackage(string appxPath)
        {
            var appManifestInfo = Utils.ReadAppManifestInfoFromPackage(appxPath);
            var devices = Utils.GetDevices();
            var deviceInfo = devices.First(x => x.ToString().Equals(this.DeviceName));

            Utils.InstallApplication(deviceInfo, appManifestInfo, DeploymentOptions.None, appxPath);
        }

        public void LaunchApplication()
        {
            var app = this.device.GetApplication(this.productId);
            app.Launch();
        }

        #endregion

        #region Methods

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                if (this.testConsole != null)
                {
                    this.testConsole.Dispose();
                }
            }

            // Free any unmanaged objects here. 
            this.device.Disconnect();

            this.disposed = true;
        }

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
        public void SendFiles(Dictionary<string, string> files)
        {
            if (files == null || !files.Any())
            {
                return;
            }
            var app = this.device.GetApplication(this.productId);
            var store = app.GetIsolatedStore("Local");
            foreach (var file in files)
            {
                var phoneDirectoryName = Path.GetDirectoryName(file.Value);
                var phoneFileName = Path.GetFileName(file.Value);
                if (string.IsNullOrEmpty(phoneFileName))
                {
                    phoneFileName = Path.GetFileName(file.Key);
                }
                store.SendFile(file.Key, Path.Combine(phoneDirectoryName, phoneFileName), true);
            }
        }
        // public void Terminate() { }
        // public void Uninstall() { }
    }
}