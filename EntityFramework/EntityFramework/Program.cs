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

		}
		static void ordersSelect(string category)
		{
			using (var db = new Northwind())
			{
				var orders = db.Orders
					.Where(o => o.Order_Details.Any(d => d.Product.Category.CategoryName == category))
					.ToList();
			}
		}
	}
}
