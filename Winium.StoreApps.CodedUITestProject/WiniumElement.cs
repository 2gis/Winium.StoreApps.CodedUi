namespace Winium.StoreApps.CodedUITestProject
{
    #region

    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Automation;
    using System.Xml.Linq;

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
                from x in this.GetChildrens() select x.AsXml());
        }

        public void Click()
        {
            Gesture.Tap(this.element.GetClickablePoint());
        }

        public string GetAttribute(string attributeName)
        {
            // TODO Add actual support for different attributes
            return this.element.Current.AutomationId;
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

        #region Methods

        private IEnumerable<WiniumElement> GetChildrens()
        {
            return
                from AutomationElement x in
                    this.AutomationElement.FindAll(TreeScope.Children, Condition.TrueCondition)
                select new WiniumElement(x);
        }

        #endregion
    }
}