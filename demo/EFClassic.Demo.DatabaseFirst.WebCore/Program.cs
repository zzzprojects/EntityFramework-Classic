using System.Data.SqlClient;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace EFClassic.Demo.DatabaseFirst.WebCore
{
	public class Program
	{
		public static string ConnectionString = "Server=localhost;Initial Catalog=EFClassic_Demo_DatabaseFirst_WebCore;Integrated Security=true;";

		public static void Main(string[] args)
		{
			// The 'ConnectionString' is used in the file: \demo\EFClassic.Demo.DatabaseFirst.NetCore\Model\EFClassic_Demo_Model.Context.cs

			EnsureDatabaseCreated();

			Z.EntityFramework.Classic.EntityFrameworkManager.UseDatabaseFirst(@"Model\EFClassic_Demo_Model.edmx");

			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args)
		{
			return WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
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
