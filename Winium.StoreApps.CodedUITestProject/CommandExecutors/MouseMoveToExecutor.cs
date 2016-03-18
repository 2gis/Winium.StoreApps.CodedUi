namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    #region

    using System;
    using System.Globalization;

    using Microsoft.VisualStudio.TestTools.UITest.Input;
    using Microsoft.VisualStudio.TestTools.UITesting;

    using Winium.StoreApps.CodedUITestProject.Annotations;
    using Winium.StoreApps.Common;

    #endregion

    [UsedImplicitly]
    public class MouseMoveToExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registredId = this.ExecutedCommand.Parameters["element"].ToObject<string>();
            var element = this.ElementsRegistry.GetRegistredElement(registredId);
            var xOffset = Convert.ToInt32(this.ExecutedCommand.Parameters["xoffset"].ToObject<string>(), CultureInfo.InvariantCulture);
            var yOffset = Convert.ToInt32(this.ExecutedCommand.Parameters["yoffset"].ToObject<string>(), CultureInfo.InvariantCulture);
            var rect = element.AutomationElement.Current.BoundingRectangle;
            this.MousePosition.Position = new Point(rect.X + xOffset, rect.Y + yOffset);

            return this.JsonResponse(ResponseStatus.Success, null);
        }
        #endregion
    }
}