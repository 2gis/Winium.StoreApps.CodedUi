namespace Winium.StoreApps.Driver.CommandExecutors
{
    #region

    using Newtonsoft.Json;

    using Winium.StoreApps.Common;
    using Winium.StoreApps.Driver.SessionHelpers;

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
            // TODO Add deployment of CodedUI test project
            session.Deployer = new Deployer("Emulator 8.1 WVGA 4 inch 512MB");
            session.Deployer.DeployCodedUiTestServer();

            session.CommandForwarder = new Requester(session.Deployer.IpAddress, session.ActualCapabilities.InnerPort);

            return this.JsonResponse(ResponseStatus.Success, session.ActualCapabilities);
        }

        #endregion
    }
}
