namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    #region

    using Winium.StoreApps.CodedUITestProject.Annotations;

    #endregion

    [UsedImplicitly]
    public class PingExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            return "pong";
        }

        #endregion
    }
}