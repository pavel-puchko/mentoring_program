using System;

namespace LogReader
{
    class Program
    {
        static void Main(string[] args)
        {
            LogReader logReader = new LogReader();

            Console.WriteLine($"Errors: {logReader.GerErrorsCount()}");
            Console.WriteLine(logReader.GetLogMetadata());

            Console.WriteLine("DONE.");
            Console.ReadLine();
        }
    }
}
