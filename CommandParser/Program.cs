using System;
using System.Linq;

namespace CommandParser
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // Run app with some arguments
            if (args.Length != 0)
            {
                Process(args);
            }
            //Run app with no arguments
            else
            {
                Flag flag = new Flag("-help");
                flag.Do();
            }
            //Processing user runtime arguments
            while (true)
            {
                Process(ReadArgs());
            }        
        }
        
        //Returns an array of arguments
        public static string[] ReadArgs()
        {
            string input = Console.ReadLine();
            string[] result = input.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
            return result;
        }

        //Processes array of arguments from ReadArgs()
        public static void Process(string[] args)
        {
            Flag flag = null;

          
            for (int i = 0; i < args.Length;  i++)
            {
                if (args[i].First() == '-' || args[i].First() == '/') //detects new flag
                {
                    if (flag == null) {
                        flag = new Flag(args[i]);
                        
                        if (!flag.receiveArgs || i == args.Length - 1)
                        {
                            flag.Do();
                            flag = null;
                        }
                    } 
                    else if (flag.IsHelp) // if flag is HELP
                    {
                        if (i == args.Length) flag.Do();
                        else
                        {
                            flag.commandArgs.Add(args[i]);
                            flag.Do();
                            flag = null;
                        }
                        
                    }
                    else     // active flag meets another flag
                    {
                        flag.Do();
                        flag = null;
                        flag = new Flag(args[i]);
                        if (!flag.receiveArgs)
                        {
                            flag.Do();
                            flag = null;
                        }
                    }
                }
                
                else {              // if flag exists and doesn't meet another flag, it takes an argument
                    if (flag != null)
                    {
                        flag.commandArgs.Add(args[i]);
                        if (i == args.Length - 1) flag.Do();
                    }
                }
            }  
        }
    }
}
