namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    #region

    using System.Linq;

    using Winium.StoreApps.CodedUITestProject.Annotations;
    using Winium.StoreApps.Common;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITest.Input;

    #endregion

    [UsedImplicitly]
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

            Gesture.Tap(new Point(0,0));
            return this.JsonResponse(ResponseStatus.Success, null);
        }

        #endregion
    }
}