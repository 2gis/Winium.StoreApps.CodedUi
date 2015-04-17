namespace Winium.StoreApps.CodedUITestProject.CommandExecutors.Helpers
{
    #region

    using System.Windows.Automation;

    using Winium.StoreApps.Common;

    #endregion

    public class By
    {
        #region Constructors and Destructors

        public By(string strategy, string value)
        {
            if (strategy.Equals("tag name") || strategy.Equals("class name"))
            {
                this.Condition = new PropertyCondition(AutomationElement.ClassNameProperty, value);
                return;
            }

            if (strategy.Equals("id"))
            {
                this.Condition = new PropertyCondition(AutomationElement.AutomationIdProperty, value);
                return;
            }

            if (strategy.Equals("name"))
            {
                this.Condition = new PropertyCondition(AutomationElement.NameProperty, value);
                return;
            }

            throw new AutomationException("Unknown By strategy {0}.", strategy);
        }

        #endregion

        #region Public Properties

        public Condition Condition { get; private set; }

        #endregion
    }
}
