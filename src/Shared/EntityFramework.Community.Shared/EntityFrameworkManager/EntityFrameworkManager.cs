using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Z.EntityFramework.Classic
{

    /// <summary>EntityFrameworkManager</summary>
    public static class EntityFrameworkManager
    {
        /// <summary>Dictionary of is assignable froms.</summary>
        public static ConcurrentDictionary<Type, ConcurrentDictionary<Type, bool>> IsAssignableFromDict = new ConcurrentDictionary<Type, ConcurrentDictionary<Type, bool>>();

#if NETSTANDARD
        /// <summary>Use database first.</summary>
        /// <param name="modelName">Name of the model.</param>
        public static void UseDatabaseFirst(string modelName)
        {
           UseDatabaseFirstManager.Execute(modelName);
        }
#endif

        /// <summary>Use fiddle SQL compact.</summary>
        /// <param name="sqlCeProviderServicesInstance">The SQL ce provider services instance.</param>
        /// <param name="sqlCeProviderFactoryInstance">The SQL ce provider factory instance.</param>
        public static void UseFiddleSqlCompact(object sqlCeProviderServicesInstance, object sqlCeProviderFactoryInstance)
        {
            Z.EntityFramework.Classic.UseFiddleSqlCompact.Hook(sqlCeProviderServicesInstance, sqlCeProviderFactoryInstance);
        }
        /// <summary>Query if 'parentClass' is assignable from.</summary>
        /// <param name="parentClass">The parent class.</param>
        /// <param name="baseClass">The base class.</param>
        /// <returns>True if assignable from, false if not.</returns>
        public static bool IsAssignableFrom(Type parentClass, Type baseClass)
        {
            // TODO: ZZZ - Need some rework!
            ConcurrentDictionary<Type, bool> assignableFromDict;

            if (!IsAssignableFromDict.TryGetValue(parentClass, out assignableFromDict)){

                assignableFromDict = IsAssignableFromDict[parentClass] = new ConcurrentDictionary<Type, bool>();
            }

            bool isAssignable;
            if (!assignableFromDict.TryGetValue(baseClass, out isAssignable))
            {
                isAssignable = baseClass.IsAssignableFrom(parentClass);
                assignableFromDict[baseClass] = isAssignable;
            }

            return isAssignable;
        }

        /// <summary>A SQL server.</summary>
        public class SqlServer
        {
            /// <summary>Manager for servers.</summary>
            public class BackwardCompatibility
            {
                /// <summary>True to use date time 2 as default.</summary>
                public static bool UseDateTime2 = false;
            }
        }
    }
}
