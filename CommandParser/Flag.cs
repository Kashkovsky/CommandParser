using System.Collections.Generic;

namespace CommandParser
{   // Context class
    class Flag
    {
        private Command command; // remains null if CommandBuilder receives invalid argument
        private ICommandBuilder builder = new CommandBuilder();
        public bool receiveArgs { get {
                if (command != null && command.receiveArgs.HasValue) return command.receiveArgs.Value;
                else return false;
            } } 
        public bool IsHelp { get {
                if (command != null && command.IsHelp.HasValue) return command.IsHelp.Value;
                else return false;
            } }
        public List<string> commandArgs;

        //Asks CommandBuilder to create an instance for Command
        public Flag(string name)
        {
            command = builder.Create(name);
            commandArgs = new List<string>();
        }
        
        // Delegates work to Command instance
        public void Do()
        {
            if (command != null) command.Do(commandArgs);
        }
    }
}
