namespace Fibonacci
{
    interface IFibonacciNumberCache
    {
        int? Get(int number);

        void Set(int number, int value);
    }
}
