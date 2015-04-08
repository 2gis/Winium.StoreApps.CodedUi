namespace Winium.StoreApps.Driver.CommandExecutors
{
    internal class CloseExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            this.Session.Deployer.Close();

            return this.JsonResponse();
        }

        #endregion
    }
}
