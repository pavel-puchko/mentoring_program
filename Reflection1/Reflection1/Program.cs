using System;
using System.Collections;
using System.Collections.Generic;

namespace Reflection
{
	class Program
	{
		private static IList CreateDynamicList(Type type)
		{
			var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(type));

			return (IList)list;
		}

		static void Main(string[] args)
		{
			var list = CreateDynamicList(typeof(int));
			for (var i = 0; i < 5; i++)
			{
				list.Add(i);

			}
		}
	}
}