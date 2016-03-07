using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Winium.StoreApps.Driver.CommandExecutors
{
    #region

    using System.Collections.Generic;

    using Winium.StoreApps.Common;
    using Winium.StoreApps.Driver.Helpers;

    #endregion

    internal class ExecuteScriptExecutor : CommandExecutorBase
    {
        private const string StorageReadLocalFile = "ReadLocalTextFile";

        #region Methods
        internal object ExecuteStorageScript(string command)
        {
            switch (command)
            {
                case StorageReadLocalFile:
                    var args = ExecutedCommand.Parameters["args"] as JArray;
                    if (args == null || args.Count == 0)
                    {
                        throw new AutomationException("Missing file name", ResponseStatus.UnknownCommand);
                    }

                    var filename = args[0].ToString();
                    var filePath = Path.GetTempFileName();
                    this.Session.Deployer.ReceiveFile("Local", filename, filePath);
                    using (var file = File.OpenText(filePath))
                    {
                        return file.ReadToEnd();
                    }
                default:
                    throw new AutomationException("Unknown storage command: " + command, ResponseStatus.UnknownCommand);
            }
        }

        protected override string DoImpl()
        {
            string command;
            var prefix = string.Empty;

            var script = this.ExecutedCommand.Parameters["script"].ToString();
            var index = script.IndexOf(':');
            if (index == -1)
            {
                command = script;
            }
            else
            {
                ++index;
                prefix = script.Substring(0, index);
                command = script.Substring(index).Trim();
            }

            object response;

            switch (prefix)
            {
                case "storage:":
                    response = this.ExecuteStorageScript(command);
                    break;
                default:
                    return this.Session.CommandForwarder.ForwardCommand(this.ExecutedCommand);
            }

            return this.JsonResponse(ResponseStatus.Success, response);
        }

        #endregion
    }
}
