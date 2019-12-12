using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace b
{
    class Program
    {
        static void Main(string[] args)
        {
            var input =
@"<x=5, y=-1, z=5>
<x=0, y=-14, z=2>
<x=16, y=4, z=0>
<x=18, y=1, z=16>";

            var pos = new[,] {
                { 5,0,16,18},
                { -1,-14,4,1},
                { 5,2,0,16}
            };

            var posO = new[,] {
                { 5,0,16,18},
                { -1,-14,4,1},
                { 5,2,0,16}
            };

            var vel = new[,] {
                { 0,0,0,0},
                { 0,0,0,0},
                { 0,0,0,0}
            };

            var periods = new long[3];
            for (int ii = 0; ii < 3; ii++) // loop over x,y,z
            {
                do
                {
                    for (int i = 0; i < 4; i++) // loop over bodies for one dimension
                    {
                        var p = pos[ii, i];
                        vel[ii, i] +=
                            pos[ii, 0].CompareTo(p) +
                            pos[ii, 1].CompareTo(p) +
                            pos[ii, 2].CompareTo(p) +
                            pos[ii, 3].CompareTo(p);
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        pos[ii, i] += vel[ii, i];
                    }
                    periods[ii]++;

                    if (pos[ii, 0] == posO[ii, 0] && pos[ii, 1] == posO[ii, 1] && pos[ii, 2] == posO[ii, 2] && pos[ii, 3] == posO[ii, 3] &&
                        vel[ii, 0] == 0 && vel[ii, 1] == 0 && vel[ii, 2] == 0)
                    {
                        Console.WriteLine($"{ii} period {periods[ii]}");
                        break;
                    }
                } while (true);
            }

            var state = new long[3];
            do
            {
                if (state[0] < state[1]) state[0] += periods[0];
                else if (state[1] < state[2]) state[1] += periods[1];
                else state[2] += periods[2];

            } while (state[0] != state[1] || state[0] != state[2]);

            Console.WriteLine("Final: " + state[0]);
        }
    }
}
