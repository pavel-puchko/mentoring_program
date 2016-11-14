using System;

namespace Fibonacci
{
    class FibonacciNumbers
    {
        private IFibonacciNumberCache _cache;

        public FibonacciNumbers(IFibonacciNumberCache cache)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            _cache = cache;
        }

        public int Get(int number)
        {
            if (number < 1)
            {
                throw new ArgumentException(nameof(number) + "can't be less than 1");
            }

            int result = this.GetFromCache(number) ?? this.CalculateValue(number);
            return result;
        }

        private int? GetFromCache(int number)
        {
            return _cache.Get(number);
        }

        private int CalculateValue(int number)
        {
            int result;

            if (number <= 2)
            {
                result = 1;
            }
            else
            {
                result = this.Get(number - 1) + this.Get(number - 2);
                _cache.Set(number, result);
            }

            return result;
        }
    }
}
