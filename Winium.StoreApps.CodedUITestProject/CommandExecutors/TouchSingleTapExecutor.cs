namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    #region

    using Winium.StoreApps.CodedUITestProject.Annotations;
    using Winium.StoreApps.Common;

    #endregion

    [UsedImplicitly]
    public class TouchSingleTapExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registredId = this.ExecutedCommand.Parameters["element"].ToObject<string>();

            var element = this.ElementsRegistry.GetRegistredElement(registredId);
            element.Click();

            return this.JsonResponse(ResponseStatus.Success, null);
        }

        #endregion
    }
}