using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo
{
    public class TransientExample
    {
        private static int _counter;

        private readonly int _instanceCounter;

        public TransientExample()
        {
            _instanceCounter = _counter++;
        }

        public void ShowCounter()
        {
            Console.WriteLine(_instanceCounter);
        }
    }
}