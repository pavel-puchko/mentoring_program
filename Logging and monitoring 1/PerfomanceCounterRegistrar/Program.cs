using System;

using System.Diagnostics;
using PerformanceCounterConstants;

namespace PerfomanceCounterRegistrar
{
	class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (PerformanceCounterCategory.Exists(MusicStorePerformanceConstants.CategoryName))
                {
                    PerformanceCounterCategory.Delete(MusicStorePerformanceConstants.CategoryName);
                }

                CounterCreationDataCollection counters = new CounterCreationDataCollection();

                counters.Add(new CounterCreationData()
                {
                    CounterName = MusicStorePerformanceConstants.LoginCounterName,
                    CounterType = PerformanceCounterType.NumberOfItems32
                });

                counters.Add(new CounterCreationData()
                {
                    CounterName = MusicStorePerformanceConstants.LogoffCounterName,
                    CounterType = PerformanceCounterType.NumberOfItems32
                });

                counters.Add(new CounterCreationData()
                {
                    CounterName = MusicStorePerformanceConstants.PaymentsCounterName,
                    CounterType = PerformanceCounterType.NumberOfItems32
                });

                PerformanceCounterCategory.Create(
                    MusicStorePerformanceConstants.CategoryName,
                    string.Empty,
                    PerformanceCounterCategoryType.SingleInstance,
                    counters);

                Console.WriteLine($"Perfomance counters added. Category name: {MusicStorePerformanceConstants.CategoryName}");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("You don't have needed permissions. Run app as administrator.");
            }

            Console.ReadLine();
        }
    }
}
