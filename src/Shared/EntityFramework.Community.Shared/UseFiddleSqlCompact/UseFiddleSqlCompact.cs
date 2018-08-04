using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Linq;

namespace Z.EntityFramework.Classic
{
    internal static class UseFiddleSqlCompact
    {
        public static object SqlCeProviderServicesInstance;
        public static object SqlCeProviderFactoryInstance;
        public static Type SqlCeProviderFactoryType;

        public static void Hook(object sqlCeProviderServicesInstance, object sqlCeProviderFactoryInstance)
        {
            SqlCeProviderServicesInstance = sqlCeProviderServicesInstance;
            SqlCeProviderFactoryInstance = sqlCeProviderFactoryInstance;

            SqlCeProviderFactoryType = sqlCeProviderFactoryInstance.GetType().Assembly
                .GetType("System.Data.SqlServerCe.SqlCeProviderFactory");

            // Add an event handler for DbConfiguration.Loaded, which adds our dependency 
            // resolver class to the chain of resolvers.
            DbConfiguration.Loaded += (_, a) => { a.AddDependencyResolver(new MyDependencyResolver(), true); };
        }

        internal class MyDependencyResolver : IDbDependencyResolver
        {
            public object GetService(Type type, object key)
            {
                if (type == typeof(DbProviderFactory) && key != null && (string) key == "System.Data.SqlClient")
                    return SqlCeProviderFactoryInstance;
                if (type == typeof(DbProviderServices) && key != null && (string) key == "System.Data.SqlServerCe.4.0")
                    return SqlCeProviderServicesInstance;
                if (type == typeof(IProviderInvariantName) && key.GetType() == SqlCeProviderFactoryType)
                    return new MyProviderInvariantName();

                return null;
            }

            public IEnumerable<object> GetServices(Type type, object key)
            {
                return new[] {GetService(type, key)}.ToList().Where(o => o != null);
            }
        }

        // Implement IProviderInvariantName so that we can return an object when
        // requested in GetService()
        internal class MyProviderInvariantName : IProviderInvariantName
        {
            public string Name => "System.Data.SqlServerCe.4.0";
        }
    }
}