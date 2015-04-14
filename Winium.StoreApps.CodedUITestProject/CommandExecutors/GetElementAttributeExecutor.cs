namespace CodedUITestProject1.CommandExecutors
{
    using System.Windows.Automation;

    using Winium.StoreApps.Common;

    public class GetElementAttributeExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registredId = this.ExecutedCommand.Parameters["ID"].ToObject<string>();
            //var attributeName = this.ExecutedCommand.Parameters[""]

            var element = this.ElementsRegistry.GetRegistredElement(registredId);
            
            return this.JsonResponse(ResponseStatus.Success, GetAttribute(element, null));
        }


        private static string GetAttribute(AutomationElement element, string attributeName)
        {
            // TODO Add actuall support for different attributes
            return element.Current.AutomationId;
        }
    }
}
