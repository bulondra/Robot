using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot
{
    public class BytesManager
    {
        private Form1 instance;
        public BytesManager(Form1 instance)
        {
            this.instance = instance;
        }

        public bool isSet(int num, int bit) // Get state of bit
        {
            return (num & (1 << (bit - 1))) > 0;
        }

        public int setBit(int num, int bit) // Set bit on
        {
            num |= 1 << bit;

            return num;
        }

        public int clearBit(int num, int bit) // Clear bit
        {
            num &= ~(1 << bit);

            return num;
        }
    }
}
