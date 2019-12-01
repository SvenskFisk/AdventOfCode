using System;
using System.IO;
using System.Linq;

namespace b
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
                    SimpleFuel =  x / 3 - 2,
                    Fuel = RecursiveFuel(x / 3 - 2)
                })
                .ToArray();

            var totalFuel = modules.Sum(x => x.Fuel);

            System.Console.WriteLine($"Total fuel required: {totalFuel}.");
        }

        static int RecursiveFuel(int mass)
        {
            var newFuel = mass / 3 - 2;
            if (newFuel > 0)
            {
                return mass + RecursiveFuel(newFuel);
            }
            else
            {
                return mass;
            }
        }
    }
}
