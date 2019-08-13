// not required if the 'EntityFramework' dll has the alias 'global'
extern alias EF6;
using EF6::System.Data.Entity;

using System; 
using System.Linq;

namespace EFClassicWithEF6.Demo.CodeFirst.Net45
{
	public class EF6
	{
		public static void Execute()
		{
			Console.WriteLine("EF6");
			Console.WriteLine("");
			Console.WriteLine("---");
			Console.WriteLine("");

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
				context.Customers.Add(new Customer { Name = "Customer_A", Description = "Description", IsActive = true });
				context.Customers.Add(new Customer { Name = "Customer_B", Description = "Description", IsActive = true });

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

				context.Customers.Add(new Customer { Name = "Customer_C", Description = "Description" });

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
		}

		public class EntityContext : DbContext
		{
			public EntityContext() : base(Program.ConnectionStringEF6)
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
