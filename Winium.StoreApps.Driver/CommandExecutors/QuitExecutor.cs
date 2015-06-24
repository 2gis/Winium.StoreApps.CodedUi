namespace Winium.StoreApps.Driver.CommandExecutors
{
    using Winium.StoreApps.Driver.Helpers;

    internal class QuitExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            SessionsManager.CloseSession(this.Session.SessionId);

            return this.JsonResponse();
        }

        #endregion
    }
}