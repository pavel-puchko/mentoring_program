using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
	class Program
	{
		public static List<int> GenerateListOfSimpleNumbers(int n, int firstSimpleNumber)
		{
			List<int> listOfSimpleNumbers = new List<int>();
			listOfSimpleNumbers.Add(firstSimpleNumber);
			int nextNumber = firstSimpleNumber == 2 ? 3 : firstSimpleNumber + 2;

			while (listOfSimpleNumbers.Count < n)
			{
				int nextNumberSqrt = (int)Math.Sqrt(nextNumber);
				bool nextNumberIsSimple = true;

				for (int i = 0; (int)listOfSimpleNumbers[i] <= nextNumberSqrt; i++)
				{
					if (nextNumber % listOfSimpleNumbers[i] == 0)
					{
						nextNumberIsSimple = false;
						break;
					}
				}

				if (nextNumberIsSimple)
				{
					listOfSimpleNumbers.Add(nextNumber);
				}

				nextNumber += 2;
			}

			return listOfSimpleNumbers;
		}

		public static double SumElementsFromListDividedByIndex(List<int> list, int indexIncrease)
		{
			double result = 0;

			for (int i = 0; i < list.Count; i++)
			{
				result += list[i] / (double)(i + indexIncrease);
            }

			return result;
		}

		static void Main(string[] args)
		{
			List<int> simpleList = GenerateListOfSimpleNumbers(4, 2);
			int sumElements = (int)Math.Floor(SumElementsFromListDividedByIndex(simpleList, 1));
			Console.WriteLine(sumElements);
        }
	}
}
