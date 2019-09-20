using System;
using System.Data.Entity;
using EFClassic.Demo.WebCore.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace EFClassic.Demo.WebCore.Data
{
    public class EntityContextInitializer : CreateDatabaseIfNotExists<EntityContext>
    {
        protected override void Seed(EntityContext context)
        {
            // ADD 2 new customers
            context.Customers.Add(new Customer {Name = "Customer_A", Description = "Description", IsActive = true});
            context.Customers.Add(new Customer {Name = "Customer_B", Description = "Description", IsActive = true});

            context.SaveChanges();
        }
    }

    [DbConfigurationType(typeof(NpgsqlEFConfiguration))]
	public class EntityContext : DbContext
    {
	    public static string ConnectionString = "Server=localhost;Port=5432;Database=EFClassic_Demo_WebCore;User Id=[User Id]; Password=[Password]";
        public static Func<string, NpgsqlConnection> NpgsqlConnection = connectionString => new NpgsqlConnection(connectionString);

        public EntityContext() : base(NpgsqlConnection(Startup.Configuration.GetConnectionString("EntityContextDatabase")), true)
        { 
		 
            Database.SetInitializer(new EntityContextInitializer());
        }

        public DbSet<Customer> Customers { get; set; }
    }
}