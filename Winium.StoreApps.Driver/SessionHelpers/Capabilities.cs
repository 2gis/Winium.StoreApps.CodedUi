namespace Winium.StoreApps.Driver.SessionHelpers
{
    #region

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    #endregion

    internal class Capabilities
    {
        #region Constructors and Destructors

        internal Capabilities()
        {
            this.App = string.Empty;
            this.DeviceName = string.Empty;
            this.InnerPort = 9998;
            this.TakesScreenshot = true;
            this.DebugCodedUi = false;
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
        public string App { get; set; }

        [JsonProperty("deviceName")]
        public string DeviceName { get; set; }

        [JsonProperty("deviceIpAddress")]
        public string DeviceIpAddress { get; set; }

        [JsonProperty("innerPort")]
        public int InnerPort { get; set; }

        [JsonProperty("takesScreenshot")]
        public bool TakesScreenshot { get; set; }

        [JsonProperty("debugCodedUI")]
        public bool DebugCodedUi { get; set; }

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

        public string CapabilitiesToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }

        #endregion
    }
}
