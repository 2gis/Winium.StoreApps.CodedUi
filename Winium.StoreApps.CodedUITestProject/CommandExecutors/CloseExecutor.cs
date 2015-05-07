namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    #region

    using Winium.StoreApps.Common;

    #endregion

    public class CloseExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            this.WindowsRegistry.CloseWindow(this.WindowsRegistry.LastOpenedWindow);

            return this.JsonResponse(ResponseStatus.Success, null);
        }

        #endregion
    }
}