namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    using System.Linq;

    using Winium.StoreApps.CodedUITestProject.CommandExecutors.Helpers;
    using Winium.StoreApps.Common;

    public class FindChildElementsExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var strategy = this.ExecutedCommand.Parameters["using"].ToObject<string>();
            var value = this.ExecutedCommand.Parameters["value"].ToObject<string>();

            var rootRegistredId = this.ExecutedCommand.Parameters["ID"].ToObject<string>();

            var registredIds = this.ElementsRegistry.FindElements(rootRegistredId, new By(strategy, value));

            var webElements = registredIds.Select(registredId => new JsonWebElementContent(registredId)).ToList();

            return this.JsonResponse(ResponseStatus.Success, webElements);
        }
    }
}
