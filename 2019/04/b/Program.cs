using System;
using System.Linq;

namespace b
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
                if (NeverDecrease(i) && HasExactDouble(i))
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

        static bool HasExactDouble(int i)
        {
            var digits = new[] {
                (i / 100000) % 10,
                (i / 10000) % 10,
                (i / 1000) % 10,
                (i / 100) % 10,
                (i / 10) % 10,
                i % 10
            };

            var triplets = new[] {
                (digits[0] == digits[1] && digits[1] == digits[2]),
                                          (digits[1] == digits[2] && digits[2] == digits[3]),
                                                                    (digits[2] == digits[3] && digits[3] == digits[4]),
                                                                                              (digits[3] == digits[4] && digits[4] == digits[5])
            };

            var doubles = new[] {
                digits[0] == digits[1],
                             digits[1] == digits[2],
                                          digits[2] == digits[3],
                                                       digits[3] == digits[4],
                                                                    digits[4] == digits[5],
            };

            return 
                doubles[0] && !triplets[0] && !triplets[1] ||
                doubles[1] && !triplets[0] && !triplets[1] && !triplets[2] ||
                doubles[2] && !triplets[0] && !triplets[1] && !triplets[2] && !triplets[3] ||
                doubles[3] &&                 !triplets[1] && !triplets[2] && !triplets[3] ||
                doubles[4] &&                                 !triplets[2] && !triplets[3];
        }
    }
}
