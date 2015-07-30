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
            Automator.Instance.Start();
            while (Automator.Instance.Running)
            {
                Task.Delay(TimeSpan.FromMilliseconds(50)).Wait();
            }
            Automator.Instance.Stop();
        }

        #endregion
    }
}