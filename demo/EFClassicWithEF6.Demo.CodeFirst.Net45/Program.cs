// not required if the 'Z.EntityFramework.Classic' dll has the alias 'global'
extern alias EFClassic;
using EFClassic::Z.EntityFramework.Classic;

using System;

namespace EFClassicWithEF6.Demo.CodeFirst.Net45
{
	public class Program
	{
		public static string ConnectionStringEF6 = "Server=localhost;Initial Catalog=EF6_Demo;Integrated Security=true;";
		public static string ConnectionStringEFClassic = "Server=localhost;Initial Catalog=EFClassic_Demo;Integrated Security=true;";

        // See 'https://entityframework-classic.net/using-ef-classic-with-ef-6' to understand about extern alias
        public static void Main()
		{ 
			// see App.Config
			EntityFrameworkManager.ConfigSectionName = "entityFrameworkClassic";

			EF6.Execute();
			EFClassic.Execute();

			Console.WriteLine("Press any key.");
			Console.ReadKey();
		}
	}
}