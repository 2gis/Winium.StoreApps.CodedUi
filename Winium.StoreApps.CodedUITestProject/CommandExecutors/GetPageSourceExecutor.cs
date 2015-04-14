namespace CodedUITestProject1.CommandExecutors
{
    #region

    using System;
    using System.Text;
    using System.Windows.Automation;

    #endregion

    public class GetPageSourceExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var sb = new StringBuilder();
            try
            {
                var elements = AutomationElement.RootElement.FindAll(TreeScope.Descendants, Condition.TrueCondition);

                foreach (AutomationElement element in elements)
                {
                    sb.AppendFormat(
                        "{0}: <{1}> #{2}\n", 
                        element.Current.Name, 
                        element.Current.ClassName, 
                        element.Current.AutomationId);
                }

                return sb.ToString();
            }
            catch (Exception e)
            {
                return string.Format("{0}\n\n-------------{1}", sb, e);
            }
        }

        #endregion
    }
}