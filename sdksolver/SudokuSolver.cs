using System;
using System.Collections.Generic;

namespace SudokuSolver
{
    public class SudokuSolver
    {
        private static IDictionary<int, ((int row, int col) start, (int row, int col) end)>
            _boundaries;

        private static void InitBoundaries(int dimension)
        {
            var subMatrixBySideCount = (int) Math.Sqrt(dimension);
            var offset = subMatrixBySideCount - 1;

            _boundaries = new Dictionary<int, ((int row, int col) start, (int row, int col) end)>();

            var startRow = 0;
            var startColumn = 0;
            var endRow = startRow + offset;
            var endColumn = offset;
            
            _boundaries.Add(0, ((startRow, startColumn), (endRow, endColumn)));

            for (var subMatrixIndex = 1;
                subMatrixIndex < subMatrixBySideCount * subMatrixBySideCount;
                subMatrixIndex++)
            {
                if (subMatrixIndex % subMatrixBySideCount != 0)
                {
                    startColumn = startColumn + subMatrixBySideCount;
                    endColumn = startColumn + offset;
                    _boundaries.Add(subMatrixIndex, ((startRow, startColumn), (endRow, endColumn)));
                }
                else
                {
                    startRow = startRow + subMatrixBySideCount;
                    startColumn = 0;
                    endRow = startRow + offset;
                    endColumn = startColumn + offset;
                    _boundaries.Add(subMatrixIndex, ((startRow, startColumn), (endRow, endColumn)));
                }
            }
        }

        private static void SetSudokuValue((int value, bool original)[,] sudoku, int row,
            int column, (int value, bool original) value)
        {
            sudoku[row, column] = value;
        }

        public static (bool solved, long attemptsCounter) Solve(
            (int value, bool original)[,] sudoku)
        {
            InitBoundaries(GetSudokuWith(sudoku));
            long attemptsCounter = 0;
            var solved = Solve(sudoku, 0, 0, ref attemptsCounter);
            return (solved, attemptsCounter);
        }

        private static bool Solve((int value, bool original)[,] sudoku, int row, int column, ref
            long attemptsCounter)
        {
            if (column == GetSudokuWith(sudoku))
            {
                return row + 1 == GetSudokuWith(sudoku) || Solve(sudoku, row + 1, 0, ref
                           attemptsCounter);
            }

            if (sudoku[row, column].value != -1)
            {
                return Solve(sudoku, row, column + 1, ref attemptsCounter);
            }

            var value = 1;

            while (value <= GetSudokuWith(sudoku))
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

        private static int GetSudokuWith((int value, bool original)[,] sudoku)
        {
            return sudoku.GetUpperBound(0) + 1;
        }

        private static bool SubMatrixCanBeSet((int value, bool original)[,] sudoku, int row,
            int column, int value)
        {
            var subMatrixBySideCount = (int)  Math.Sqrt(GetSudokuWith(sudoku));
            var subMatrixRow = row / subMatrixBySideCount;
            var subMatrixCol = column / subMatrixBySideCount;

            var index = subMatrixRow * subMatrixBySideCount + subMatrixCol;

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
            for (var row = 0; row < GetSudokuWith(sudoku); row++)
            {
                if (sudoku[row, column].value == value)
                    return false;
            }

            return true;
        }

        private static bool RowCanBeSet((int value, bool original)[,] sudoku, int row, int value)
        {
            for (var column = 0; column < GetSudokuWith(sudoku); column++)
            {
                if (sudoku[row, column].value == value)
                    return false;
            }

            return true;
        }

        public static void PrintSudokuInConsole((int value, bool original)[,] sudoku,
            (bool solved, long attemptsCounter) result)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(
                $"Sudoku solved: {result.solved}. Number changes attempted: {result.attemptsCounter}");
            Console.WriteLine();
            
            var subMatrixBySideCount = (int) Math.Sqrt(GetSudokuWith(sudoku) );

            for (var row = 0; row < GetSudokuWith(sudoku); row++)
            {
                if (row != 0 && row % subMatrixBySideCount == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.BackgroundColor = ConsoleColor.White;
                    for (var separator = 0;
                        separator <= GetSudokuWith(sudoku) * subMatrixBySideCount + 1;
                        separator++)
                    {
                        Console.Write((separator + 1) % 10 == 0 ? "┼" : "─");
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                }

                for (var column = 0; column < GetSudokuWith(sudoku); column++)
                {
                    var field = sudoku[row, column];
                    var value = $" {field.value} ";
                    if (field.value < 0)
                    {
                        value = " x ";
                    }

                    if (column != 0 && column %  subMatrixBySideCount == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write("|");
                    }

                    Console.ForegroundColor = ConsoleColor.White;

                    Console.BackgroundColor = ConsoleColor.White;
                    if (field.original)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.Write(value);
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine();
            }
        }
    }
}