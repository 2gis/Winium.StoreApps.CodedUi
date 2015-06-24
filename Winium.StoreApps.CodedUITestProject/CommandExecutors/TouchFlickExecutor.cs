namespace Winium.StoreApps.CodedUITestProject.CommandExecutors
{
    #region

    using System;
    using System.Globalization;

    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
    using Microsoft.VisualStudio.TestTools.UITest.Input;
    using Microsoft.VisualStudio.TestTools.UITesting;

    using Winium.StoreApps.Common;

    #endregion

    public class TouchFlickExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            FlickParameters flickParameters;

            if (this.ExecutedCommand.Parameters.ContainsKey("element"))
            {
                flickParameters = this.ParseFlickFromElementParameters();
            }
            else
            {
                throw new AutomationException(
                    "Flick without start point is not supported. Please provide lement to flick form.");
            }

            Logger.LogMessage("Flick with: {0}", flickParameters.ToString());

            Gesture.Flick(
                flickParameters.StartPoint, 
                flickParameters.Length, 
                flickParameters.Direction, 
                flickParameters.Duration);

            return this.JsonResponse(ResponseStatus.Success, null);
        }

        private FlickParameters ParseFlickFromElementParameters()
        {
            FlickParameters flickParameters;

            var registredId = this.ExecutedCommand.Parameters["element"].ToObject<string>();
            var element = this.ElementsRegistry.GetRegistredElement(registredId);

            var xOffset = Convert.ToInt32(this.ExecutedCommand.Parameters["xoffset"], CultureInfo.InvariantCulture);
            var yOffset = Convert.ToInt32(this.ExecutedCommand.Parameters["yoffset"], CultureInfo.InvariantCulture);
            var speed = Convert.ToDouble(this.ExecutedCommand.Parameters["speed"], CultureInfo.InvariantCulture);

            flickParameters.StartPoint = element.AutomationElement.GetClickablePoint();
            flickParameters.Length = (uint)Math.Sqrt((xOffset * xOffset) + (yOffset * yOffset));
            flickParameters.Duration = (uint)(1000 * flickParameters.Length / speed);
            flickParameters.Direction = Math.Atan2(yOffset, xOffset) * 180 / Math.PI;

            return flickParameters;
        }

        #endregion

        private struct FlickParameters
        {
            #region Fields

            public double Direction;

            public uint Duration;

            public uint Length;

            public Point StartPoint;

            #endregion

            #region Public Methods and Operators

            public override string ToString()
            {
                return string.Format(
                    CultureInfo.InvariantCulture, 
                    "StartPoint: {0}, Length: {1}px, Duration: {2}ms, Direction: {3}°", 
                    this.StartPoint, 
                    this.Length, 
                    this.Duration, 
                    this.Direction);
            }

            #endregion
        }
    }
}