namespace Winium.StoreApps.Driver.Helpers
{
    using Winium.StoreApps.Logging;

    internal static class SessionsManager
    {
        #region Static Fields

        private static Session currentSession;

        #endregion

        #region Constructors and Destructors

        static SessionsManager()
        {
            currentSession = null;
        }

        #endregion

        #region Public Methods and Operators

        public static void CloseSession(string sessionId)
        {
            if (currentSession == null)
            {
                return;
            }

            Logger.Info("Closing current session {0}.", sessionId);
            currentSession.Close();
            currentSession = null;
        }

        public static Session CreateSession()
        {
            // TODO add support for multiple sessions using different devices or emulators
            if (currentSession != null)
            {
                Logger.Warn("Driver does not support multiple sessions!");
                CloseSession(currentSession.SessionId);
            }

            currentSession = new Session("AwesomeSession");

            return currentSession;
        }

        public static Session GetSessionbyId(string sessionId)
        {
            return currentSession;
        }

        #endregion
    }
}