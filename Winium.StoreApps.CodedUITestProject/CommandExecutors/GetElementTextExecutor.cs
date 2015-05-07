namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    #region

    using Winium.StoreApps.Common;

    #endregion

    public class GetElementTextExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            // TODO Buttons do not have any patterns besides Invoke
            var registredId = this.ExecutedCommand.Parameters["ID"].ToObject<string>();

            var element = this.ElementsRegistry.GetRegistredElement(registredId);

            return this.JsonResponse(ResponseStatus.Success, element.GetText());
        }

        #endregion
    }
}