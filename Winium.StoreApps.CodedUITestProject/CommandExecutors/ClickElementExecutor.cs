
namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    using Winium.StoreApps.Common;

    public class ClickElementExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registredId = this.ExecutedCommand.Parameters["ID"].ToObject<string>();

            var element = this.ElementsRegistry.GetRegistredElement(registredId);
            element.Click();

            return this.JsonResponse(ResponseStatus.Success, null);
        }
    }
}
