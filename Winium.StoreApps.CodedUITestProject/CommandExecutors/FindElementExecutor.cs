namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    using System.Windows.Automation;

    using Winium.StoreApps.CodedUITestProject.CommandExecutors.Helpers;
    using Winium.StoreApps.Common;

    public class FindElementExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var strategy = this.ExecutedCommand.Parameters["using"].ToObject<string>();
            var value = this.ExecutedCommand.Parameters["value"].ToObject<string>();

            var registredId = this.ElementsRegistry.FindElement(AutomationElement.RootElement, new By(strategy, value));

            var webElement = new JsonWebElementContent(registredId);

            return this.JsonResponse(ResponseStatus.Success, webElement);
        }
    }
}
