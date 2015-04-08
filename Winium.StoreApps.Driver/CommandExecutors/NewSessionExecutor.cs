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
            const string InnerIp = "169.254.251.141"; // TODO discover IP
            session.CommandForwarder = new Requester(InnerIp, session.ActualCapabilities.InnerPort);

            // TODO Add code to start emulator or device and deploy app
            // TODO Add deployment of CodedUI test project
            session.Deployer = new Deployer();
            session.Deployer.DeployCodedUiTestServer();

            return this.JsonResponse(ResponseStatus.Success, session.ActualCapabilities);
        }

        #endregion
    }
}
