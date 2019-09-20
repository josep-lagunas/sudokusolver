namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Helper.InitSudoku(out var sudoku);
            var result = SudokuSolver.Solve(sudoku);
            SudokuSolver.PrintSudokuInConsole(sudoku, result);

            Helper.InitSudoku2(out sudoku);
            result = SudokuSolver.Solve(sudoku);
            SudokuSolver.PrintSudokuInConsole(sudoku, result);
            
            Helper.InitSudoku3(out sudoku);
            result = SudokuSolver.Solve(sudoku);
            SudokuSolver.PrintSudokuInConsole(sudoku, result);
        }
    }
}