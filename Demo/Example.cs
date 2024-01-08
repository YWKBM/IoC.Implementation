using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo
{
    public class ExternalDependency
    {
        public void Print()
        {
            Console.WriteLine("external dependency text");
        }
    }

    public class Example
    {
        private readonly ExternalDependency _externalDependency;

        public Example(ExternalDependency externalDependency)
        {
            _externalDependency = externalDependency;
        }

        public void Print()
        {
            _externalDependency.Print();
        }
    }
}