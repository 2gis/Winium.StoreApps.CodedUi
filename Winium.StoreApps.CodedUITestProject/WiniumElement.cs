namespace Winium.StoreApps.CodedUITestProject
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;
    using System.Xml.Linq;
    using System.Reflection;

    using Microsoft.VisualStudio.TestTools.UITest.Input;
    using Microsoft.VisualStudio.TestTools.UITesting;

    using Winium.StoreApps.Common;

    #endregion

    public class WiniumElement
    {
        #region Fields

        private readonly AutomationElement element;

        #endregion

        #region Constructors and Destructors

        public WiniumElement(AutomationElement element)
        {
            this.element = element;
        }

        #endregion

        #region Public Properties

        public static WiniumElement RootElement
        {
            get
            {
                return new WiniumElement(AutomationElement.RootElement);
            }
        }

        public AutomationElement AutomationElement
        {
            get
            {
                return this.element;
            }
        }

        public string RuntimeId
        {
            get
            {
                return string.Join(string.Empty, this.AutomationElement.GetRuntimeId());
            }
        }

        #endregion

        #region Public Methods and Operators

        public XStreamingElement AsXml()
        {
            return new XStreamingElement(
                "W" + this.RuntimeId, 
                new XAttribute("class", this.AutomationElement.Current.ClassName), 
                new XAttribute("id", this.AutomationElement.Current.AutomationId), 
                new XAttribute("name", this.AutomationElement.Current.Name), 
                new XAttribute("rect", this.AutomationElement.Current.BoundingRectangle), 
                from x in this.IterFind(TreeScope.Children, null) select x.AsXml());
        }

        public void Click()
        {
            Gesture.Tap(this.GetClickablePoint());
        }

        public string GetAttribute(string attributeName)
        {
            PropertyInfo property = this.AutomationElement.Current.GetType().GetRuntimeProperty(attributeName);
            object value = property.GetValue(this.AutomationElement.Current);

            return value.ToString();
        }

        public Point GetClickablePoint()
        {
            try
            {
                return this.element.GetClickablePoint();
            }
            catch (NoClickablePointException)
            {
                // TODO this is temporary solution for WebView elements, that do not return clickable point even if visible
                // FIXME middle point of BoundingRectangle might be under anither element or off screen, need some checks
                var rect = this.AutomationElement.Current.BoundingRectangle;
                return new Point(rect.Left + (rect.Width / 2), rect.Top + (rect.Height / 2));
            }
        }

        public string GetText()
        {
            object patternObj;
            if (this.element.TryGetCurrentPattern(ValuePattern.Pattern, out patternObj))
            {
                var valuePattern = (ValuePattern)patternObj;
                return valuePattern.Current.Value;
            }

            if (this.element.TryGetCurrentPattern(TextPattern.Pattern, out patternObj))
            {
                var textPattern = (TextPattern)patternObj;
                return textPattern.DocumentRange.GetText(-1);
            }

            return this.element.Current.Name;
        }

        public IEnumerable<WiniumElement> IterFind(TreeScope scope, Predicate<WiniumElement> predicate)
        {
            if (scope != TreeScope.Descendants && scope != TreeScope.Children)
            {
                throw new ArgumentException("scope should be one of TreeScope.Descendants or TreeScope.Children");
            }

            var walker = new TreeWalker(Condition.TrueCondition);

            var elementNode = walker.GetFirstChild(this.AutomationElement);

            while (elementNode != null)
            {
                var winiumElement = new WiniumElement(elementNode);

                if (predicate == null || predicate(winiumElement))
                {
                    yield return winiumElement;
                }

                if (scope == TreeScope.Descendants)
                {
                    foreach (var descendant in winiumElement.IterFind(scope, predicate))
                    {
                        yield return descendant;
                    }
                }

                elementNode = walker.GetNextSibling(elementNode);
            }
        }

        public void SendKeys(string value)
        {
            object patternObj;
            if (!this.element.TryGetCurrentPattern(ValuePattern.Pattern, out patternObj))
            {
                throw new AutomationException("Element does not support sending keys.");
            }

            var valuePattern = (ValuePattern)patternObj;
            valuePattern.SetValue(value);
        }

        #endregion
    }
}