using System;
using System.Collections.Generic;

namespace CommandParser.Commands
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
