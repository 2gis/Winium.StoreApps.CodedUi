namespace Winium.StoreApps.Common
{
    /// <summary>
    /// The driver command.
    /// </summary>
    public static class DriverCommand
    {
        #region Static Fields

        /// <summary>
        /// The accept alert.
        /// </summary>
        public static readonly string AcceptAlert = "acceptAlert";

        /// <summary>
        /// The add cookie.
        /// </summary>
        public static readonly string AddCookie = "addCookie";

        /// <summary>
        /// The clear element.
        /// </summary>
        public static readonly string ClearElement = "clearElement";

        /// <summary>
        /// The click element.
        /// </summary>
        public static readonly string ClickElement = "clickElement";

        /// <summary>
        /// The close.
        /// </summary>
        public static readonly string Close = "close";

        /// <summary>
        /// The define driver mapping.
        /// </summary>
        public static readonly string DefineDriverMapping = "defineDriverMapping";

        /// <summary>
        /// The delete all cookies.
        /// </summary>
        public static readonly string DeleteAllCookies = "deleteAllCookies";

        /// <summary>
        /// The delete cookie.
        /// </summary>
        public static readonly string DeleteCookie = "deleteCookie";

        /// <summary>
        /// The describe element.
        /// </summary>
        public static readonly string DescribeElement = "describeElement";

        /// <summary>
        /// The dismiss alert.
        /// </summary>
        public static readonly string DismissAlert = "dismissAlert";

        /// <summary>
        /// The element equals.
        /// </summary>
        public static readonly string ElementEquals = "elementEquals";

        /// <summary>
        /// The execute async script.
        /// </summary>
        public static readonly string ExecuteAsyncScript = "executeAsyncScript";

        /// <summary>
        /// The execute script.
        /// </summary>
        public static readonly string ExecuteScript = "executeScript";

        /// <summary>
        /// The find child element.
        /// </summary>
        public static readonly string FindChildElement = "findChildElement";

        /// <summary>
        /// The find child elements.
        /// </summary>
        public static readonly string FindChildElements = "findChildElements";

        /// <summary>
        /// The find element.
        /// </summary>
        public static readonly string FindElement = "findElement";

        /// <summary>
        /// The find elements.
        /// </summary>
        public static readonly string FindElements = "findElements";

        /// <summary>
        /// The get.
        /// </summary>
        public static readonly string Get = "get";

        /// <summary>
        /// The get active element.
        /// </summary>
        public static readonly string GetActiveElement = "getActiveElement";

        /// <summary>
        /// The get alert text.
        /// </summary>
        public static readonly string GetAlertText = "getAlertText";

        /// <summary>
        /// The get all cookies.
        /// </summary>
        public static readonly string GetAllCookies = "getCookies";

        /// <summary>
        /// The get current url.
        /// </summary>
        public static readonly string GetCurrentUrl = "getCurrentUrl";

        /// <summary>
        /// The get current window handle.
        /// </summary>
        public static readonly string GetCurrentWindowHandle = "getCurrentWindowHandle";

        /// <summary>
        /// The get element attribute.
        /// </summary>
        public static readonly string GetElementAttribute = "getElementAttribute";

        /// <summary>
        /// The get element location.
        /// </summary>
        public static readonly string GetElementLocation = "getElementLocation";

        /// <summary>
        /// The get element location once scrolled into view.
        /// </summary>
        public static readonly string GetElementLocationOnceScrolledIntoView = "getElementLocationOnceScrolledIntoView";

        /// <summary>
        /// The get element size.
        /// </summary>
        public static readonly string GetElementSize = "getElementSize";

        /// <summary>
        /// The get element tag name.
        /// </summary>
        public static readonly string GetElementTagName = "getElementTagName";

        /// <summary>
        /// The get element text.
        /// </summary>
        public static readonly string GetElementText = "getElementText";

        /// <summary>
        /// The get element value of css property.
        /// </summary>
        public static readonly string GetElementValueOfCssProperty = "getElementValueOfCssProperty";

        /// <summary>
        /// The get orientation.
        /// </summary>
        public static readonly string GetOrientation = "getOrientation";

        /// <summary>
        /// The get page source.
        /// </summary>
        public static readonly string GetPageSource = "getPageSource";

        /// <summary>
        /// The get session capabilities.
        /// </summary>
        public static readonly string GetSessionCapabilities = "getSessionCapabilities";

        /// <summary>
        /// The get session list.
        /// </summary>
        public static readonly string GetSessionList = "getSessionList";

        /// <summary>
        /// The get title.
        /// </summary>
        public static readonly string GetTitle = "getTitle";

        /// <summary>
        /// The get window handles.
        /// </summary>
        public static readonly string GetWindowHandles = "getWindowHandles";

        /// <summary>
        /// The get window position.
        /// </summary>
        public static readonly string GetWindowPosition = "getWindowPosition";

        /// <summary>
        /// The get window size.
        /// </summary>
        public static readonly string GetWindowSize = "getWindowSize";

        /// <summary>
        /// The go back.
        /// </summary>
        public static readonly string GoBack = "goBack";

        /// <summary>
        /// The go forward.
        /// </summary>
        public static readonly string GoForward = "goForward";

        /// <summary>
        /// The implicitly wait.
        /// </summary>
        public static readonly string ImplicitlyWait = "implicitlyWait";

        /// <summary>
        /// The is element displayed.
        /// </summary>
        public static readonly string IsElementDisplayed = "isElementDisplayed";

        /// <summary>
        /// The is element enabled.
        /// </summary>
        public static readonly string IsElementEnabled = "isElementEnabled";

        /// <summary>
        /// The is element selected.
        /// </summary>
        public static readonly string IsElementSelected = "isElementSelected";

        /// <summary>
        /// The maximize window.
        /// </summary>
        public static readonly string MaximizeWindow = "maximizeWindow";

        /// <summary>
        /// The mouse click.
        /// </summary>
        public static readonly string MouseClick = "mouseClick";

        /// <summary>
        /// The mouse double click.
        /// </summary>
        public static readonly string MouseDoubleClick = "mouseDoubleClick";

        /// <summary>
        /// The mouse down.
        /// </summary>
        public static readonly string MouseDown = "mouseDown";

        /// <summary>
        /// The mouse move to.
        /// </summary>
        public static readonly string MouseMoveTo = "mouseMoveTo";

        /// <summary>
        /// The mouse up.
        /// </summary>
        public static readonly string MouseUp = "mouseUp";

        /// <summary>
        /// The new session.
        /// </summary>
        public static readonly string NewSession = "newSession";

        /// <summary>
        /// The quit.
        /// </summary>
        public static readonly string Quit = "quit";

        /// <summary>
        /// The refresh.
        /// </summary>
        public static readonly string Refresh = "refresh";

        /// <summary>
        /// The screenshot.
        /// </summary>
        public static readonly string Screenshot = "screenshot";

        /// <summary>
        /// The send keys to active element.
        /// </summary>
        public static readonly string SendKeysToActiveElement = "sendKeysToActiveElement";

        /// <summary>
        /// The send keys to element.
        /// </summary>
        public static readonly string SendKeysToElement = "sendKeysToElement";

        /// <summary>
        /// The set alert value.
        /// </summary>
        public static readonly string SetAlertValue = "setAlertValue";

        /// <summary>
        /// The set async script timeout.
        /// </summary>
        public static readonly string SetAsyncScriptTimeout = "setScriptTimeout";

        /// <summary>
        /// The set orientation.
        /// </summary>
        public static readonly string SetOrientation = "setOrientation";

        /// <summary>
        /// The set timeout.
        /// </summary>
        public static readonly string SetTimeout = "setTimeout";

        /// <summary>
        /// The set window position.
        /// </summary>
        public static readonly string SetWindowPosition = "setWindowPosition";

        /// <summary>
        /// The set window size.
        /// </summary>
        public static readonly string SetWindowSize = "setWindowSize";

        /// <summary>
        /// The status.
        /// </summary>
        public static readonly string Status = "status";

        /// <summary>
        /// The submit element.
        /// </summary>
        public static readonly string SubmitElement = "submitElement";

        /// <summary>
        /// The switch to frame.
        /// </summary>
        public static readonly string SwitchToFrame = "switchToFrame";

        /// <summary>
        /// The switch to parent frame.
        /// </summary>
        public static readonly string SwitchToParentFrame = "switchToParentFrame";

        /// <summary>
        /// The switch to window.
        /// </summary>
        public static readonly string SwitchToWindow = "switchToWindow";

        /// <summary>
        /// The touch double tap.
        /// </summary>
        public static readonly string TouchDoubleTap = "touchDoubleTap";

        /// <summary>
        /// The touch flick.
        /// </summary>
        public static readonly string TouchFlick = "touchFlick";

        /// <summary>
        /// The touch long press.
        /// </summary>
        public static readonly string TouchLongPress = "touchLongPress";

        /// <summary>
        /// The touch move.
        /// </summary>
        public static readonly string TouchMove = "touchMove";

        /// <summary>
        /// The touch press.
        /// </summary>
        public static readonly string TouchPress = "touchDown";

        /// <summary>
        /// The touch release.
        /// </summary>
        public static readonly string TouchRelease = "touchUp";

        /// <summary>
        /// The touch scroll.
        /// </summary>
        public static readonly string TouchScroll = "touchScroll";

        /// <summary>
        /// The touch single tap.
        /// </summary>
        public static readonly string TouchSingleTap = "touchSingleTap";

        /// <summary>
        /// The upload file.
        /// </summary>
        public static readonly string UploadFile = "uploadFile";

        #endregion
    }
}