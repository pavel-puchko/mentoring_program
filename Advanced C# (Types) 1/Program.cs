using System.Collections.Generic;
using System.Linq;

namespace InfoDataOptimizitaion
{
	class Program
	{
	}

	struct InfoData : IInfoData
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}

	interface IInfoData
	{
		string FirstName { get; set; }
		string LastName { get; set; }
	}

	class Source
	{
		internal void CheckAndProceed(List<InfoData> data)
		{
			var dest = new Destination();
			var cData = data.Cast<IInfoData>().ToList();

			dest.ProceedData(cData);
		}
	}

	class Destination
	{
		internal void ProceedData(List<IInfoData> data)
		{
			foreach (var item in data)
			{
				//do something
			}
		}
}
}
