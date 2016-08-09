using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebThreads.Models
{
	public class Options
	{
		public string InputFolder { get; set; }

		public string OutputFolder { get; set; }

		public string Sizes { get;  set; }

		public int ThreadsNumber { get; set; }
    }
}