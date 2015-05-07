namespace Winium.StoreApps.Driver.SessionHelpers
{
    internal class Session
    {
        #region Constructors and Destructors

        public Session(string sessionId)
        {
            this.SessionId = sessionId;
        }

        #endregion

        #region Public Properties

        public Capabilities ActualCapabilities { get; set; }

        public Requester CommandForwarder { get; set; }

        public Deployer Deployer { get; set; }

        public string SessionId { get; private set; }

        #endregion
    }
}