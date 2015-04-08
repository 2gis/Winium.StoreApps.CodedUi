
namespace CodedUITestProject1.CommandExecutors
{
    using Microsoft.VisualStudio.TestTools.UITesting;

    using Winium.StoreApps.Common;

    public class ClickElementExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registredId = this.ExecutedCommand.Parameters["ID"].ToObject<string>();

            var element = this.ElementsRegistry.GetRegistredElement(registredId);
            Gesture.Tap(element.GetClickablePoint());

            return this.JsonResponse(ResponseStatus.Success, null);
        }
    }
}
