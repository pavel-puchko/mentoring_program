using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var db = new Northwind())
			{
				var orders = (
					from order in db.Orders
					select order
				).ToList();

				Console.WriteLine("Press any key to exit...");
				Console.ReadKey();
			}
		}
	}
}
