namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    #region

    using System.Windows.Automation;

    #endregion

    public class GetPageSourceExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var x = new WiniumElement(AutomationElement.RootElement);
            return x.AsXml().ToString();
        }

        #endregion
    }
}