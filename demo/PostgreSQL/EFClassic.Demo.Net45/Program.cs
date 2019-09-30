using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using Npgsql;

namespace EFClassic.Demo.Net45
{
    internal class Program
    { 
        public static string ConnectionString = "Server=localhost;Port=5432;Database=EFClassic_Demo_Net45;User Id=[User Id]; Password=[Password]";

		public static void Main()
        { 
			// CLEAR
			using (var context = new EntityContext())
            {
                context.Customers.RemoveRange(context.Customers);
                context.SaveChanges();

                Console.WriteLine("Database clearer...");
                Console.WriteLine("");
                Console.WriteLine("---");
                Console.WriteLine("");
            }

            // ADD 2 new customers
            using (var context = new EntityContext())
            {
                context.Customers.Add(new Customer {Name = "Customer_A", Description = "Description", IsActive = true});
                context.Customers.Add(new Customer {Name = "Customer_B", Description = "Description", IsActive = true});

                context.SaveChanges();

                Console.WriteLine("Customers added...");
            }

            using (var context = new EntityContext())
            {
                foreach (var customer in context.Customers.ToList())
                {
                    Console.WriteLine("");
                    Console.WriteLine("Customer.CustomerID : " + customer.CustomerID);
                    Console.WriteLine("Customer.Name : " + customer.Name);
                    Console.WriteLine("Customer.Description : " + customer.Description);
                    Console.WriteLine("Customer.IsActive : " + customer.IsActive);
                }

                Console.WriteLine("");
                Console.WriteLine("---");
                Console.WriteLine("");
            }

            using (var context = new EntityContext())
            {
                var list = context.Customers.ToList();

                list.First().Description = "Updated_A";

                context.Customers.Add(new Customer {Name = "Customer_C", Description = "Description"});

                context.SaveChanges();
                Console.WriteLine("Customers added/updated...");
            }

            using (var context = new EntityContext())
            {
                foreach (var customer in context.Customers.ToList())
                {
                    Console.WriteLine("");
                    Console.WriteLine("Customer.CustomerID : " + customer.CustomerID);
                    Console.WriteLine("Customer.Name : " + customer.Name);
                    Console.WriteLine("Customer.Description : " + customer.Description);
                    Console.WriteLine("Customer.IsActive : " + customer.IsActive);
                }

                Console.WriteLine("");
                Console.WriteLine("---");
                Console.WriteLine("");
            }

            Console.WriteLine("Press any key.");
            Console.ReadKey();
        }

		[DbConfigurationType(typeof(NpgsqlEFConfiguration))]
		public class EntityContext : DbContext
        {
	        public static Func<string, NpgsqlConnection> NpgsqlConnection = connectionString => new NpgsqlConnection(connectionString);
			 
			public EntityContext()  : base(NpgsqlConnection(ConnectionString), true)
            {  
            }

            public DbSet<Customer> Customers { get; set; }
        }


        public class Customer
        {
            public int CustomerID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool IsActive { get; set; }
        }
    }
}