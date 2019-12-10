using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace b
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
                .Select(x => (x: x.j, y: x.i));

            var station = (x: 22, y: 19);
            var lines = new Dictionary<(int x, int y), List<(int x, int y)>>();
            foreach (var asteroid in map.Where(x => x != station))
            {
                var line = lines.Keys.Where(l => OnLine(station, l, asteroid) && SameDirection(station, l, asteroid)).ToArray();
                if (line.Length == 1)
                {
                    lines[line[0]].Add(asteroid);
                }
                else
                {
                    lines[asteroid] = new List<(int x, int y)> { asteroid };
                }
            }

            var cwLines = lines.Values
                .OrderBy(x => Angle(station, x[0]))
                .Select(x => (a: Angle(station, x[0]), l: x.OrderBy(y => Distance(station, y)).ToList()))
                .ToList();

            var killCount = 0;
            var lastAngle = -1.0;
            while (killCount < 200)
            {
                var line = cwLines.Where(x => x.a > lastAngle).DefaultIfEmpty(cwLines.First()).First();
                lastAngle = line.a;
                var asteroid = line.l.First();

                line.l.Remove(asteroid);
                if (line.l.Count == 0)
                {
                    cwLines.Remove(line);
                }

                killCount++;
                System.Console.WriteLine($"{killCount}: {asteroid}");

                if (killCount==200)
                {
                    System.Console.WriteLine(100*asteroid.x+asteroid.y);
                }
            }
        }

        private static int Distance((int x, int y) a, (int x, int y) b)
        {
            return a.x + a.y - b.x - b.y;
        }

        private static double Angle((int x, int y) a, (int x, int y) b)
        {
            var normB = (x: b.x - a.x, y: b.y - a.y);
            var ret = normB.x >= 0 ? Math.Atan2(normB.x, -normB.y) : Math.Atan2(-normB.x, normB.y) + Math.PI;

            return ret;
        }

        private static bool OnLine((int x, int y) a, (int x, int y) b, (int x, int y) p)
        {
            var normB = (x: b.x - a.x, y: b.y - a.y);
            var normP = (x: p.x - a.x, y: p.y - a.y);

            return normB.x * normP.y == normB.y * normP.x;
        }

        private static bool SameDirection((int x, int y) a, (int x, int y) b, (int x, int y) p)
        {
            var normB = (x: b.x - a.x, y: b.y - a.y);
            var normP = (x: p.x - a.x, y: p.y - a.y);

            return !((normB.x > 0 || normB.x == 0 && normB.y > 0) ^ (normP.x > 0 || normP.x == 0 && normP.y > 0));
        }
    }
}
