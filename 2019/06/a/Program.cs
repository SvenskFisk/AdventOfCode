using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace a
{
    class Node
    {
        public Node(string name, string parent)
        {
            Name = name;
            Parent = parent;
        }

        public string Name { get; }
        public string Parent { get; }

        public int Orbits { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var nodes = File.ReadAllLines("input.txt")
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToDictionary(
                    x => x.Substring(4, 3), 
                    x => new Node(x.Substring(4, 3), x.Substring(0, 3)));

            foreach (var node in nodes.Values)
            {
                node.Orbits = 1;
                var current = node;
                while (current.Parent != "COM")
                {
                    node.Orbits++;
                    current = nodes[current.Parent];
                }
            }

            var orbits = nodes.Values.Sum(x => x.Orbits);
            System.Console.WriteLine(orbits);
        }
    }
}
