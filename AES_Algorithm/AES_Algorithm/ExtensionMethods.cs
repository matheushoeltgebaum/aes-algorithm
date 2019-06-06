using System;
using System.Linq;

namespace AES_Algorithm
{
    public static class ExtensionMethods
    {
        public static T[] GetRow<T>(this T[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[rowNumber, x])
                .ToArray();
        }

        public static T[] GetColumn<T>(this T[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
        }

        public static T[] LeftShiftArray<T>(this T[] array, int shift)
        {
            var newArray = array;
            shift = shift % array.Length;
            T[] buffer = new T[shift];
            Array.Copy(newArray, buffer, shift);
            Array.Copy(newArray, shift, newArray, 0, newArray.Length - shift);
            Array.Copy(buffer, 0, newArray, newArray.Length - shift, shift);

            return newArray;
        }
    }
}
