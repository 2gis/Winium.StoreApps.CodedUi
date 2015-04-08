namespace Winium.StoreApps.Driver
{
    #region

    using System;

    #endregion

    internal class Program
    {
        #region Methods

        [STAThread]
        private static void Main(string[] args)
        {
            var listeningPort = 9999;

            Logger.TargetConsole(true);

            try
            {
                var listener = new Listener(listeningPort);
                Listener.UrnPrefix = string.Empty;

                Console.WriteLine("Starting Winium.StoreApps Driver on port {0}\n", listeningPort);

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
