namespace Winium.StoreApps.Driver
{
    #region

    using System;
    using System.Globalization;
    using System.Threading;

    #endregion

    internal class Program
    {
        #region Methods

        [STAThread]
        private static void Main(string[] args)
        {
            const int ListeningPort = 9999;

            Logger.TargetConsole(true);

            try
            {
                var listener = new Listener(ListeningPort);
                Listener.UrnPrefix = string.Empty;

                Console.WriteLine("Starting Winium.StoreApps Driver on port {0}\n", ListeningPort);

                listener.StartListening();
            }
            catch (Exception ex)
            {
                Logger.Fatal("Failed to start driver: {0}", ex);
                throw;
            }
        }

        #endregion
    }
}
