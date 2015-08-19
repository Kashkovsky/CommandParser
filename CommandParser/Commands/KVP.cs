using System;
using System.Collections.Generic;

namespace CommandParser.Commands
{
    class KVP : Command
    {
        public KVP()
        {
            base.receiveArgs = true;
        }
        Random random = new Random();
        public override void Do(IEnumerable<string> flagArguments)
        {
            if (flagArguments is List<string>)
            {
                List<string> printable = flagArguments as List<string>;
                if (printable.Count != 0)
                {
                    Dictionary<int, string> dict = new Dictionary<int, string>();
                    int i = 0;
                    string error = "";
                    while (i < printable.Count)
                    {
                        int key1;
                        if (int.TryParse(printable[i], out key1))
                        {
                           
                           int key2;
                           if (i + 1 < printable.Count && !int.TryParse(printable[i + 1], out key2))
                                {
                                if (!dict.ContainsKey(key1)) dict.Add(key1, printable[i + 1]); 
                                else Console.WriteLine($"You entered the same Key twice : {key1}. \nOnly the first Pair within this Key will be taken.");
                                i += 2;
                            }
                           else
                                {
                                if (!dict.ContainsKey(key1)) dict.Add(key1, "null");
                                else Console.WriteLine($"You entered the same Key twice : {key1}. \nOnly the first Pair within this Key will be taken.");
                                i++;
                                } 
                        }
                        else
                        {
                            while (dict.ContainsKey(key1))
                            {
                                key1 = random.Next(int.MaxValue);
                            }
                            error += "\n" + "Argument <" + printable[i] + "> has invalid type and can't be taken as a Key.\n" +
                                           "The Key was set to random value: " + key1;
                            dict.Add(key1, printable[i]);
                            i++;
                        }

                    }
                    foreach (var item in dict)
                    {
                        Console.WriteLine("{0} - {1}", item.Key, item.Value);
                    }
                    if (!String.IsNullOrEmpty(error)) Console.WriteLine(error);
                }
                else Console.WriteLine("There's no arguments passed to the method. Suggest to call <-k> <int Key> <string Value>");
            }
        }
        public override string ToString()
        {
            return "prints a table of user specified Key-Value pairs <int, string>";
        }
    }
}
