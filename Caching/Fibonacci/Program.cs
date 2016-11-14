using System;

namespace Fibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            FibonacciNumbers numbers = new FibonacciNumbers(new FibonacciNumberMemoryCache());

            Console.WriteLine(numbers.Get(11));
            Console.WriteLine(numbers.Get(1));
            Console.WriteLine(numbers.Get(2));
            Console.WriteLine(numbers.Get(3));
            Console.WriteLine(numbers.Get(4));
            Console.WriteLine(numbers.Get(5));
            Console.WriteLine(numbers.Get(6));
            Console.WriteLine(numbers.Get(7));
            Console.WriteLine(numbers.Get(9));
            Console.WriteLine(numbers.Get(10));
        }
    }
}
