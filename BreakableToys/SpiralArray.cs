using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace BreakableToys
{
    [TestFixture]
    public class SpiralArray
    {
        [Test]
        [TestCaseSource(nameof(SpiralArrayTestCases))]
        public void SpiralArrayTest(int n, int[,] expected)
        {
            int[,] matrix = new int[n,n];
            //GreedyImplementation(n, matrix);
            RecursiveImplementation(0, 0, n, matrix, 1);

            matrix.Should().Equal(expected);
        }

        private void RecursiveImplementation(int startI, int startJ, int n, int[,] matrix, int value)
        {
            if (n == 0)
                return;
            if (n == 1)
                matrix[startI, startJ] = value;
            else
            {
                for (int i = startI, j = startJ; j < startJ + n; j++)
                {
                    matrix[i, j] = value++;
                }

                for (int i = startI + 1, j = startJ + n; i < startI + n; i++)
                {
                    matrix[i, j - 1] = value++;
                }
                
                for (int i = startI + n - 1, j = startJ + n - 1; j > startJ; j--)
                {
                    matrix[i, j - 1] = value++;
                }
                
                for (int i = startI + n - 1, j = startJ; i > startI + 1; i--)
                {
                    matrix[i - 1, j] = value++;
                }

//                PrintMatrix(matrix);
                
                RecursiveImplementation(startI + 1, startJ + 1, n - 2, matrix, value);
            }
        }


        private static void GreedyImplementation(int n, int[,] matrix)
        {
            var rowIncrementByDirection = new Dictionary<Direction, int>()
            {
                {Direction.Right, 0},
                {Direction.Down, 1},
                {Direction.Left, 0},
                {Direction.Up, -1}
            };
            var colIncrementByDirection = new Dictionary<Direction, int>()
            {
                {Direction.Right, 1},
                {Direction.Down, 0},
                {Direction.Left, -1},
                {Direction.Up, 0}
            };
            var order = new[] {Direction.Right, Direction.Down, Direction.Left, Direction.Up};
            int limit = n * n;

            int row = 0, column = 0, current = 1, orderIndex = 0;
            var currentDirection = order[orderIndex];
            while (current <= limit)
            {
                if (row < 0 || column < 0 || row >= matrix.GetLength(0) || column >= matrix.GetLength(1) ||
                    matrix[row, column] != 0)
                {
                    row -= rowIncrementByDirection[currentDirection];
                    column -= colIncrementByDirection[currentDirection];
                    orderIndex = orderIndex + 1;
                    currentDirection = order[orderIndex % order.Length];
                    row += rowIncrementByDirection[currentDirection];
                    column += colIncrementByDirection[currentDirection];
                }

                matrix[row, column] = current++;
                row += rowIncrementByDirection[currentDirection];
                column += colIncrementByDirection[currentDirection];
                //PrintMatrix(matrix);
            }
        }

        static IEnumerable<object[]> SpiralArrayTestCases
        {
            get
            {
                yield return new object[] {2, new [,] {{1,2}, {4,3}}};
                yield return new object[] {3, new [,] {{1,2,3}, {8,9,4}, {7,6,5}}};
                yield return new object[] {4, new [,] {{1,2,3,4}, {12,13,14,5}, {11,16,15,6}, {10,9,8,7}}};
            }
        }

        static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i,j]}");
                }

                Console.WriteLine();
            }
        }
    }

    public enum Direction
    {
        Right,
        Down,
        Left,
        Up
    }
}