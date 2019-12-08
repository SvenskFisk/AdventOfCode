using System;
using System.IO;
using System.Linq;

namespace b
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

            var image = Enumerable.Range(0, width*height).Select(i => layers.Select(x => x[i]).First(x=> x != '2')).ToArray();

            for (int i = 0; i < height; i++)
            {
                Console.WriteLine(new string(image, i * width, width).Replace('0',' ').Replace('1', '8'));
            }
        }
    }
}
