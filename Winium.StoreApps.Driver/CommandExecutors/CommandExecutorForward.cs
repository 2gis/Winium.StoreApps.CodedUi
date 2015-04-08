namespace Winium.StoreApps.Driver.CommandExecutors
{
    internal class CommandExecutorForward : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            return this.Session.CommandForwarder.ForwardCommand(this.ExecutedCommand);
        }

        #endregion
    }
}
