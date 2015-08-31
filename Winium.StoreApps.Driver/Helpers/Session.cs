namespace Winium.StoreApps.Driver.Helpers
{
    using System;

    using Winium.StoreApps.Common;

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

        public void Close()
        {
            if (this.ActualCapabilities.DebugCodedUi)
            {
                return;
            }

            if (this.CommandForwarder == null)
            {
                return;
            }

            try
            {
                var quitCommand = new Command(DriverCommand.Quit);
                this.CommandForwarder.ForwardCommand(quitCommand);
                this.Deployer.Dispose();
            }
            catch (Exception e)
            {
                Logger.Error("Exception occured while trying to close session {0}", e);
            }
        }
    }
}