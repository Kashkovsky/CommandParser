using System;
using System.Collections.Generic;

namespace CommandParser
{
    class Print : Command
    {
        public Print() 
        {
            base.receiveArgs = true;
        }
        public override void Do(IEnumerable<string> flagArguments)
        {
            if (flagArguments is List<string>)
            {
                List<string> printable = flagArguments as List<string>;
                if (printable.Count != 0)
                {
                    foreach (string s in printable) Console.Write("{0} ", s);
                    Console.WriteLine();
                }
                else Console.WriteLine("There's no arguments passed to the method. Suggest to call <-print> <message>");
            }
            
        }
        
        public override string ToString()
        {
            return "prints specified string to the console";
        }
    }
}
