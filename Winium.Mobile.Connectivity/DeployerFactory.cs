namespace Winium.Mobile.Connectivity
{
    #region

    using System.Globalization;
    using System.IO;

    #endregion

    public static class DeployerFactory
    {
        #region Public Methods and Operators

        public static IDeployer DeployerForPackage(FileInfo package, string desiredDevice, bool strict, CultureInfo cultureInfo = null)
        {
            return new Deployer(desiredDevice, strict, cultureInfo);
        }

        #endregion
    }
}
