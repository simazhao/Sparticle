using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Common
{
    public static class SerialRandom
    {
        /// <summary>
        /// you can use like this:
        /// foreach(var m in new CyptoRandom().Serial(10, 100))
        /// {
        ///
        /// }
        /// </summary>
        /// <param name="random"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static IEnumerable<int> Serial(this Random random, int minValue=0, int maxValue=int.MaxValue)
        {
            return new SerialRandomEnumer(random, minValue, maxValue);
        }

        class SerialRandomEnumer : IEnumerable<int>
        {
            Random _random;
            int _minValue;
            int _maxValue;

            public SerialRandomEnumer(Random random, int minValue, int maxValue)
            {
                _random = random;
                _minValue = minValue;
                _maxValue = maxValue;
            }

            public IEnumerator<int> GetEnumerator()
            {
                return Serial(_random, _minValue, _maxValue);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return Serial(_random, _minValue, _maxValue);
            }

            public static IEnumerator<int> Serial(Random random, int minValue, int MaxValue)
            {
                int diff = MaxValue - minValue;
                int count = 0;

                while (count < diff)
                {
                    ++count;
                    yield return random.Next(minValue, MaxValue);
                }
            }
        }
    }

    public class CyptoRandom : Random 
    {
        private readonly static uint BiggerBuffer = 8; 

        private RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();
        private byte[] _buffer;
        private int _bufferPosition;
        private bool _useBigBuffer = false;

        public CyptoRandom()
            :this(false)
        {

        }

        public CyptoRandom(bool pooled)
        {
            _useBigBuffer = pooled;
        }

        public override int Next()
        {
            byte[] bytes = new byte[sizeof(int)];

            this._rng.GetBytes(bytes);

            return BitConverter.ToInt32(bytes, 0);
        }

        public override int Next(int maxValue)
        {
            return Next(0, maxValue);
        }

        public override int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentException("minValue must be less than maxValue");

            if (minValue == maxValue)
                return minValue;

            long diff = maxValue;
            diff -= minValue;
            ++diff;

            var rand = GetRandomUint32();

            return (int)(minValue + (rand % diff));
        }

        public override void NextBytes(byte[] buffer)
        {
            if (_useBigBuffer && buffer.Length < GetBufferSize())
            {
                EnsureBuffer(buffer.Length);

                Buffer.BlockCopy(this._buffer, _bufferPosition, buffer, 0, buffer.Length);

                _bufferPosition += buffer.Length;
            }
            else
            {
                _rng.GetBytes(buffer);
            }
        }

        public override double NextDouble()
        {
            return GetRandomUint32() / (1.0 + uint.MaxValue);
        }

        private uint GetRandomUint32()
        {
            const int Uint32Size = sizeof(uint);

            EnsureBuffer(Uint32Size);
            
            var ret = BitConverter.ToUInt32(_buffer, _bufferPosition);

            _bufferPosition += Uint32Size;

            return ret;
        }

        private void EnsureBuffer(int requireSize)
        {
            if (_buffer == null)
            {
                InitBuffer();
            }

            if (requireSize > _buffer.Length)
                throw new ArgumentOutOfRangeException("requiredSize", "cannot be greater than buffer length");

            if (_bufferPosition + requireSize > _buffer.Length)
            {
                InitBuffer();
            }
        }

        private void InitBuffer()
        {
            var buffersize = GetBufferSize();

            _buffer = new byte[buffersize];

            _rng.GetBytes(_buffer);

            _bufferPosition = 0;
        }

        private uint GetBufferSize()
        {
            return _useBigBuffer ? BiggerBuffer * sizeof(uint) : sizeof(uint);
        }
    }
}
