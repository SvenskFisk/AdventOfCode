using System;
using System.Collections.Generic;
using System.Linq;

namespace a
{
    class Computer
    {
        const int s_pageSize = 100;

        long _pos = 0;

        long _paramOffset = 0;

        Dictionary<long, long[]> _pages = new Dictionary<long, long[]>();

        public Computer(IEnumerable<long> memory)
        {
            var i = 0;
            foreach (var item in memory)
            {
                Set(i++, item);
            }
        }

        public Queue<long> Inputs { get; } = new Queue<long>();

        public Queue<long> Outputs { get; } = new Queue<long>();

        /// <summary>
        /// true when finished, false when requiring input.
        /// </summary>
        /// <returns>true when finished, false when requiring input</returns>
        public bool Run()
        {
            if (Get(_pos) % 100 == 99)
            {
                throw new Exception("trying to run finished program");
            }

            while (true)
            {
                var instruction = Get(_pos);
                switch (instruction % 100)
                {
                    case 1:
                        S(2, P(0) + P(1));
                        _pos += 4;
                        break;
                    case 2:
                        S(2, P(0) * P(1));
                        _pos += 4;
                        break;
                    case 3:
                        if (Inputs.Count > 0)
                        {
                            S(0, Inputs.Dequeue());
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
                        S(2, P(0) < P(1) ? 1 : 0);
                        _pos += 4;
                        break;
                    case 8:
                        S(2, P(0) == P(1) ? 1 : 0);
                        _pos += 4;
                        break;
                    case 9:
                        _paramOffset += P(0);
                        _pos += 2;
                        break;
                    case 99:
                        return true;
                    default:
                        throw new Exception($"pos {_pos}.");
                }
            }
        }

        private long Get(long position)
        {
            long[] page;
            if (_pages.TryGetValue(position / s_pageSize, out page))
            {
                return page[position % s_pageSize];
            }
            else
            {
                return 0;
            }
        }

        private void Set(long position, long value)
        {
            long[] page;
            if (!_pages.TryGetValue(position / s_pageSize, out page))
            {
                _pages.Add(position / s_pageSize, page = new long[s_pageSize]);
            }

            page[position % s_pageSize] = value;
        }

        private long P(int paramNumber)
        {
            var pows = new[] { 100, 1000, 10000 };
            var mode = Get(_pos) / pows[paramNumber] % 10;

            return mode switch
            {
                0 => Get(Get(_pos + 1 + paramNumber)),
                1 => Get(_pos + 1 + paramNumber),
                2 => Get(Get(_pos + 1 + paramNumber) + _paramOffset),
                _ => throw new Exception()
            };
        }

        private void S(int paramNumber, long value)
        {
            var pows = new[] { 100, 1000, 10000 };
            var mode = Get(_pos) / pows[paramNumber] % 10;

            var position = mode switch
            {
                0 => Get(_pos + 1 + paramNumber),
                1 => _pos + 1 + paramNumber,
                2 => Get(_pos + 1 + paramNumber) + _paramOffset,
                _ => throw new Exception()
            };

            Set(position, value);
        }
    }
}
