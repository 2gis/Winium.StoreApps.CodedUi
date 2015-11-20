namespace Winium.StoreApps.Driver.Helpers
{
    #region

    using System;
    using System.Globalization;

    using Winium.Mobile.Connectivity;
    using Winium.StoreApps.Logging;

    #endregion

    public class DeviceBridge : IDisposable
    {
        #region Fields

        private readonly bool captureCodedUiLogs;

        private readonly IDeployer deployer;

        private bool disposed;

        private VsTestConsoleWrapper testConsole;

        #endregion

        #region Constructors and Destructors

        public DeviceBridge(string desiredDeviceName, string ipAddress, string localeTag, bool captureCodedUiLogs)
        {
            this.Culture = new CultureInfo(localeTag);

            this.deployer = DeployerFactory.DeployerForPackage(null, desiredDeviceName, false, this.Culture);

            this.GetIpAddress(ipAddress);

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
                return this.deployer.DeviceName;
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
            this.deployer.Install(appxPath, null);
            this.deployer.Launch();
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

            // TODO make IDeployer disposable and disconnect form connectableDevice
            this.disposed = true;
        }

        private void GetIpAddress(string providedIpAddress)
        {
            // "deviceIpAddress" capability is ignored for emulators
            if (providedIpAddress == null || this.deployer.IsEmulator)
            {
                string sourceIp;
                string destinationIp;
                int destinationPort;
                this.deployer.GetEndPoints(9998, out sourceIp, out destinationIp, out destinationPort);
                this.IpAddress = destinationIp;
            }
            else
            {
                this.IpAddress = providedIpAddress;
            }

            Logger.Info("CodedUI Test server IP address is {0}", this.IpAddress);
        }

        #endregion
    }
}
