using System;
using System.Collections.Generic;
using System.Linq;
using CommandParser.Commands;

namespace CommandParser
{
    class CommandBuilder : ICommandBuilder
    {
        //Concrete factory
        //Can be extended by adding a new KeyValuePair to commandList
        private static Dictionary<string, Command> commandsList = new Dictionary<string, Command>()
        {
            {"-help",   new Help() },
            {@"/?",     new Help() },
            {@"/help",  new Help() },
            {"-k",      new KVP() },
            {"-ks",     new KVP2() },
            {"-ping",   new Ping() },
            {"-print",  new Print() },
            {"-clear",  new Clear() },
            {"-fact",   new Factorial() },
            {"-rss",    new Rss() },
        };

        public Command Create(string flag)
        {
            foreach (KeyValuePair<string, Command> kvp in commandsList)
            {
                if (flag == kvp.Key) return kvp.Value;
            }
            //In case of uknown flag...
            Console.WriteLine("Command <{0}> is not supported, use CommandParser.exe /? to see the set of allowed commands.", flag);
            return null;
        }
        
        //Sorts dictionary and returns string of available commands
        public static string GetCommands()
        {
        string result = "";
        var list = commandsList.Keys.ToList();
        list.Sort();
            foreach (var item in list)
                {
                result += item;
                for (int i = item.Length; i <= 10; i++) result += " ";
                result += " - " + commandsList[item].ToString() + "\r\n";
                }
        return result;
        }
        //Returns string with reference for a command specified
        public static string GetCommand(string command)
        {
            string result = "";
            foreach (var item in commandsList)
            {
                if (item.Key == command)
                {
                    result += item.Key;
                    for (int i = item.Key.Length; i <= 10; i++) result += " ";
                    result += " - " + item.Value.ToString() + "\r\n";
                }
            }
            if (String.IsNullOrEmpty(result)) return "There's no reference for requested command in the database";
            else return result;
        }
    }
}
