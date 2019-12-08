using System;
using System.IO;
using System.Linq;

namespace a
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
            var width = 25;
            var height = 6;
            var numLayers = input.Length / (width * height);

            var layers = Enumerable.Range(0, numLayers).Select(x => input.ToCharArray(x * width * height, width * height)).ToArray();

            var bestLayer = layers.OrderBy(x => x.Count(y => y == '0')).First();
            var res = bestLayer.Count(x => x == '1') * bestLayer.Count(x => x == '2');
            
            System.Console.WriteLine(res);
        }
    }
}
