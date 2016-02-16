using Windows.Foundation;
using Windows.Foundation.Collections;

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
    public class TouchPinchExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var x1 = Convert.ToInt32(this.ExecutedCommand.Parameters["x1"].ToObject<string>(), CultureInfo.InvariantCulture);
            var y1 = Convert.ToInt32(this.ExecutedCommand.Parameters["y1"].ToObject<string>(), CultureInfo.InvariantCulture);
            var x2 = Convert.ToInt32(this.ExecutedCommand.Parameters["x2"].ToObject<string>(), CultureInfo.InvariantCulture);
            var y2 = Convert.ToInt32(this.ExecutedCommand.Parameters["y2"].ToObject<string>(), CultureInfo.InvariantCulture);
            var distanceToMove = Convert.ToInt32(this.ExecutedCommand.Parameters["distance"].ToObject<string>(), CultureInfo.InvariantCulture);
            var point1 = new Point(x1, y1);
            var point2 = new Point(x2, y2);

            Gesture.Pinch(point1, point2, distanceToMove);

            return this.JsonResponse(ResponseStatus.Success, null);
        }
        #endregion
    }
}