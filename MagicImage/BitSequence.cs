using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicImage
{
    internal class BitSequence
    {
        Random random = new Random();
        private int _bitCounter = 0;
        private int _byteCounter = 0;
        private byte[] _data;
        private byte[] _dataSize = new byte[4];
        private bool getDataSize = true;
        public bool endOfPaylod { get; private set; }
        /// <summary>
        /// First 4 bytes is sizeof payload
        /// </summary>
        /// <param name="data">payload</param>
        public BitSequence(byte[] data)
        {
            endOfPaylod = false;
            int len = data.Length;
            for (int i = 0; i < _dataSize.Length; i++)
            {
                _dataSize[i] = (byte)(len >> i * 8);
            }
            _data = _dataSize.Concat(data).ToArray();
        }

        public BitSequence()
        {
            endOfPaylod = false;
        }

        public bool SetBit(int bit)
        {
            return SetBit((byte)bit);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bit"> new bit to add to data array</param>
        /// <returns>true of end of payload</returns>
        public bool SetBit(byte bit)
        {
            if (getDataSize)
            {
                _dataSize[_byteCounter] = (byte)(_dataSize[_byteCounter] | bit << _bitCounter++);
                if (_bitCounter > 7)
                {
                    _bitCounter = 0;
                    _byteCounter++;
                    if (_byteCounter > 3)
                    {
                        getDataSize = false;
                        int length = 0;
                        for (int i = 0; i < _dataSize.Length; i++)
                        {
                            length = length | (_dataSize[i] << i * 8);
                        }
                        _data = new byte[length];
                        reset();
                    }
                }
            }
            else
            {
                if (!endOfPaylod)
                {
                    _data[_byteCounter] = (byte)(_data[_byteCounter] | bit << _bitCounter++);
                    if (_bitCounter > 7)
                    {
                        _bitCounter = 0;
                        _byteCounter++;
                        endOfPaylod = _byteCounter < _data.Length ? false : true;
                    }
                }
            }
            return endOfPaylod;
        }

        public byte[] GetPaylod()
        {
            return _data;
        }

        public void reset()
        {
            _bitCounter = 0;
            _byteCounter = 0;
        }
        public byte NextBit()
        {
            byte bit;
            if (endOfPaylod)
            { // pad with random data...
                bit = (byte)(random.Next() % 2);
            }
            else
            {
                bit = (byte)((_data[_byteCounter] >> _bitCounter++) & 1);
                if (_bitCounter > 7)
                {
                    _bitCounter = 0;
                    _byteCounter++;
                    endOfPaylod = _byteCounter < _data.Length ? false : true;
                }
            }
            //Debug.Write(bit + "   ");
            return bit;
        }
    }
}
