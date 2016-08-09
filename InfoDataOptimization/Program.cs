using System.Collections.Generic;

namespace InfoDataOptimizitaion
{
	class Program
	{
		static void Main(string[] args)
		{
			var list = new List<InfoData>();
			list.Add(new InfoData { FirstName = "Pavel", LastName = "Puchko" });
			Source.CheckAndProceed(list);
		}
	}
	struct InfoData
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}

	class Source
	{
		static internal void CheckAndProceed(IEnumerable<InfoData> data)
		{
			//do something
			foreach (var item in data)
			{
				Destination.ProceedData(item.FirstName, item.LastName);
			}
		}
	}

	class Destination
	{
		static internal void ProceedData(string FirstName, string LastName)
		{
			//do something
		}
	}
}
