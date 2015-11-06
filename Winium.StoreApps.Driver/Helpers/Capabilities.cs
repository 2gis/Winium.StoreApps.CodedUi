namespace Winium.StoreApps.Driver.Helpers
{
    #region

    using System.Globalization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System.Collections.Generic;

    #endregion

    internal class Capabilities
    {
        #region Constructors and Destructors

        internal Capabilities()
        {
            this.App = string.Empty;
            this.DeviceName = string.Empty;
            this.InnerPort = 9998;
            this.DeviceIpAddress = "localhost";
            this.DebugCodedUi = false;
            this.Locale = CultureInfo.CurrentCulture.Name;
            this.DebugCaptureLogs = false;
            this.Dependencies = new List<string>();
        }

        #endregion

        #region Public Properties

        [JsonProperty("platformName")]
        public static string PlatformName
        {
            get
            {
                return "WindowsPhone";
            }
        }

        [JsonProperty("app")]
        public string App { get; private set; }

        [JsonProperty("deviceName")]
        public string DeviceName { get; private set; }

        [JsonProperty("deviceIpAddress")]
        public string DeviceIpAddress { get; private set; }

        [JsonProperty("innerPort")]
        public int InnerPort { get; private set; }

        [JsonProperty("locale")]
        public string Locale { get; private set; }

        [JsonProperty("debugCodedUI")]
        public bool DebugCodedUi { get; private set; }

        [JsonProperty("debugCaptureLogs")]
        public bool DebugCaptureLogs { get; private set; }

        [JsonProperty("dependencies")]
        public List<string> Dependencies { get; set; }

        #endregion

        #region Public Methods and Operators

        public static Capabilities CapabilitiesFromJsonString(string jsonString)
        {
            var capabilities = JsonConvert.DeserializeObject<Capabilities>(
                jsonString,
                new JsonSerializerSettings
                {
                    Error =
                        delegate(object sender, ErrorEventArgs args)
                        {
                            args.ErrorContext.Handled = true;
                        }
                });

            return capabilities;
        }

        #endregion
    }
}
