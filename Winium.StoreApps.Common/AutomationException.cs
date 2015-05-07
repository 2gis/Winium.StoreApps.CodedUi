namespace Winium.StoreApps.Common
{
    #region

    using System;

    #endregion

    public class AutomationException : Exception
    {
        #region Constructors and Destructors

        public AutomationException()
        {
            this.Status = ResponseStatus.UnknownError;
        }

        public AutomationException(string message, ResponseStatus status)
            : base(message)
        {
            this.Status = status;
        }

        public AutomationException(string message, params object[] args)
            : base(string.Format(message, args))
        {
            this.Status = ResponseStatus.UnknownError;
        }

        public AutomationException(string message, Exception innerException)
            : base(message, innerException)
        {
            this.Status = ResponseStatus.UnknownError;
        }

        #endregion

        #region Public Properties

        public ResponseStatus Status { get; set; }

        #endregion
    }
}