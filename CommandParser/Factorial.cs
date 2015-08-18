using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandParser
{
    class Factorial : Command
    {
        public Factorial()
        {
            base.receiveArgs = true;
        }
        public override void Do(IEnumerable<string> flagArguments)
        {
            if (flagArguments is List<string>)
            {
                List<string> numList = flagArguments as List<string>;
                foreach (var item in numList) 
                {
                    ulong num;
                    if (ulong.TryParse(item, out num))
                    {
                        ulong result = GetFactorial(num);
                        if (result > 0) Console.WriteLine($"{num}! = {result}");
                    }
                    else Console.WriteLine($"Value {item} is not an integer. Must be an integer greater than zero.");
                }
            }
        }
        private ulong GetFactorial (ulong x)
        {
            if (x > 0)
            {
                if (x == 1) return 1;
                ulong result = GetFactorial(x - 1) * x;
                return result;
            } else if (x == 0) return 1;
            else return 0;
            
        }
        public override string ToString()
        {
            return @"calculates factorial of given number / list of numbers. Returns value within ULONG range (65! is MAX)";
        }
    }
}
