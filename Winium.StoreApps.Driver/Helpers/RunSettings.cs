namespace Winium.StoreApps.Driver.Helpers
{
    #region

    using System.Xml;

    #endregion

    public class RunSettings
    {
        #region Constructors and Destructors

        public RunSettings(string deviceName)
        {
            this.DeviceName = deviceName;

            var doc = new XmlDocument();
            var rootNode = (XmlElement)doc.AppendChild(doc.CreateElement("RunSettings"));

            var testNode = (XmlElement)rootNode.AppendChild(doc.CreateElement("MSPhoneTest"));
            testNode.AppendChild(doc.CreateElement("TargetDevice")).InnerText = this.DeviceName;

            this.XmlDoc = doc;
        }

        #endregion

        #region Public Properties

        public string DeviceName { get; private set; }

        public XmlDocument XmlDoc { get; private set; }

        #endregion
    }
}