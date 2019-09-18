namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var sudoku = new (int value, bool original)[9, 9];
            Helper.InitSudoku(sudoku);
            var result = SudokuSolver.Solve(sudoku);
            SudokuSolver.PrintSudokuInConsole(sudoku, result);

            Helper.InitSudoku2(sudoku);
            result = SudokuSolver.Solve(sudoku);
            SudokuSolver.PrintSudokuInConsole(sudoku, result);
        }
    }
}