namespace CodedUITestProject1
{
    #region

    using System;
    using System.Globalization;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Automation;

    using Windows.Networking.Sockets;
    using Windows.Storage.Streams;
    using Windows.UI.Xaml;

    using Microsoft.VisualStudio.TestTools.UITesting;

    using Winium.StoreApps.Common;

    using UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding;

    #endregion

    public class SocketServer
    {
        #region Static Fields

        #endregion

        #region Fields


        private bool isServerActive;

        private StreamSocketListener listener;

        private readonly Func<string, string, CommandResponse> requestHandlerFunc;

        private int listeningPort;

        #endregion

        #region Public Methods and Operators

        public SocketServer(Func<string, string, CommandResponse> requestHandlerFunc)
        {
            if (requestHandlerFunc == null)
            {
                throw new ArgumentNullException("requestHandlerFunc");
            }

            this.requestHandlerFunc = requestHandlerFunc;
        }

        public void InitializeAndStart(UIElement visualRoot)
        {
            this.Start(9998);
        }


        public void InitializeAndStart(UIElement visualRoot, int port)
        {
            this.Start(port);
        }

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
