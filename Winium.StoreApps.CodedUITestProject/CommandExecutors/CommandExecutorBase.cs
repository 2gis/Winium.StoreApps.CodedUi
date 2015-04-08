namespace CodedUITestProject1
{
    using System;
    using System.Net;

    using Newtonsoft.Json;

    using Winium.StoreApps.Common;

    public class CommandExecutorBase
    {
        public Command ExecutedCommand { get; set; }
        
        public ElementsRegistry ElementsRegistry { get; set; }

        public string Session { get; set; }

        public CommandResponse Do()
        {
            try
            {
                return CommandResponse.Create(HttpStatusCode.OK, this.DoImpl());
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

    }
}
