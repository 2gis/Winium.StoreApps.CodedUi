using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    #region

    using Winium.StoreApps.CodedUITestProject.Annotations;
    using Winium.StoreApps.Common;

    #endregion

    [UsedImplicitly]
    public class GetElementSizeExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registredId = this.ExecutedCommand.Parameters["ID"].ToObject<string>();

            var element = this.ElementsRegistry.GetRegistredElement(registredId);
            var rect = element.AutomationElement.Current.BoundingRectangle;
            var response = new JObject()
            {
                ["width"] = rect.Width,
                ["height"] = rect.Height
            };
            return this.JsonResponse(ResponseStatus.Success, response);
        }

        #endregion
    }
}