using System;
using System.Linq;
using System.Text;

namespace Neddle
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] bytesHaystack = { 50, 52, 51, 45, 21, 50, 52, 51, 53 };
            byte[] byteNeedle = { 50, 52, 51, 53 };
            int threshold = 3;

            for (int i = 0; i < byteNeedle.Length; i++)
            {
                //break loop if 
                if (byteNeedle.Length < threshold)
                    break;

                for (int j = 0; j < byteNeedle.Length; j++)
                {
                    long position = 0;

                    //break loop if threshold value is greater than needle length
                    if (byteNeedle.Length < threshold + j + i)
                        break;

                    //Create a subArray  from needle based on threshold value
                    var subNeedle = Helper.Subarray(byteNeedle, i, threshold + j);

                    //Check if subArray is found in haystack
                    var res = Helper.Contains(bytesHaystack, subNeedle, out position);

                    //Start Index of the needle on the haystack
                    var startIndex = (position - subNeedle.Length + 1) < 0 ? 0 : position - subNeedle.Length + 1;
                    if (res)
                        Console.WriteLine($"sequence of length = {subNeedle.Length} found at haystack offset {startIndex} and haystack end offset {position}");
                }
            }
        }
    }

    public static class Helper
    {

        public static bool Contains(this byte[] buffer, byte[] sequence, out long position)
        {
            int currOffset = 0;

            for (position = 0; position < buffer.Length; position++)
            {
                byte b = buffer[position];
                if (b == sequence[currOffset])
                {
                    if (currOffset == sequence.Length - 1) return true;
                    currOffset++;
                    continue;
                }

                if (currOffset == 0) continue;
                position -= currOffset;
                currOffset = 0;
            }

            return false;
        }

        /// <summary>
        /// Creates a copy of array from a starting index with the given lenght
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static T[] Subarray<T>(this T[] array, int startIndex, int length)
        {
            T[] result = new T[length];
            Array.Copy(array, startIndex, result, 0, length);
            return result;
        }
    }
}
