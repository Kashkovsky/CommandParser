using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CommandParser
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // Run app with some arguments
            if (args.Length != 0)
            {
                ProcessWithRegEx(args);
                //alternatively one can use the old method Process(args);
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
                ProcessWithRegEx(Console.ReadLine());
                //alternatively one can use the old method Process(ReadArgs());
            }
        }
        
        

        //Processes input using RegEx
        public static void ProcessWithRegEx(string input)
        {
            List<string[]> commands = new List<string[]>(); 
            string patternwithArgs = "(-[A-Za-z,;'\"\\s\\d]+)|(-\\w+)/g";
            string[] temp = Regex.Split(input, patternwithArgs, RegexOptions.IgnorePatternWhitespace);

            for (int i = 0; i < temp.Length; i++)
            {
                string command = "";
                List<string> args = new List<string>();
                if (!String.IsNullOrEmpty(temp[i]))
                {
                    string[] tempArray = temp[i].Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
                    command = tempArray[0];
                    Flag flag = new Flag(command);
                    if (tempArray.Length > 1 && !flag.IsHelp)
                    {
                        for (int j = 1; j < tempArray.Length; j++)
                        {
                            flag.commandArgs.Add(tempArray[j]);
                        }
                    }
                    else if (flag.IsHelp) {
                        for (int h = i+1; h < temp.Length; h++)
                        {
                            if (!String.IsNullOrEmpty(temp[h])) {
                                string[] helpArgs = temp[h].Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
                                flag.commandArgs.Add(helpArgs[0]);
                                i = temp.Length;
                            }  
                        }
                        flag.Do();
                        flag = null;
                    }
                    if(flag != null) flag.Do();
                }
                
            }    
        }
        public static void ProcessWithRegEx(string[]startArgs)
        {
            string input = "";
            foreach (var item in startArgs)
            {
                input += $"{item} ";
            }
            List<string[]> commands = new List<string[]>();
            string patternwithArgs = "(-[A-Za-z,;'\"\\s\\d]+)|(-\\w+)/g";
            string[] temp = Regex.Split(input, patternwithArgs, RegexOptions.IgnorePatternWhitespace);

            for (int i = 0; i < temp.Length; i++)
            {
                string command = "";
                List<string> args = new List<string>();
                if (!String.IsNullOrEmpty(temp[i]))
                {
                    string[] tempArray = temp[i].Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
                    command = tempArray[0];
                    Flag flag = new Flag(command);
                    if (tempArray.Length > 1 && !flag.IsHelp)
                    {
                        for (int j = 1; j < tempArray.Length; j++)
                        {
                            flag.commandArgs.Add(tempArray[j]);
                        }
                    }
                    else if (flag.IsHelp)
                    {
                        for (int h = i + 1; h < temp.Length; h++)
                        {
                            if (!String.IsNullOrEmpty(temp[h]))
                            {
                                string[] helpArgs = temp[h].Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
                                flag.commandArgs.Add(helpArgs[0]);
                                i = temp.Length;
                            }
                        }
                        flag.Do();
                        flag = null;
                    }
                    if (flag != null) flag.Do();
                }

            }
        }


        //The old method without RegEx
        //Processes array of arguments from ReadArgs()
        //Returns an array of arguments
        public static string[] ReadArgs()
        {
            string input = Console.ReadLine();
            string[] result = input.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
            return result;
        }
        public static void Process(string[] args)
        {
            Flag flag = null;


            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].First() == '-' || args[i].First() == '/') //detects new flag
                {
                    if (flag == null)
                    {
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

                else
                {              // if flag exists and doesn't meet another flag, it takes an argument
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
