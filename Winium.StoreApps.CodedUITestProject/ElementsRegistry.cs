namespace Winium.StoreApps.CodedUITestProject
{
    #region

    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows.Automation;

    using Winium.StoreApps.CodedUITestProject.CommandExecutors.Helpers;
    using Winium.StoreApps.Common;

    #endregion

    public class ElementsRegistry
    {
        #region Static Fields

        private static int safeInstanceCount;

        #endregion

        #region Fields

        // TODO use WeakReference?
        private readonly Dictionary<string, AutomationElement> registeredElements;

        #endregion

        #region Constructors and Destructors

        public ElementsRegistry()
        {
            this.registeredElements = new Dictionary<string, AutomationElement>();
        }

        #endregion

        #region Public Methods and Operators

        public string FindElement(AutomationElement root, By strategy)
        {
            var element = root.FindFirst(TreeScope.Descendants, strategy.Condition);

            if (element == null)
            {
                throw new AutomationException("Element could not be found.", ResponseStatus.NoSuchElement);
            }

            return this.RegisterElement(element);
        }

        public string FindElement(string rootRegisteredId, By strategy)
        {
            var parent = this.GetRegistredElement(rootRegisteredId);

            return this.FindElement(parent, strategy);
        }

        public List<string> FindElements(AutomationElement root, By strategy)
        {
            var elements = root.FindAll(TreeScope.Descendants, strategy.Condition);

            var registred =
                (from AutomationElement automationElement in elements select this.RegisterElement(automationElement))
                    .ToList();

            return registred;
        }

        public List<string> FindElements(string rootRegisteredId, By strategy)
        {
            var parent = this.GetRegistredElement(rootRegisteredId);

            return this.FindElements(parent, strategy);
        }

        public AutomationElement GetRegistredElement(string registeredId)
        {
            AutomationElement item;
            if (this.registeredElements.TryGetValue(registeredId, out item))
            {
                // TODO replace with checking for stalness of the element.
                // Or catch ElementNotAvailableException in Executor Do and rethrough as StaleElement
                if (item != null)
                {
                    return item;
                }
            }

            throw new AutomationException("Stale element reference", ResponseStatus.StaleElementReference);
        }

        public string RegisterElement(AutomationElement element)
        {
            var registeredKey = this.registeredElements.FirstOrDefault(x => x.Value.Equals(element)).Key;

            if (registeredKey != null)
            {
                return registeredKey;
            }

            Interlocked.Increment(ref safeInstanceCount);

            registeredKey = string.Format(
                "{0}-{1}", 
                element.GetHashCode(), 
                safeInstanceCount.ToString(string.Empty, CultureInfo.InvariantCulture));
            this.registeredElements.Add(registeredKey, element);

            return registeredKey;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var pair in this.registeredElements)
            {
                sb.AppendFormat("{0} -> [{1}]\n", pair.Key, pair.Value);
            }

            return sb.ToString();
        }

        #endregion
    }
}
