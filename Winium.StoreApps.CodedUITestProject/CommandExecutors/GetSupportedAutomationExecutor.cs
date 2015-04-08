namespace CodedUITestProject1.CommandExecutors
{
    using System.Text;
    using System.Windows.Automation;

    using Winium.StoreApps.Common;

    public class GetSupportedAutomationExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            // TODO Buttons do not have any patterns besides Invoke
            var registredId = this.ExecutedCommand.Parameters["ID"].ToObject<string>();


            var element = this.ElementsRegistry.GetRegistredElement(registredId);



            return this.JsonResponse(ResponseStatus.Success, GetPatterns(element));
        }


        private static string GetPatterns(AutomationElement element)
        {
            var properties = element.GetSupportedProperties();

            var msg = new StringBuilder();

            msg.Append("Supported Properties:\n");
            foreach (var automationProperty in properties)
            {
                var value = element.GetCurrentPropertyValue(automationProperty);
                msg.AppendFormat("{0}: {1}\n", automationProperty.ProgrammaticName, value);
            }

            return msg.ToString();
        }
    }
}
