namespace Winium.StoreApps.Driver.SessionHelpers
{
    internal class Session
    {
        public Session(string sessionId)
        {
            this.SessionId = sessionId;
        }

        #region Fields

        public string SessionId { get; private set; }

        #endregion

        #region Public Properties

        public Capabilities ActualCapabilities { get; set; }

        public Requester CommandForwarder { get; set; }

        public Deployer Deployer { get; set; }

        #endregion


    }
}
