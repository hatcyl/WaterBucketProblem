using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal class Bucket
    {
        private readonly int _capacity;
        public int Capacity { get { return _capacity; } }

        public int Level { get; private set; }
        public int Remaining { get { return Capacity - Level; } }

        public bool IsEmpty { get { return Level == 0; } }
        public bool IsFull { get { return Remaining == 0; } }

        public Bucket(int capacity)
        {
            _capacity = capacity;
        }

        public void Fill()
        {
            Level = Capacity;
        }

        public void Dump()
        {
            Level = 0;
        }

        public void TransferTo(Bucket bucket)
        {
            int transfer = 0;

            if(this.Level <= bucket.Remaining)
            {
                transfer = this.Level; 
            }

            else if(this.Level > bucket.Remaining)
            {
                transfer = bucket.Remaining;
            }

            bucket.Level += transfer;
            this.Level -= transfer;
        }
    }
}
