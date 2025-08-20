using System;
using System.Collections.Generic;

namespace SudokuSolver
{
    public class SudokuSolver
    {
        private static IDictionary<int, ((int row, int col) start, (int row, int col) end)>
            _boundaries;

        private const int N = 9;

        static SudokuSolver()
        {
            _boundaries = new Dictionary<int, ((int row, int col) start, (int row, int col) end)>
            {
                {0, ((0, 0), (2, 2))},
                {1, ((0, 3), (2, 5))},
                {2, ((0, 6), (2, 8))},
                {3, ((3, 0), (5, 2))},
                {4, ((3, 3), (5, 5))},
                {5, ((3, 6), (5, 8))},
                {6, ((6, 0), (8, 2))},
                {7, ((6, 3), (8, 5))},
                {8, ((6, 6), (8, 8))}
            };
        }

        private static void SetSudokuValue((int value, bool original)[,] sudoku, int row,
            int column, (int value, bool original) value)
        {
            sudoku[row, column] = value;
        }

        public static (bool solved, long attemptsCounter) Solve(
            (int value, bool original)[,] sudoku)
        {
            long attemptsCounter = 0;
            var solved = Solve(sudoku, 0, 0, ref attemptsCounter);
            return (solved, attemptsCounter);
        }

        private static bool Solve((int value, bool original)[,] sudoku, int row, int column, ref
            long attemptsCounter)
        {
            if (column == N)
            {
                return row + 1 == N || Solve(sudoku, row + 1, 0, ref attemptsCounter);
            }

            if (sudoku[row, column].value != -1)
            {
                return Solve(sudoku, row, column + 1, ref attemptsCounter);
            }

            var value = 1;

            while (value < 10)
            {
                if (RowCanBeSet(sudoku, row, value) &&
                    ColCanBeSet(sudoku, column, value) &&
                    SubMatrixCanBeSet(sudoku, row, column, value))
                {
                    SetSudokuValue(sudoku, row, column, (value, false));

                    if (Solve(sudoku, row, column + 1, ref attemptsCounter))
                    {
                        return true;
                    }

                    SetSudokuValue(sudoku, row, column, (-1, false));
                    attemptsCounter++;
                }

                value++;
            }

            return false;
        }

        private static bool SubMatrixCanBeSet((int value, bool original)[,] sudoku, int row,
            int column, int value)
        {
            var subMatrixRow = row / 3;
            var subMatrixCol = column / 3;

            var index = subMatrixRow * 3 + subMatrixCol;

            var (start, end) = _boundaries[index];

            for (var r = start.row; r <= end.row; r++)
            {
                for (var c = start.col; c <= end.col; c++)
                {
                    if (sudoku[r, c].value == value)
                        return false;
                }
            }

            return true;
        }

        private static bool ColCanBeSet((int value, bool original)[,] sudoku, int column, int value)
        {
            for (var row = 0; row < N; row++)
            {
                if (sudoku[row, column].value == value)
                    return false;
            }

            return true;
        }

        private static bool RowCanBeSet((int value, bool original)[,] sudoku, int row, int value)
        {
            for (var column = 0; column < N; column++)
            {
                if (sudoku[row, column].value == value)
                    return false;
            }

            return true;
        }

        public static void PrintSudokuInConsole((int value, bool original)[,] sudoku,
            (bool solved, long attemptsCounter) result)
        {
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(
                $"Sudoku solved: {result.solved}. Number changes attempted: {result.attemptsCounter}");
            Console.WriteLine();
            for (var row = 0; row <= sudoku.GetUpperBound(0); row++)
            {
                Console.Write("   ");
                if (row != 0 && row % 3 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.BackgroundColor = ConsoleColor.White;
                    for (var separator = 0;
                        separator <= (sudoku.GetUpperBound(0) + 1) * 3 + 1;
                        separator++)
                    {
                        Console.Write((separator + 1) % 10 == 0 ? "┼" : "─");
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.Write("   ");
                }

                for (var column = 0; column <= sudoku.GetUpperBound(0); column++)
                {
                    var field = sudoku[row, column];
                    var value = $" {field.value} ";
                    if (field.value < 0)
                    {
                        value = " x ";
                    }

                    if (column != 0 && column % 3 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write("│");
                    }

                    Console.ForegroundColor = ConsoleColor.DarkGreen;

                    Console.BackgroundColor = ConsoleColor.White;
                    if (field.original)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.Write(value);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.ResetColor();
                Console.WriteLine();
            }
            
            Console.WriteLine();
            Console.WriteLine();
            
        }
    }
}