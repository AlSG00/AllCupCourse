using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllCupCourse_12
{
    class Program
    {
        static void Main(string[] args)
        {
            int targetValue = 175801;

            int chestOpenCount = 20;
            int startValue = 0;
            int secondValue = 0;
            int currentValue = 0;
            int lastValue = 0;
            int beforeLastValue = 0;

            for (int i = 0; i < 1000; i++)
            {
                startValue++;

                secondValue = 0;
                for (int j = 0; j < startValue; j++)
                {
                    secondValue++;

                    lastValue = secondValue;
                    beforeLastValue = startValue;
                    currentValue = 0;
                    while (currentValue < targetValue)
                    {
                        currentValue = lastValue + beforeLastValue;
                        beforeLastValue = lastValue;
                        lastValue = currentValue;
                        Console.WriteLine(currentValue);
                    }

                    if (lastValue == targetValue)
                    {
                        break;
                    }
                }

                if (lastValue == targetValue)
                {
                    Console.Write($"Answer is {startValue} {secondValue}");
                    
                    break;
                }
            }

            Console.ReadLine();
        }
    }
}
