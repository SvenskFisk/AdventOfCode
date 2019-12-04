using System;

namespace a
{
    class Program
    {
        static void Main(string[] args)
        {
            var start = 165432;
            var end = 707912;

            var validCount = 0;

            for (int i = start; i <= end; i++)
            {
                if (NeverDecrease(i) && HasDouble(i))
                {
                    validCount++;
                }
            }

            System.Console.WriteLine(validCount);
        }

        static bool NeverDecrease(int i)
        {
            return 
                (i / 100000) % 10 <= (i / 10000) % 10 &&
                (i / 10000) % 10 <= (i / 1000) % 10 &&
                (i / 1000) % 10 <= (i / 100) % 10 &&
                (i / 100) % 10 <= (i / 10) % 10 &&
                (i / 10) % 10 <= (i) % 10;
        }

        static bool HasDouble(int i)
        {
            return 
                (i / 100000) % 10 == (i / 10000) % 10 ||
                (i / 10000) % 10 == (i / 1000) % 10 ||
                (i / 1000) % 10 == (i / 100) % 10 ||
                (i / 100) % 10 == (i / 10) % 10 ||
                (i / 10) % 10 == (i) % 10;
        }
    }
}
