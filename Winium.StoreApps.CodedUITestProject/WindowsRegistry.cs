namespace Winium.StoreApps.CodedUITestProject
{
    #region

    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting.WindowsRuntimeControls;

    using Winium.StoreApps.Common;

    #endregion

    public class WindowsRegistry
    {
        #region Fields

        private Dictionary<string, XamlWindow> registry;

        #endregion

        #region Constructors and Destructors

        public WindowsRegistry()
        {
            this.registry = new Dictionary<string, XamlWindow>();
            this.LastOpenedWindow = null;
        }

        #endregion

        #region Public Properties

        public string LastOpenedWindow { get; private set; }

        #endregion

        #region Public Methods and Operators

        public void CloseWindow(string tileAutomationId)
        {
            XamlWindow window;
            if (this.registry.TryGetValue(tileAutomationId, out window))
            {
                this.registry.Remove(tileAutomationId);
                window.Close();
            }
            else
            {
                throw new AutomationException(
                    string.Format(
                        "Colud not close {0}. Only windows opened with switchToWindow can be closed.", 
                        tileAutomationId), 
                    ResponseStatus.NoSuchWindow);
            }
        }

        public void OpenWindow(string tileAutomationId)
        {
            try
            {
                this.registry.Add(tileAutomationId, XamlWindow.Launch(tileAutomationId));
                this.LastOpenedWindow = tileAutomationId;
            }
            catch (PlaybackFailureException e)
            {
                throw new AutomationException(e.Message, ResponseStatus.NoSuchWindow);
            }
        }

        #endregion
    }
}