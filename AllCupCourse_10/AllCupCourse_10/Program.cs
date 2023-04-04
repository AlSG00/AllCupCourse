using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllCupCourse_10
{
    class Program
    {
        static void Main(string[] args)
        {
            int tasksCount = 29;
            int[] solveResults = new int[] { 
                0, 0, 0, 1, 1, 1, 0, 1, 1, 1,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 
                0, 1, 0, 1, 0, 1, 0, 1, 1 
            };

            int digit = 5;
            int resultSum = 0;
            foreach (int result in solveResults)
            {
                if (result == 0)
                {
                    digit = 0;
                }
                else
                {
                    if (digit == 0)
                    {
                        digit = 5;
                    }
                    else
                    {
                        digit++;
                    }
                }

                resultSum += digit;
            }

            Console.WriteLine(resultSum);
            Console.ReadLine();
        }
    }
}
