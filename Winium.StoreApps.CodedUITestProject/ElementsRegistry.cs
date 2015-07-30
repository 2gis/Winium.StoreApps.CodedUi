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

        private readonly Dictionary<string, WiniumElement> registeredElements;

        #endregion

        #region Constructors and Destructors

        public ElementsRegistry()
        {
            this.registeredElements = new Dictionary<string, WiniumElement>();
        }

        #endregion

        #region Public Methods and Operators

        public string FindElement(WiniumElement root, By strategy)
        {
            var element = root.IterFind(TreeScope.Descendants, strategy.Predicate).FirstOrDefault();

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

        public List<string> FindElements(WiniumElement root, By strategy)
        {
            var elements = root.IterFind(TreeScope.Descendants, strategy.Predicate);

            return elements.Select(this.RegisterElement).ToList();
        }

        public List<string> FindElements(string rootRegisteredId, By strategy)
        {
            var parent = this.GetRegistredElement(rootRegisteredId);

            return this.FindElements(parent, strategy);
        }

        public WiniumElement GetRegistredElement(string registeredId)
        {
            WiniumElement item;
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

        public string RegisterElement(WiniumElement element)
        {
            Interlocked.Increment(ref safeInstanceCount);

            var registeredKey = string.Format(CultureInfo.InvariantCulture,
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
                sb.AppendFormat(CultureInfo.InvariantCulture, "{0} -> [{1}]\n", pair.Key, pair.Value);
            }

            return sb.ToString();
        }

        #endregion
    }
}
