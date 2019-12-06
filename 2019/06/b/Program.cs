using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace b
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
            nodes.Add("COM", new Node("COM", null));

            var santa = nodes[nodes["SAN"].Parent];
            var santaCount = 0;
            while (true)
            {
                var meCount = 0;
                var me = nodes[nodes["YOU"].Parent];
                while (me.Name != santa.Name && me.Parent != null)
                {
                    me = nodes[me.Parent];
                    meCount++;
                }

                if (me.Name == santa.Name)
                {
                    System.Console.WriteLine(meCount + santaCount);
                    break;
                }

                santa = nodes[santa.Parent];
                santaCount++;
            }
        }
    }
}
