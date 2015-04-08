namespace CodedUITestProject1.CommandExecutors
{
    using CodedUITestProject1.CommandExecutors.Helpers;

    using Winium.StoreApps.Common;

    public class FindChildElementExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var strategy = this.ExecutedCommand.Parameters["using"].ToObject<string>();
            var value = this.ExecutedCommand.Parameters["value"].ToObject<string>();

            var rootRegistredId = this.ExecutedCommand.Parameters["ID"].ToObject<string>();

            var registredId = this.ElementsRegistry.FindElement(rootRegistredId, new By(strategy, value));

            var webElement = new JsonWebElementContent(registredId);

            return this.JsonResponse(ResponseStatus.Success, webElement);
        }
    }
}
