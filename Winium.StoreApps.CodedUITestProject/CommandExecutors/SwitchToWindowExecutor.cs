namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    #region

    using Winium.StoreApps.CodedUITestProject.Annotations;
    using Winium.StoreApps.Common;

    #endregion

    [UsedImplicitly]
    public class SwitchToWindowExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var tileAutomationId = this.ExecutedCommand.Parameters["name"].ToObject<string>();
            
            this.WindowsRegistry.OpenWindow(tileAutomationId);

            return this.JsonResponse(ResponseStatus.Success, tileAutomationId);
        }

        #endregion
    }
}