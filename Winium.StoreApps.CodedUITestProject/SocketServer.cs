namespace CodedUITestProject1
{
    #region

    using System;
    using System.Globalization;
    using System.Net;
    using System.Threading.Tasks;

    using Windows.Networking.Sockets;
    using Windows.Storage.Streams;

    using Winium.StoreApps.Common;

    #endregion

    public class SocketServer
    {
        #region Fields

        private readonly Func<string, string, CommandResponse> requestHandlerFunc;

        private bool isServerActive;

        private StreamSocketListener listener;

        private int listeningPort;

        #endregion

        #region Constructors and Destructors

        public SocketServer(Func<string, string, CommandResponse> requestHandlerFunc)
        {
            if (requestHandlerFunc == null)
            {
                throw new ArgumentNullException("requestHandlerFunc");
            }

            this.requestHandlerFunc = requestHandlerFunc;
        }

        #endregion

        #region Public Methods and Operators

        public async void Start(int port)
        {
            if (this.isServerActive)
            {
                return;
            }

            this.listeningPort = port;

            this.isServerActive = true;
            this.listener = new StreamSocketListener();
            this.listener.Control.QualityOfService = SocketQualityOfService.Normal;
            this.listener.ConnectionReceived += this.ListenerConnectionReceived;
            await this.listener.BindServiceNameAsync(this.listeningPort.ToString(CultureInfo.InvariantCulture));
        }

        public void Stop()
        {
            if (this.isServerActive)
            {
                this.listener.Dispose();
                this.isServerActive = false;
            }
        }

        #endregion

        #region Methods

        private async void HandleRequest(StreamSocket socket)
        {
            var reader = new DataReader(socket.InputStream) { InputStreamOptions = InputStreamOptions.Partial };
            var writer = new DataWriter(socket.OutputStream) { UnicodeEncoding = UnicodeEncoding.Utf8 };

            var acceptedRequest = new AcceptedRequest();
            await acceptedRequest.AcceptRequest(reader);

            string response;
            try
            {
                var commandRespnose = this.requestHandlerFunc(acceptedRequest.Request, acceptedRequest.Content);
                response = HttpResponseHelper.ResponseString(commandRespnose.HttpStatusCode, commandRespnose.Content);
            }
            catch (NotImplementedException ex)
            {
                response = HttpResponseHelper.ResponseString(HttpStatusCode.NotImplemented, ex.Message);
            }

            writer.WriteString(response);
            await writer.StoreAsync();

            socket.Dispose();
        }

        private async void ListenerConnectionReceived(
            StreamSocketListener sender, 
            StreamSocketListenerConnectionReceivedEventArgs args)
        {
            await Task.Run(() => this.HandleRequest(args.Socket));
        }

        #endregion
    }
}