using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandParser
{
    class Clear : Command
    {
        public Clear ()
        {
            base.receiveArgs = false;

        }
        public override void Do(IEnumerable<string> flagArguments = null)
        {
            Console.Clear();
        }
        public override string ToString()
        {
            return "clears entire screen";
        }
    }
}
