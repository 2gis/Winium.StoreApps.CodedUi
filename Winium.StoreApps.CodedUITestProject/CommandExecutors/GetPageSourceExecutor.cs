namespace CodedUITestProject1.CommandExecutors
{
    using System;
    using System.Text;
    using System.Windows.Automation;

    using Microsoft.VisualStudio.TestTools.UITest.Input;

    public class GetPageSourceExecutor : CommandExecutorBase
    {
        private void WalkControlElements(AutomationElement rootElement, StringBuilder builder)
        {
            // Conditions for the basic views of the subtree (content, control, and raw)  
            // are available as fields of TreeWalker, and one of these is used in the  
            // following code.
            AutomationElement elementNode = TreeWalker.ControlViewWalker.GetFirstChild(rootElement);

            while (elementNode != null)
            {
                builder.AppendFormat("class {0}, id {1}, rect {2}\n", elementNode.Current.ClassName, elementNode.Current.AutomationId, elementNode.Current.BoundingRectangle);
                WalkControlElements(elementNode, builder);
                elementNode = TreeWalker.ControlViewWalker.GetNextSibling(elementNode);
            }
        }

        protected override string DoImpl()
        {

            // XamlWindow myAppWindow = XamlWindow.Launch("df4f21bc-8ad2-4e67-ade4-e5dc307ba179_rvx3kg629jaq4!App");
            var sb = new StringBuilder();
            WalkControlElements(AutomationElement.RootElement, sb);
//            try
//            {
//                var elements = AutomationElement.RootElement.FindAll(TreeScope.Descendants, Condition.TrueCondition);
//
//                foreach (AutomationElement element in elements)
//                {
//                    var point = new Point();
//                    try
//                    {
//                        point = element.GetClickablePoint();
//                    }
//                    catch (NoClickablePointException)
//                    {
//                    }
//
//                    sb.AppendFormat(
//                        "{0}: <{1}> {2} [{3}] {4}\n",
//                        element.Current.Name,
//                        element.Current.ClassName,
//                        element.Current.AutomationId,
//                        point,
//                        string.Join("", element.GetRuntimeId()));
//                }
//
//                return sb.ToString();
//            }
//            catch (Exception e)
//            {
//                return string.Format("{0}\n\n-------------{1}", sb, e);
//            }
            return sb.ToString();
        }
    }
}
