
namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    using System.Linq;
    using System.Windows.Automation;

    using Winium.StoreApps.Common;

    public class SendKeysToElementExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registredId = this.ExecutedCommand.Parameters["ID"].ToObject<string>();
            var keysArray = this.ExecutedCommand.Parameters["value"].ToObject<string[]>();
            var value = keysArray.Aggregate((aggregated, next) => aggregated + next);

            var element = this.ElementsRegistry.GetRegistredElement(registredId);

            element.SendKeys(value);

            return this.JsonResponse(ResponseStatus.Success, null);
        }
    }
}
