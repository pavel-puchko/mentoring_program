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
			public string Name { get; set; }

			public float? Price { get; set; }

			public int Count { get; set; }
		}
	}
}
