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
    }
}
