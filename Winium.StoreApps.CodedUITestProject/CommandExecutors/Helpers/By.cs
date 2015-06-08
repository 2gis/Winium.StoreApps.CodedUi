namespace Winium.StoreApps.CodedUITestProject.CommandExecutors.Helpers
{
    #region

    using System;
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
                this.Predicate = x => x.AutomationElement.Current.ClassName.Equals(value);
                return;
            }

            if (strategy.Equals("id"))
            {
                this.Predicate = x => x.AutomationElement.Current.AutomationId.Equals(value);
                return;
            }

            if (strategy.Equals("name"))
            {
                this.Predicate = x => x.AutomationElement.Current.Name.Equals(value);
                return;
            }

            throw new AutomationException("Unknown By strategy {0}.", strategy);
        }

        #endregion

        #region Public Properties

        public Predicate<WiniumElement> Predicate { get; private set; } 

        #endregion
    }
}
