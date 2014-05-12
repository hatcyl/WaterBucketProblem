using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Solution
    {
        private readonly bool _solveable;
        public bool Solveable { get { return _solveable; } }

        private readonly List<Step> _steps;
        public List<Step> Steps { get { return _steps; } }

        internal Solution(bool solveable, List<Step> solution)
        {
            _solveable = solveable;
            _steps = solution;
        }
    }
}
