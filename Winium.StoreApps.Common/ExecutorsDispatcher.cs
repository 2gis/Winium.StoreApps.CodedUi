namespace Winium.StoreApps.Common
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    #endregion

    /// <summary>
    /// Creates instances of command executors based on command name
    /// </summary>
    public class ExecutorsDispatcher
    {
        #region Fields

        private readonly Dictionary<string, Type> executorsDespatchTable;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Mapping between command names and classes that execute corresponding commands.
        /// </summary>
        /// <param name="assembly">Assemlby where executor classes are stored</param>
        /// <param name="baseClassType">Base class for executors</param>
        /// <param name="postfix">Class name postfix</param>
        public ExecutorsDispatcher(Assembly assembly, Type baseClassType, string postfix = "Executor")
        {
            this.executorsDespatchTable = new Dictionary<string, Type>();

            var executorTypesMap =
                assembly.DefinedTypes.Where(type => type.IsSubclassOf(baseClassType))
                    .ToDictionary(x => x.Name, x => x.AsType());

            var fields = typeof(DriverCommand).GetRuntimeFields();
            foreach (var localField in fields.Where(field => field.IsStatic && field.FieldType == typeof(string)))
            {
                Type executorType;
                if (executorTypesMap.TryGetValue(localField.Name + postfix, out executorType))
                {
                    this.executorsDespatchTable.Add(localField.GetValue(null).ToString(), executorType);
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Creates instance of executor based on command name.
        /// If no such command is registred in mapping, then executor of type defaultType is created.
        /// If defaultType is null, then null is returned.
        /// </summary>
        /// <typeparam name="T">Class to cast instantiated executor to</typeparam>
        /// <param name="key">Command name, i.e. switchToWindow or findChildElement</param>
        /// <param name="defaultType">Type of class that will be instantiated in case if no command executor is found, or null</param>
        /// <returns></returns>
        public T GetExecutor<T>(string key, Type defaultType = null) where T : class
        {
            Type executorType;
            if (!this.executorsDespatchTable.TryGetValue(key, out executorType))
            {
                executorType = defaultType;
            }

            if (executorType == null)
            {
                return null;
            }

            return Activator.CreateInstance(executorType) as T;
        }

        #endregion
    }
}