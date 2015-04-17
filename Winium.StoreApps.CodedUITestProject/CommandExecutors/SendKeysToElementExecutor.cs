
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

            SendKeys(element, value);

            return this.JsonResponse(ResponseStatus.Success, null);
        }

        private static void SendKeys(AutomationElement element, string value)
        {
            object patternObj;
            if (!element.TryGetCurrentPattern(ValuePattern.Pattern, out patternObj))
            {
                throw new AutomationException("Element does not support sending keys.");
            }

            var valuePattern = (ValuePattern)patternObj;
            valuePattern.SetValue(value);
        }
    }
}
