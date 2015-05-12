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
            var session = SessionsManager.CreateSession();
            var serializedCapability =
                JsonConvert.SerializeObject(this.ExecutedCommand.Parameters["desiredCapabilities"]);
            session.ActualCapabilities = Capabilities.CapabilitiesFromJsonString(serializedCapability);

            // TODO Add code to start emulator or device and deploy app
            session.Deployer = new Deployer(
                session.ActualCapabilities.DeviceName, 
                session.ActualCapabilities.DeviceIpAddress,
                session.ActualCapabilities.Locale);

            if (!session.ActualCapabilities.DebugCodedUi)
            {
                Logger.Info("Deploying CodedUI test server.");
                session.Deployer.DeployCodedUiTestServer();
            }

            if (!string.IsNullOrEmpty(session.ActualCapabilities.App))
            {
                session.Deployer.Install(session.ActualCapabilities.App);
            }

            session.CommandForwarder = new Requester(session.Deployer.IpAddress, session.ActualCapabilities.InnerPort);

            return this.JsonResponse(ResponseStatus.Success, session.ActualCapabilities);
        }

        #endregion
    }
}