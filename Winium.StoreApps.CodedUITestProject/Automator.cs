namespace CodedUITestProject1
{
    #region

    using System;
    using System.Net;

    using CodedUITestProject1.CommandExecutors;

    using Microsoft.VisualBasic.CompilerServices;

    using Newtonsoft.Json;

    using Winium.StoreApps.Common;

    #endregion

    public class Automator
    {
        #region Static Fields

        public static readonly Automator Instance = new Automator();

        #endregion

        #region Fields

        private readonly SocketServer socketServer;

        private readonly ElementsRegistry elementsRegistry;

        #endregion

        #region Constructors and Destructors

        public Automator()
        {
            this.socketServer = new SocketServer(this.RequestHandler);
            this.elementsRegistry = new ElementsRegistry();
            this.Session = "AwesomeSession";
        }

        #endregion

        #region Public Properties

        public string Session { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Start()
        {
            this.socketServer.Start(9998);
        }

        public void Stop()
        {
            this.socketServer.Stop();
        }

        #endregion

        #region Methods

        private CommandResponse RequestHandler(string uri, string content)
        {
            var command = JsonConvert.DeserializeObject<Command>(content);

            CommandExecutorBase executor = null;

            // TODO replace with command dispatcher idctionary (with some reflection magic)
            if (command.Name.Equals(DriverCommand.GetPageSource))
            {
                executor = new GetPageSourceExecutor();
            }
            else if (command.Name.Equals(DriverCommand.FindElement))
            {
                executor = new FindElementExecutor();
            }
            else if (command.Name.Equals(DriverCommand.FindChildElement))
            {
                executor = new FindChildElementExecutor();
            }
            else if (command.Name.Equals(DriverCommand.GetElementText))
            {
                executor = new GetElementTextExecutor();
            }
            else if (command.Name.Equals(DriverCommand.ClickElement))
            {
                executor = new ClickElementExecutor();
            }
            else if (command.Name.Equals("getSupportedAutomation"))
            {
                executor = new GetSupportedAutomationExecutor();
            }

            if (executor != null)
            {
                executor.Session = this.Session;
                executor.ExecutedCommand = command;
                executor.ElementsRegistry = this.elementsRegistry;
                return executor.Do();
            }

            return CommandResponse.Create(HttpStatusCode.NotImplemented, "Oh no!");
        }

        #endregion
    }
}
