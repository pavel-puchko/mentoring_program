using System;
using System.Drawing.Imaging;



namespace HttpParser
{
	class Program
    {
        static void Main(string[] args)
        {
            var parser = new HttpParser();
            parser.ParseUrl("https://code.google.com/archive/p/fizzler/", @"D:\site\", 2, ImageFormat.Png);
            Console.ReadKey();
        }
    }
}
