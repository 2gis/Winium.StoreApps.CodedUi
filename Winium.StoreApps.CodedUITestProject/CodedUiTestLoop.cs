namespace Winium.StoreApps.CodedUITestProject
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
    using Microsoft.VisualStudio.TestTools.UITesting;

    [CodedUITest]
    public class CodedUiTestLoop
    {
        public CodedUiTestLoop()
        {
        }

        [TestMethod]
        public void CodedUiTestMethod1()
        {
            Automator.Instance.Start();
            while (true)
            {
                Task.Delay(TimeSpan.FromMilliseconds(50)).Wait();
            }
        }
    }
}



