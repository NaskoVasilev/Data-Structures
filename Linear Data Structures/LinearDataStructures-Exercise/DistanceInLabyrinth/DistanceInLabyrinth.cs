using System;
using System.Collections.Generic;

namespace DistanceInLabyrinth
{
    class DistanceInLabyrinth
    {
        static int[,] matrix;
        static int startRow = 0;
        static int startCol = 0;
        const int blockCellValue = -1;
        const int initialCellValue = -2;

        static void Main(string[] args)
        {
            int size = int.Parse(Console.ReadLine());
            matrix = new int[size, size];
            FillMatrix();
            CalculateDisatnce();
            PrintMatrix();
        }

        private static void PrintMatrix()
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    int value = matrix[row, col];

                    if (value > 0)
                    {
                        Console.Write(value);
                    }
                    else if (value == blockCellValue)
                    {
                        Console.Write('x');
                    }
                    else if (value == initialCellValue)
                    {
                        Console.Write('*');
                    }
                    else if (value == 0)
                    {
                        Console.Write('u');
                    }
                }
                Console.WriteLine();
            }
        }

        private static void CalculateDisatnce()
        {
            matrix[startRow, startCol] = 0;
            Queue<Cell> queue = new Queue<Cell>();
            queue.Enqueue(new Cell(startRow, startCol));

            while (queue.Count > 0)
            {
                Cell currentCell = queue.Dequeue();

                CalculateCellDisatnce(-1, 0, queue, currentCell);
                CalculateCellDisatnce(1, 0, queue, currentCell);
                CalculateCellDisatnce(0, 1, queue, currentCell);
                CalculateCellDisatnce(0, -1, queue, currentCell);
            }

            matrix[startRow, startCol] = initialCellValue;
        }

        private static void CalculateCellDisatnce(int row, int col, Queue<Cell> queue, Cell cell)
        {
            int targetRow = cell.Row + row;
            int targetCol = cell.Col + col;
            if (IsInside(targetRow, targetCol) && matrix[targetRow, targetCol] == 0)
            {
                matrix[targetRow, targetCol] = matrix[cell.Row, cell.Col] + 1;
                queue.Enqueue(new Cell(targetRow, targetCol));
            }
        }

        private static bool IsInside(int row, int col)
        {
            return row >= 0 && row < matrix.GetLength(0)
                && col >= 0 && col < matrix.GetLength(1);
        }

        private static void FillMatrix()
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                char[] elements = Console.ReadLine().ToCharArray();

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    char currenElement = elements[col];

                    switch (currenElement)
                    {
                        case 'x':
                            matrix[row, col] = blockCellValue;
                            break;
                        case '*':
                            matrix[row, col] = initialCellValue;
                            startRow = row;
                            startCol = col;
                            break;
                        case '0':
                            matrix[row, col] = 0;
                            break;
                    }
                }
            }
        }

        private class Cell
        {
            public int Row { get; set; }
            public int Col { get; set; }

            public Cell(int row, int col)
            {
                Row = row;
                Col = col;
            }
        }
    }
}
