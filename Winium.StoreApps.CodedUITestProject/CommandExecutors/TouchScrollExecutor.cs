namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    #region

    using System;
    using System.Globalization;

    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
    using Microsoft.VisualStudio.TestTools.UITest.Input;
    using Microsoft.VisualStudio.TestTools.UITesting;

    using Winium.StoreApps.CodedUITestProject.Annotations;
    using Winium.StoreApps.Common;

    #endregion

    [UsedImplicitly]
    public class TouchScrollExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registredId = this.ExecutedCommand.Parameters["element"].ToObject<string>();
            var element = this.ElementsRegistry.GetRegistredElement(registredId);
            var xOffset = Convert.ToInt32(this.ExecutedCommand.Parameters["xoffset"].ToObject<string>(), CultureInfo.InvariantCulture);
            var yOffset = Convert.ToInt32(this.ExecutedCommand.Parameters["yoffset"].ToObject<string>(), CultureInfo.InvariantCulture);
            var start = element.GetClickablePoint();
            var end = new Point(start.X + xOffset, start.Y + yOffset);

            Gesture.Slide(start, end);

            return this.JsonResponse(ResponseStatus.Success, null);
        }
        #endregion
    }
}