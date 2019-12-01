using System;
using System.IO;
using System.Linq;

namespace a
{
    class Program
    {
        static void Main(string[] args)
        {
            var modules = File.ReadAllLines("input.txt")
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(int.Parse)
                .Select(x => new {
                    Weight = x,
                    Fuel =  x / 3 - 2
                })
                .ToArray();

            var totalFuel = modules.Sum(x => x.Fuel);

            System.Console.WriteLine($"Total fuel required: {totalFuel}.");
        }
    }
}
