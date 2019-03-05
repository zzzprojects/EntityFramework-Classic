using System.Data.Entity;
using EFClassic.Demo.WebCore.Entities;
using Microsoft.Extensions.Configuration;

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

    public class EntityContext : DbContext
    {
        public static string ConnectionString = "Server=localhost;Initial Catalog=EFClassic_Demo_WebCore;Integrated Security=true;";

        public EntityContext() : base(Startup.Configuration.GetConnectionString("EntityContextDatabase"))
        {
            Database.SetInitializer(new EntityContextInitializer());
        }

        public DbSet<Customer> Customers { get; set; }
    }
}