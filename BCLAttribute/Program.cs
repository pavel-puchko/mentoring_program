using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BCLAttribute
{
	class Program
	{
		static void Main(string[] args)
		{
		}

		[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
		public class CustomRangeAttribute : ValidationAttribute
		{
			public decimal From;
			public decimal To;

			public CustomRangeAttribute(string from, string to)
			{
				From = decimal.Parse(from, CultureInfo.InvariantCulture);
				To = decimal.Parse(to, CultureInfo.InvariantCulture);
			}

			public override bool IsValid(object value)
			{
				var decimalValue = (decimal)value;

				return decimalValue >= From && decimalValue <= To;
			}
		}
	}
}