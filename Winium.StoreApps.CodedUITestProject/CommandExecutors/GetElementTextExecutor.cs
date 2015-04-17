namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    using System.Windows.Automation;

    using Winium.StoreApps.Common;

    public class GetElementTextExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            // TODO Buttons do not have any patterns besides Invoke
            var registredId = this.ExecutedCommand.Parameters["ID"].ToObject<string>();


            var element = this.ElementsRegistry.GetRegistredElement(registredId);



            return this.JsonResponse(ResponseStatus.Success, GetText(element));
        }


        private static string GetText(AutomationElement element)
        {
            object patternObj;
            if (element.TryGetCurrentPattern(ValuePattern.Pattern, out patternObj))
            {
                var valuePattern = (ValuePattern)patternObj;
                return valuePattern.Current.Value;
            }
            
            if (element.TryGetCurrentPattern(TextPattern.Pattern, out patternObj))
            {
                var textPattern = (TextPattern)patternObj;
                return textPattern.DocumentRange.GetText(-1);
            }

            return element.Current.Name;
        }
    }
}
