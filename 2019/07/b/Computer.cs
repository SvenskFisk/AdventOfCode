using System;
using System.Collections.Generic;
using System.Linq;

namespace b
{
    class Computer
    {
        readonly int[] _mem;

        int _pos = 0;

        public Computer(IEnumerable<int> memory, IEnumerable<int> initialInputs)
        {
            _mem = memory.ToArray();

            Inputs = new Queue<int>(initialInputs);
        }

        public Queue<int> Inputs { get; }

        public Queue<int> Outputs { get; } = new Queue<int>();

        /// <summary>
        /// true when finished, false when requiring input.
        /// </summary>
        /// <returns>true when finished, false when requiring input</returns>
        public bool Run()
        {
            if (_mem[_pos] % 100 == 99)
            {
                throw new Exception("trying to run finished program");
            }

            while (true)
            {
                var instruction = _mem[_pos];
                switch (instruction % 100)
                {
                    case 1:
                        _mem[_mem[_pos + 3]] = P(0) + P(1);
                        _pos += 4;
                        break;
                    case 2:
                        _mem[_mem[_pos + 3]] = P(0) * P(1);
                        _pos += 4;
                        break;
                    case 3:
                        if (Inputs.Count > 0)
                        {
                            _mem[_mem[_pos + 1]] = Inputs.Dequeue();
                            _pos += 2;
                            break;
                        }
                        else
                        {
                            return false;
                        }
                    case 4:
                        Outputs.Enqueue(P(0));
                        _pos += 2;
                        break;
                    case 5:
                        _pos = P(0) != 0 ? P(1) : _pos + 3;
                        break;
                    case 6:
                        _pos = P(0) == 0 ? P(1) : _pos + 3;
                        break;
                    case 7:
                        _mem[_mem[_pos + 3]] = P(0) < P(1) ? 1 : 0;
                        _pos += 4;
                        break;
                    case 8:
                        _mem[_mem[_pos + 3]] = P(0) == P(1) ? 1 : 0;
                        _pos += 4;
                        break;
                    case 99:
                        return true;
                    default:
                        throw new Exception($"pos {_pos}.");
                }
            }
        }

        private int P(int paramNumber)
        {
            var pows = new[] { 100, 1000, 10000 };
            var posMode = _mem[_pos] / pows[paramNumber] % 10 == 0;
            return posMode ? _mem[_mem[_pos + 1 + paramNumber]] : _mem[_pos + 1 + paramNumber];
        }
    }
}
