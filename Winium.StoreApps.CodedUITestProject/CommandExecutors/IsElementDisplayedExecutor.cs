namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    using Winium.StoreApps.CodedUITestProject.Annotations;
    using Winium.StoreApps.Common;

    [UsedImplicitly]
    public class IsElementDisplayedExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registredId = this.ExecutedCommand.Parameters["ID"].ToObject<string>();

            var element = this.ElementsRegistry.GetRegistredElement(registredId);

            return this.JsonResponse(ResponseStatus.Success, !element.AutomationElement.Current.IsOffscreen);
        }
    }
}
