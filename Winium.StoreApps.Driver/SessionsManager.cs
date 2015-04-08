namespace Winium.StoreApps.Driver
{
    #region

    using System.Collections.Generic;

    using Winium.StoreApps.Driver.SessionHelpers;

    #endregion

    internal static class SessionsManager
    {
        #region Static Fields

        private static readonly Dictionary<string, Session> Sessions;

        #endregion

        #region Constructors and Destructors

        static SessionsManager()
        {
            Sessions = new Dictionary<string, Session>();
            Sessions["AwesomeSession"] = new Session("AwesomeSession");
        }

        #endregion

        #region Public Methods and Operators

        public static void CloseSession(string sessionId)
        {
            var session = GetSessionbyId(sessionId);
            if (null != session)
            {
                // sessions.Remove(sessionId);
            }
        }

        public static Session CreateSession()
        {
            // TODO add support for multiply sessions using different devices or emulators
            return Sessions["AwesomeSession"];
        }

        public static Session GetSessionbyId(string sessionId)
        {
            return Sessions["AwesomeSession"];
        }

        #endregion
    }
}
