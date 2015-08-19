using System;
using System.Collections.Generic;

namespace CommandParser.Commands
{
    class Help : Command
    {
        public Help()
        {
            base.receiveArgs = true;
            base.IsHelp = true;
        }
        public override void Do(IEnumerable<string> flagArguments)
        {
            if (flagArguments is List<string>)
            {
                List<string> helpArguments = flagArguments as List<string>;

                if (helpArguments.Count == 0)
                    Console.WriteLine("SUPPORTED COMMANDS: \n\r" + @"Use -help <-flag> or /help <-flag> or /? <-flag> to see the reference."
                                       +"\r\n\r\n" + CommandBuilder.GetCommands());
                else
                {
                    foreach (var item in flagArguments)
                    {
                        Console.WriteLine("COMMAND REFERENCE: \n\r" + CommandBuilder.GetCommand(item));
                    }
                }
            }
            
        }
        public override string ToString()
        {
            return "prints the list of supported commands to the console";
        }
    }
}
