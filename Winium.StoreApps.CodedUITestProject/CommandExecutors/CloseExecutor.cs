
namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    using Winium.StoreApps.Common;

    public class CloseExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            this.WindowsRegistry.CloseWindow(this.WindowsRegistry.LastOpenedWindow);

            return this.JsonResponse(ResponseStatus.Success, null);
        }
    }
}
