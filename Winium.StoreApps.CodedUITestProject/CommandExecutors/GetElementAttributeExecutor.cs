namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    using Newtonsoft.Json.Linq;

    using Winium.StoreApps.Common;

    public class GetElementAttributeExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registredId = this.ExecutedCommand.Parameters["ID"].ToObject<string>();

            JToken value;
            string attributeName = null;
            if (this.ExecutedCommand.Parameters.TryGetValue("NAME", out value))
            {
                attributeName = value.ToString();
            }

            if (attributeName == null)
            {
                return this.JsonResponse();
            }

            var element = this.ElementsRegistry.GetRegistredElement(registredId);
            
            return this.JsonResponse(ResponseStatus.Success, element.GetAttribute(attributeName));
        }
    }
}
