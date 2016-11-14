using System.Diagnostics;
using PerformanceCounterConstants;

namespace MvcMusicStore.Infrastructure.Performance
{
    public static class PerformanceHelper
    {
        private static string _categoryName = MusicStorePerformanceConstants.CategoryName;

        public static string CategoryName
        {
            get { return _categoryName; }
        }

        public static void IncrementCounter(string counterName)
        {
            using (var counter = new PerformanceCounter(_categoryName, counterName, false))
            {
                counter.Increment();
            }
        }
    }
}