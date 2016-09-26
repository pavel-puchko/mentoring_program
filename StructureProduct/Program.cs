using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureProduct
{
	class Program
	{
		static void Main(string[] args)
		{
		}

		struct Product
		{
			public readonly string name;
			public readonly int? count;
			public readonly int price;

			public Product(string name, int? count, int price)
			{
				this.name = name;
				this.count = count;
				this.price = price;
			}
		}
	}
}
