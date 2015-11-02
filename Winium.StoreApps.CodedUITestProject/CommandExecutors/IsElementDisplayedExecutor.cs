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

            // TODO: Investigate if this is a good approach, or if ther are corner cases that needs to be addressed
            return this.JsonResponse(ResponseStatus.Success, !element.AutomationElement.Current.IsOffscreen);
        }
    }
}
