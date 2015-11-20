namespace Winium.StoreApps.Driver.CommandExecutors
{
    #region

    using System;
    using System.Security.Authentication;
    using System.Threading;

    using Newtonsoft.Json;

    using Winium.StoreApps.Common;
    using Winium.StoreApps.Driver.Helpers;
    using Winium.StoreApps.Logging;

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

            session.DeviceBridge = new DeviceBridge(
                session.ActualCapabilities.DeviceName, 
                session.ActualCapabilities.DeviceIpAddress, 
                session.ActualCapabilities.Locale, 
                session.ActualCapabilities.DebugCaptureLogs);

            if (!session.ActualCapabilities.DebugCodedUi)
            {
                Logger.Info("Deploying CodedUI test server.");
                session.DeviceBridge.DeployCodedUiTestServer();
            }

            if (!string.IsNullOrEmpty(session.ActualCapabilities.App))
            {
                session.DeviceBridge.Install(session.ActualCapabilities.App);
            }

            session.CommandForwarder = new Requester(
                session.DeviceBridge.IpAddress, 
                session.ActualCapabilities.InnerPort);

            if (WaitForConnection(session) == false)
            {
                throw new AutomationException("Could not connect to Test Server", ResponseStatus.SessionNotCreatedException);
            }

            return this.JsonResponse(ResponseStatus.Success, session.ActualCapabilities);
        }

        private static bool WaitForConnection(Session session)
        {
            var retires = session.ActualCapabilities.PingRetries;

            while (retires > 0)
            {
                --retires;
                var rv = SendPing(session);
                if (rv)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool SendPing(Session session)
        {
            Logger.Debug("Sending ping request to Test Server");
            var pingCmd = new Command("ping");
            var response = session.CommandForwarder.ForwardCommand(pingCmd, false, 2500);
            return response.Trim().Equals("pong");
        }

        #endregion
    }
}
