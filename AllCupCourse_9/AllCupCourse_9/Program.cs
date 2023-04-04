using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllCupCourse_9
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] firstTrajectory = new int[] { 2, 4, 2, 4, 5, 5, 3, 3, 4, 5, 5, 5, 1, 4, 6 };
            int[] secondTrajectory = new int[] { 2, 4, 6, 2, 1, 4, 1, 6, 1, 5, 3, 2, 3, 6, 1 };

            float firstDistance = 0f;
            foreach (int figure in firstTrajectory)
            {
                firstDistance += CalculateDistance(figure);
            }

            float secondDistance = 0f;
            foreach (int figure in secondTrajectory)
            {
                secondDistance += CalculateDistance(figure);
            }

            Console.WriteLine(firstDistance);
            Console.WriteLine(secondDistance);
            Console.ReadLine();
        }

        private static float Square(float digitToSquare)
        {
            return (float)Math.Pow(digitToSquare, 2);
        }

        private static float CalculateDistance(int type)
        {
            float pi = 3.14f;

            float smallCircleRadius = 2.228169203286535f;
            float mediumCircleRadius = 7.321127382227186f;
            float bigCircleRadius = 9.867606471697512f;

            //float smallTriangleHeight = (float)Math.Sqrt(Square(19) - Square(19 / 2)); // 16.454482671904
            //float biglTriangleHeight = (float)Math.Sqrt(Square(28) - Square(28 / 2));   //24.248711305964

            float smallTriangleHeight = 16.454482671904f;
            float biglTriangleHeight = 24.248711305964f;

            float rectangleBiggerSide = 67f;

            switch (type)
            {
                case 1:
                    return smallCircleRadius;
                    break;
                case 2:
                    return mediumCircleRadius;
                    break;
                case 3:
                    return bigCircleRadius;
                    break;
                case 4:
                    return smallTriangleHeight;
                    break;
                case 5:
                    return biglTriangleHeight;
                    break;
                case 6:
                    return rectangleBiggerSide;
                    break;
            }

            return 0;
        }


    }
}
