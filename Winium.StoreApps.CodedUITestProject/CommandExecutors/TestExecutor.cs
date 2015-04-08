namespace CodedUITestProject1.CommandExecutors
{
    using System.Text;
    using System.Windows.Automation;

    using CodedUITestProject1.CommandExecutors.Helpers;

    using Winium.StoreApps.Common;

    public class TestExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
//            var c1 = new PropertyCondition(AutomationElement.IsTextPatternAvailableProperty, true);
//            var c2 = new PropertyCondition(AutomationElement.IsValuePatternAvailableProperty, true);
//            var cond = new AndCondition(c1, c2);
//
//            var elements = AutomationElement.RootElement.FindAll(TreeScope.Descendants, cond);
//            var sb = new StringBuilder();
//
//            foreach (AutomationElement element in elements)
//            {
//                sb.AppendFormat("{0}\n", element.)
//            }
//            return this.JsonResponse(ResponseStatus.Success, string.Format("{0}\n---\n{1}", webElement, this.ElementsRegistry));
            return null;
        }
    }
}
