using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace AllCupCourse_11
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] fileData = File.ReadAllLines("data.txt");
            List<string> clearedFileData = new List<string>();
            foreach(string dataBlock in fileData)
            {
                if (dataBlock != "")
                {
                    clearedFileData.Add(dataBlock);
                }
            }

            string[] splittedData = clearedFileData[0].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            List<string> participants = new List<string>();
            List<string> circleTimeStringList = new List<string>();
            List<double> circleTimeDoubleList = new List<double>();

            int participantsCount = Convert.ToInt32(splittedData[0]);
            int circlesCount = Convert.ToInt32(splittedData[1]);
            for (int i = 1; i < participantsCount * 2 + 1; i += 2)
            {
                participants.Add(clearedFileData[i]);
                circleTimeStringList.Add(clearedFileData[i + 1]);
            }

            for (int i = 0; i < circleTimeStringList.Count; i++)
            {
                double totalTime = CalculateTotalTime(circleTimeStringList[i]);
                circleTimeDoubleList.Add(totalTime);
            }

            int bestScore = circleTimeDoubleList.IndexOf(circleTimeDoubleList.Min());
            string winnerNickname = participants[bestScore];

            Console.WriteLine(winnerNickname);
            Console.ReadLine();
        }

        private static double CalculateTotalTime(string circleTimeString)
        {
            string[] circleTimeStringList = circleTimeString.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            double totalTime = 0;
            foreach (string cirleTime in circleTimeStringList)
            {
                //totalTime += Convert.ToDouble(cirleList);
                // totalTime += float.Parse(cirleTime, CultureInfo.InvariantCulture.NumberFormat);
                totalTime += double.Parse(cirleTime, CultureInfo.InvariantCulture.NumberFormat);
            }

            return totalTime;
        }
    }
}
