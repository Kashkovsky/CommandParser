using System;
using System.Collections.Generic;

namespace CommandParser
{
    class KVP2 : Command
    {
        public KVP2()
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
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    for (int i = 0; i < printable.Count; i += 2)
                    {   
                        if (!dict.ContainsKey(printable[i]))
                        {
                            if (i + 1 < printable.Count) dict.Add(printable[i], printable[i + 1]);
                            else dict.Add(printable[i], "null");
                        }
                        else
                        {
                            Console.WriteLine($"Error: You've entered at least 2 same keys. ({printable[i]}). \n"
                                                + "Only the first Pair with this Key will be taken.");
                        }
                        
                    }
                    foreach (var item in dict)
                    {
                        Console.WriteLine("{0} - {1}", item.Key, item.Value);
                    }
                    
                }
                else Console.WriteLine("There's no arguments passed to the method. Suggest to call <-k> <int Key> <string Value>");
            }
        }
        public override string ToString()
        {
            return "prints a table of user specified Key-Value pairs <string, string>";
        }
    }
}
