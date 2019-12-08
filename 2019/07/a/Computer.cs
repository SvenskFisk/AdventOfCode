using System;
using System.Collections.Generic;
using System.Linq;

namespace a
{
    class Computer
    {
        readonly int[] _mem;

        public Computer(IEnumerable<int> memory)
        {
            _mem = memory.ToArray();
        }

        public IEnumerable<int> Compute(IEnumerable<int> userInputs)
        {
            var inputs = new Queue<int>(userInputs);
            var pos = 0;

            var outputs = new List<int>();
            while (true)
            {
                var instruction = _mem[pos];
                switch (instruction % 100)
                {
                    case 1:
                        _mem[_mem[pos + 3]] = P(0, pos) + P(1, pos);
                        pos += 4;
                        break;
                    case 2:
                        _mem[_mem[pos + 3]] = P(0, pos) * P(1, pos);
                        pos += 4;
                        break;
                    case 3:
                        _mem[_mem[pos + 1]] = inputs.Dequeue();
                        pos += 2;
                        break;
                    case 4:
                        outputs.Add(P(0, pos));
                        pos += 2;
                        break;
                    case 5:
                        pos = P(0, pos) != 0 ? P(1, pos) : pos + 3;
                        break;
                    case 6:
                        pos = P(0, pos) == 0 ? P(1, pos) : pos + 3;
                        break;
                    case 7:
                        _mem[_mem[pos + 3]] = P(0, pos) < P(1, pos) ? 1 : 0;
                        pos += 4;
                        break;
                    case 8:
                        _mem[_mem[pos + 3]] = P(0, pos) == P(1, pos) ? 1 : 0;
                        pos += 4;
                        break;
                    case 99:
                        return outputs;
                    default:
                        throw new Exception($"pos {pos}.");
                }
            }
        }

        private int P(int paramNumber, int pos)
        {
            var pows = new[] { 100, 1000, 10000 };
            var posMode = _mem[pos] / pows[paramNumber] % 10 == 0;
            return posMode ? _mem[_mem[pos + 1 + paramNumber]] : _mem[pos + 1 + paramNumber];
        }
    }
}
