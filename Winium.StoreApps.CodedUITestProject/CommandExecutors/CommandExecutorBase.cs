namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    #region

    using System;
    using System.Net;

    using CodedUITestProject1;

    using Newtonsoft.Json;

    using Winium.StoreApps.Common;

    #endregion

    public class CommandExecutorBase
    {
        #region Public Properties

        public ElementsRegistry ElementsRegistry { get; set; }

        public Command ExecutedCommand { get; set; }

        public string Session { get; set; }

        #endregion

        #region Public Methods and Operators

        public CommandResponse Do()
        {
            try
            {
                return CommandResponse.Create(HttpStatusCode.OK, this.DoImpl());
            }
            catch (AutomationException exception)
            {
                return CommandResponse.Create(HttpStatusCode.OK, this.JsonResponse(exception.Status, exception.Message));
            }
            catch (NotImplementedException exception)
            {
                return CommandResponse.Create(
                    HttpStatusCode.NotImplemented, 
                    this.JsonResponse(ResponseStatus.UnknownCommand, exception.Message));
            }
            catch (Exception exception)
            {
                return CommandResponse.Create(HttpStatusCode.BadRequest, exception.ToString());
            }
        }

        #endregion

        #region Methods

        protected virtual string DoImpl()
        {
            throw new InvalidOperationException("DoImpl should never be called in CommandExecutorBase");
        }

        protected string JsonResponse()
        {
            return this.JsonResponse(ResponseStatus.Success, null);
        }

        protected string JsonResponse(ResponseStatus status, object value)
        {
            return JsonConvert.SerializeObject(new JsonResponse(this.Session, status, value));
        }

        #endregion
    }
}