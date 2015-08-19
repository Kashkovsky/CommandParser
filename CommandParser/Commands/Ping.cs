using System;
using System.Collections.Generic;

namespace CommandParser.Commands
{
    class Ping : Command
    {
        public Ping ()
        {
            base.receiveArgs = false;
        }
        public override void Do(IEnumerable<string> flagArguments = null)
        {
            Console.Beep();
            Console.WriteLine("Pinging...");
        }
        public override string ToString()
        {
            return "makes some noise within computer's internal speaker and prints it's visual representation";
        }
    }
}
