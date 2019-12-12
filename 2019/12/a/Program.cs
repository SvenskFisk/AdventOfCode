using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace a
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

            var bodies = input.Split("\r\n")
                .Select(x => new Body
                {
                    Position = new IntVector3(
                        int.Parse(Regex.Match(x, @"x=(-?\d+)").Groups[1].Value),
                        int.Parse(Regex.Match(x, @"y=(-?\d+)").Groups[1].Value),
                        int.Parse(Regex.Match(x, @"z=(-?\d+)").Groups[1].Value)),
                    Velocity = IntVector3.Zero
                })
                .ToArray();

            foreach (var b in bodies)
            {
                Console.WriteLine(b.ToString());
            }

            for (int i = 0; i < 1000; i++)
            {
                var gs = bodies
                    .Select(x => new
                    {
                        Body = x,
                        G = bodies
                            .Select(y => new IntVector3(
                                y.Position.X.CompareTo(x.Position.X),
                                y.Position.Y.CompareTo(x.Position.Y),
                                y.Position.Z.CompareTo(x.Position.Z)))
                            .Aggregate((a, n) => a + n)
                    })
                    .ToArray();

                Console.WriteLine();
                foreach (var g in gs)
                {
                    g.Body.Tick(g.G);
                    Console.WriteLine(g.Body.ToString());
                }
            }

            Console.WriteLine();
            Console.WriteLine("Energy: " + bodies.Sum(x => x.Energy));
        }
    }

    class Body
    {
        public IntVector3 Position { get; set; }

        public IntVector3 Velocity { get; set; }

        public int Energy => Position.AbsSum * Velocity.AbsSum;

        public void Tick(IntVector3 gravity)
        {
            Velocity += gravity;
            Position += Velocity;
        }

        public override string ToString()
        {
            return $"pos=<{Position.X,2} {Position.Y,2} {Position.Z,2}> vel=<{Velocity.X,2} {Velocity.Y,2} {Velocity.Z,2}>";
        }
    }

    [DebuggerDisplay("{X} {Y} {Z}")]
    struct IntVector3
    {
        public IntVector3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static IntVector3 Zero => new IntVector3(0, 0, 0);

        public static IntVector3 operator +(IntVector3 a, IntVector3 b) => new IntVector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        public static IntVector3 operator -(IntVector3 a, IntVector3 b) => new IntVector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        public int X { get; }

        public int Y { get; }

        public int Z { get; }

        public int AbsSum => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
    }
}
