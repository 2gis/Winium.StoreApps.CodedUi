using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    #region

    using Winium.StoreApps.CodedUITestProject.Annotations;
    using Winium.StoreApps.Common;

    #endregion

    [UsedImplicitly]
    public class GetElementLocationExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            // TODO Buttons do not have any patterns besides Invoke
            var registredId = this.ExecutedCommand.Parameters["ID"].ToObject<string>();

            var element = this.ElementsRegistry.GetRegistredElement(registredId);
            var point = element.GetClickablePoint();
            var response = new JObject
            {
                ["x"] = point.X,
                ["y"] = point.Y
            };
            return this.JsonResponse(ResponseStatus.Success, response);
        }

        #endregion
    }
}