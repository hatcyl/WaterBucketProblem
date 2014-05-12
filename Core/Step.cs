using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Step
    {
        private readonly int _smallBucketLevel;
        public int SmallBucketLevel { get { return _smallBucketLevel; } }

        private readonly int _largeBucketLevel;
        public int LargeBucketLevel { get { return _largeBucketLevel; } }

        private readonly Action _action;
        public Action Action { get { return _action; } }

        internal Step(int smallBucketLevel, int largeBucketLevel, Action action)
        {
            _smallBucketLevel = smallBucketLevel;
            _largeBucketLevel = largeBucketLevel;
            _action = action;
        }
    }

    public enum Action
    {
        DUMP,
        FILL,
        TRANSFER
    }
}
