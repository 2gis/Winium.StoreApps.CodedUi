namespace Winium.StoreApps.CodedUITestProject
{
    #region

    using System;
    using System.Net;
    using System.Reflection;

    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

    using Newtonsoft.Json;

    using Winium.StoreApps.CodedUITestProject.CommandExecutors;
    using Winium.StoreApps.Common;

    #endregion

    public class Automator
    {
        #region Static Fields

        public static readonly Automator Instance = new Automator();

        #endregion

        #region Fields

        private readonly ExecutorsDispatcher commandsExecutorsDispatcher;

        private readonly ElementsRegistry elementsRegistry;

        private readonly SocketServer socketServer;

        private readonly WindowsRegistry windowsesRegistry;

        #endregion

        #region Constructors and Destructors

        private Automator()
        {
            this.socketServer = new SocketServer(this.RequestHandler);

            this.elementsRegistry = new ElementsRegistry();
            this.windowsesRegistry = new WindowsRegistry();
            this.Session = "AwesomeSession";

            this.commandsExecutorsDispatcher =
                new ExecutorsDispatcher(typeof(CommandExecutorBase).GetTypeInfo().Assembly, typeof(CommandExecutorBase));
        }

        #endregion

        #region Public Properties

        private string Session { get; set; }

        public bool Running
        {
            get
            {
                return this.socketServer.IsServiceActive;
            } 
        }

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

            if (command.Name.Equals(DriverCommand.Quit))
            {
                this.socketServer.ShouldStopAfterResponse = true;
                return CommandResponse.Create(HttpStatusCode.OK, null);
            }

            var executor = this.commandsExecutorsDispatcher.GetExecutor<CommandExecutorBase>(command.Name);

            if (executor == null)
            {
                return CommandResponse.Create(
                    HttpStatusCode.NotImplemented, 
                    string.Format("Command '{0}' not yet implemented.", command.Name));
            }

            Logger.LogMessage("Executing command: {0}", command.Name);

            executor.Session = this.Session;
            executor.ExecutedCommand = command;
            executor.ElementsRegistry = this.elementsRegistry;
            executor.WindowsRegistry = this.windowsesRegistry;
            return executor.Do();
        }

        #endregion
    }
}