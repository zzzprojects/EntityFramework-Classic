using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;

namespace EFClassic.Demo.DatabaseFirst.NetCore
{
	public class Program
	{
		public static string ConnectionString = "Server=localhost;Initial Catalog=EFClassic_Demo_DatabaseFirst_Net45;Integrated Security=true;";

		public static void Main()
		{
			// The 'ConnectionString' is used in the file: \demo\EFClassic.Demo.DatabaseFirst.NetCore\EFClassic_Demo_Model.Context.cs

			EnsureDatabaseCreated();

            Z.EntityFramework.Classic.EntityFrameworkManager.UseDatabaseFirst("EFClassic_Demo_Model.edmx");

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

			Console.WriteLine("Press any key.");
			Console.ReadKey();
		}

		public static void EnsureDatabaseCreated()
		{
			// CREATE database
			var connectionStringBuilder = new SqlConnectionStringBuilder(ConnectionString);
			var database = connectionStringBuilder.InitialCatalog;
			connectionStringBuilder.InitialCatalog = "master";

			using (var connection = new SqlConnection(connectionStringBuilder.ToString()))
			{
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText = @" 
DECLARE @isDbExist INT
SELECT @isDbExist = ISNULL(db_id('" + database + @"'),-1)

IF (@isDbExist = -1)
BEGIN
	CREATE DATABASE " + database + @"
END";
					command.ExecuteNonQuery();
				}
			}

			// CREATE table
			using (var connection = new SqlConnection(ConnectionString))
			{
				connection.Open();
				using (var command = connection.CreateCommand())
				{
					command.CommandText = @"   
IF OBJECT_ID('Customers', 'U') IS  NULL 
BEGIN
	CREATE	TABLE [dbo].[Customers]
	(
		[CustomerID] [INT] IDENTITY(1, 1) NOT NULL,
		[Name] [NVARCHAR](MAX) NULL,
		[Description] [NVARCHAR](MAX) NULL,
		[IsActive] [BIT] NOT NULL,
		CONSTRAINT [PK_dbo.Customers]
			PRIMARY KEY CLUSTERED ([CustomerID] ASC)
			WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
						  	";
					command.ExecuteNonQuery();
				}
			}
		}
	}
}
