using System.Collections.Generic;

namespace CommandParser
{
    //Abstract strategy
    abstract class Command
    {
        public bool? receiveArgs { get; protected set; }
        public bool? IsHelp      { get; protected set; }
        public abstract void Do(IEnumerable<string> flagArguments); 
    }
}
