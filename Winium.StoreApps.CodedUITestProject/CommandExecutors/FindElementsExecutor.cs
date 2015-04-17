namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    using System.Linq;
    using System.Windows.Automation;

    using Winium.StoreApps.CodedUITestProject.CommandExecutors.Helpers;
    using Winium.StoreApps.Common;

    public class FindElementsExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var strategy = this.ExecutedCommand.Parameters["using"].ToObject<string>();
            var value = this.ExecutedCommand.Parameters["value"].ToObject<string>();

            var registredIds = this.ElementsRegistry.FindElements(AutomationElement.RootElement, new By(strategy, value));

            var webElements = registredIds.Select(registredId => new JsonWebElementContent(registredId)).ToList();

            return this.JsonResponse(ResponseStatus.Success, webElements);
        }
    }
}
