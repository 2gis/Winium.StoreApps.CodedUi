namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    using System;
    using System.Windows.Automation;

    using Winium.StoreApps.CodedUITestProject.Annotations;
    using Winium.StoreApps.CodedUITestProject.CommandExecutors.Helpers;
    using Winium.StoreApps.Common;

    [UsedImplicitly]
    public class FindElementExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var strategy = this.ExecutedCommand.Parameters["using"].ToObject<string>();
            var value = this.ExecutedCommand.Parameters["value"].ToObject<string>();

            try
            {
                var registredId = this.ElementsRegistry.FindElement(WiniumElement.RootElement, new By(strategy, value));
                var webElement = new JsonWebElementContent(registredId);

                return this.JsonResponse(ResponseStatus.Success, webElement);
            }
            catch (AutomationException e){
                return this.JsonResponse(ResponseStatus.NoSuchElement, "Couldn't find element : " + value);
            }

        }
    }
}
