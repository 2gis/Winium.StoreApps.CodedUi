namespace Winium.StoreApps.Driver.CommandExecutors
{
    #region

    using Newtonsoft.Json;

    using Winium.StoreApps.Common;
    using Winium.StoreApps.Driver.Helpers;

    #endregion

    internal class NewSessionExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            this.Session = SessionsManager.CreateSession();

            var session = this.Session;
            var serializedCapability =
                JsonConvert.SerializeObject(this.ExecutedCommand.Parameters["desiredCapabilities"]);
            session.ActualCapabilities = Capabilities.CapabilitiesFromJsonString(serializedCapability);

            // TODO Add code to start emulator or device and deploy app
            session.Deployer = new Deployer(
                session.ActualCapabilities.DeviceName, 
                session.ActualCapabilities.DeviceIpAddress,
                session.ActualCapabilities.Locale,
                session.ActualCapabilities.DebugCaptureLogs);

            if (!session.ActualCapabilities.DebugCodedUi)
            {
                Logger.Info("Deploying CodedUI test server.");
                session.Deployer.DeployCodedUiTestServer();
            }

            session.Deployer.InstallDependencies(session.ActualCapabilities.Dependencies);

            if (!string.IsNullOrEmpty(session.ActualCapabilities.App))
            {
                session.Deployer.Install(session.ActualCapabilities.App);
                session.Deployer.SendFiles(session.ActualCapabilities.Files);
                session.Deployer.LaunchApplication();
            }

            session.CommandForwarder = new Requester(session.Deployer.IpAddress, session.ActualCapabilities.InnerPort);

            return this.JsonResponse(ResponseStatus.Success, session.ActualCapabilities);
        }

        #endregion
    }
}