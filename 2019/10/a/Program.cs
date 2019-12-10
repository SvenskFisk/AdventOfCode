using System;
using System.Collections.Generic;
using System.Linq;

namespace a
{
    class Program
    {
        static void Main(string[] args)
        {
            string input =
@"###..#.##.####.##..###.#.#..
#..#..###..#.......####.....
#.###.#.##..###.##..#.###.#.
..#.##..##...#.#.###.##.####
.#.##..####...####.###.##...
##...###.#.##.##..###..#..#.
.##..###...#....###.....##.#
#..##...#..#.##..####.....#.
.#..#.######.#..#..####....#
#.##.##......#..#..####.##..
##...#....#.#.##.#..#...##.#
##.####.###...#.##........##
......##.....#.###.##.#.#..#
.###..#####.#..#...#...#.###
..##.###..##.#.##.#.##......
......##.#.#....#..##.#.####
...##..#.#.#.....##.###...##
.#.#..#.#....##..##.#..#.#..
...#..###..##.####.#...#..##
#.#......#.#..##..#...#.#..#
..#.##.#......#.##...#..#.##
#.##..#....#...#.##..#..#..#
#..#.#.#.##..#..#.#.#...##..
.#...#.........#..#....#.#.#
..####.#..#..##.####.#.##.##
.#.######......##..#.#.##.#.
.#....####....###.#.#.#.####
....####...##.#.#...#..#.##.";

            var map = input.Split("\r\n")
                .SelectMany((x, i) => x
                    .Select((y, j) => (j, i, y)))
                .Where(x => x.y == '#')
                .Select(x => (x: x.j, y: x.i))
                .ToArray();

            var pairs = map.SelectMany((x, i) => map.Where((y, j) => j > i).Select(y => (a: x, b: y)));
            var observablePairs = pairs.Where(p => !map.Where(x => x != p.a && x != p.b).Any(x => OnLine(p.a, p.b, x) && Between(p.a, p.b, x)));

            var observableCount = observablePairs
                .SelectMany(p => new[] { p.a, p.b })
                .GroupBy(x => x, (x, xs) => (x, count: xs.Count()))
                .OrderByDescending(x => x.count)
                .ToArray();

            System.Console.WriteLine(observableCount[0]);
        }

        private static bool OnLine((int x, int y) a, (int x, int y) b, (int x, int y) p)
        {
            var normB = (x: b.x - a.x, y: b.y - a.y);
            var normP = (x: p.x - a.x, y: p.y - a.y);

            return normB.x * normP.y == normB.y * normP.x;
        }

        private static bool Between((int x, int y) a, (int x, int y) b, (int x, int y) p)
        {
            var outsideX = (p.x > a.x && p.x > b.x) || (p.x < a.x && p.x < b.x);
            var outsideY = (p.y > a.y && p.y > b.y) || (p.y < a.y && p.y < b.y);
            
            return !outsideX && !outsideY;
        }
    }
}
