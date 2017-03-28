namespace Winium.StoreApps.CodedUITestProject
{
    #region

    using System;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
    using Microsoft.VisualStudio.TestTools.UITesting;

    #endregion

    [CodedUITest]
    public class CodedUiTestLoop
    {
        #region Public Methods and Operators

        [TestMethod]
        public void CodedUiTestMethod1()
        {
            try
            {
                Logger.LogMessage("Starting automation server");
                Automator.Instance.Start();
                while (Automator.Instance.Running)
                {
                    Task.Delay(TimeSpan.FromMilliseconds(50)).Wait();
                }
                Logger.LogMessage("Stopping automation server");
                Automator.Instance.Stop();
            }
            catch (Exception ex)
            {
                Logger.LogMessage("Unhandled exception: {0}", ex.ToString());
                throw;
            }
        }

        #endregion
    }
}