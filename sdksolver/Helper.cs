namespace SudokuSolver
{
    public class Helper
    {
        public static void InitSudoku(out (int value, bool original)[,] sudoku)
        {
            sudoku = new (int value, bool original)[9, 9];
            InitializeEmptySudokuMatrix(sudoku);

            SetSudokuValue(sudoku, 0, 0, (8, true));

            SetSudokuValue(sudoku, 1, 2, (3, true));
            SetSudokuValue(sudoku, 1, 3, (6, true));

            SetSudokuValue(sudoku, 2, 1, (7, true));
            SetSudokuValue(sudoku, 2, 4, (9, true));
            SetSudokuValue(sudoku, 2, 6, (2, true));

            SetSudokuValue(sudoku, 3, 1, (5, true));
            SetSudokuValue(sudoku, 3, 5, (7, true));

            SetSudokuValue(sudoku, 4, 4, (4, true));
            SetSudokuValue(sudoku, 4, 5, (5, true));
            SetSudokuValue(sudoku, 4, 6, (7, true));

            SetSudokuValue(sudoku, 5, 3, (1, true));
            SetSudokuValue(sudoku, 5, 7, (3, true));

            SetSudokuValue(sudoku, 6, 2, (1, true));
            SetSudokuValue(sudoku, 6, 7, (6, true));
            SetSudokuValue(sudoku, 6, 8, (8, true));

            SetSudokuValue(sudoku, 7, 2, (8, true));
            SetSudokuValue(sudoku, 7, 3, (5, true));
            SetSudokuValue(sudoku, 7, 7, (1, true));

            SetSudokuValue(sudoku, 8, 1, (9, true));
            SetSudokuValue(sudoku, 8, 6, (4, true));
        }

        public static void InitSudoku2(out (int value, bool original)[,] sudoku)
        {
            sudoku = new (int value, bool original)[9, 9];
            InitializeEmptySudokuMatrix(sudoku);

            SetSudokuValue(sudoku, 0, 0, (5, true));
            SetSudokuValue(sudoku, 0, 1, (3, true));
            SetSudokuValue(sudoku, 0, 4, (7, true));

            SetSudokuValue(sudoku, 1, 0, (6, true));
            SetSudokuValue(sudoku, 1, 3, (1, true));
            SetSudokuValue(sudoku, 1, 4, (9, true));
            SetSudokuValue(sudoku, 1, 5, (5, true));

            SetSudokuValue(sudoku, 2, 1, (9, true));
            SetSudokuValue(sudoku, 2, 2, (8, true));
            SetSudokuValue(sudoku, 2, 7, (6, true));

            SetSudokuValue(sudoku, 3, 0, (8, true));
            SetSudokuValue(sudoku, 3, 4, (6, true));
            SetSudokuValue(sudoku, 3, 8, (3, true));

            SetSudokuValue(sudoku, 4, 0, (4, true));
            SetSudokuValue(sudoku, 4, 3, (8, true));
            SetSudokuValue(sudoku, 4, 5, (3, true));
            SetSudokuValue(sudoku, 4, 8, (1, true));

            SetSudokuValue(sudoku, 5, 0, (7, true));
            SetSudokuValue(sudoku, 5, 4, (2, true));
            SetSudokuValue(sudoku, 5, 8, (6, true));

            SetSudokuValue(sudoku, 6, 1, (6, true));
            SetSudokuValue(sudoku, 6, 6, (2, true));
            SetSudokuValue(sudoku, 6, 7, (8, true));

            SetSudokuValue(sudoku, 7, 3, (4, true));
            SetSudokuValue(sudoku, 7, 4, (1, true));
            SetSudokuValue(sudoku, 7, 5, (9, true));
            SetSudokuValue(sudoku, 7, 8, (5, true));

            SetSudokuValue(sudoku, 8, 4, (8, true));
            SetSudokuValue(sudoku, 8, 7, (7, true));
            SetSudokuValue(sudoku, 8, 8, (9, true));
        }

        public static void InitSudoku3(out (int value, bool original)[,] sudoku)
        {
            sudoku = new (int value, bool original)[25, 25];
            InitializeEmptySudokuMatrix(sudoku);
        }

        private static void SetSudokuValue((int value, bool original)[,] sudoku, int row,
            int column, (int value, bool original) value)
        {
            sudoku[row, column] = value;
        }

        private static void InitializeEmptySudokuMatrix((int value, bool original)[,] sudoku)
        {
            for (var row = 0; row <= sudoku.GetUpperBound(0); row++)
            {
                for (var column = 0; column <= sudoku.GetUpperBound(0); column++)
                {
                    SetSudokuValue(sudoku, row, column, (-1, false));
                }
            }
        }
    }
}