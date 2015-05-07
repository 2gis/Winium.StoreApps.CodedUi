namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    #region

    using System.Linq;

    using Winium.StoreApps.Common;

    #endregion

    public class SendKeysToElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registredId = this.ExecutedCommand.Parameters["ID"].ToObject<string>();
            var keysArray = this.ExecutedCommand.Parameters["value"].ToObject<string[]>();
            var value = keysArray.Aggregate((aggregated, next) => aggregated + next);

            var element = this.ElementsRegistry.GetRegistredElement(registredId);

            element.SendKeys(value);

            return this.JsonResponse(ResponseStatus.Success, null);
        }

        #endregion
    }
}