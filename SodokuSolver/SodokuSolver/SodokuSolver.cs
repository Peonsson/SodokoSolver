using System;

/*
 * Sodoku Solver finds the solution to sodoko puzzles.
 * @author Johan Pettersson
 * @version 1.0
 * 
*/

namespace SudokuSolver
{
    class SudokuProgram
    {
        static void Main(string[] args)
        {
            int[][] puzzle = new int[9][];
            puzzle[0] = new int[] { 0, 0, 6, 2, 0, 0, 0, 8, 0 };
            puzzle[1] = new int[] { 0, 0, 8, 9, 7, 0, 0, 0, 0 };
            puzzle[2] = new int[] { 0, 0, 4, 8, 1, 0, 5, 0, 0 };
            puzzle[3] = new int[] { 0, 0, 0, 0, 6, 0, 0, 0, 2 };
            puzzle[4] = new int[] { 0, 7, 0, 0, 0, 0, 0, 3, 0 };
            puzzle[5] = new int[] { 6, 0, 0, 0, 5, 0, 0, 0, 0 };
            puzzle[6] = new int[] { 0, 0, 2, 0, 4, 7, 1, 0, 0 };
            puzzle[7] = new int[] { 0, 0, 3, 0, 2, 8, 4, 0, 0 };
            puzzle[8] = new int[] { 0, 5, 0, 0, 0, 1, 2, 0, 0 };

            Console.WriteLine("Input is: ");
            printGrid(puzzle);
            solve(puzzle);
            Console.WriteLine();
            Console.WriteLine("Output is: ");
            printGrid(puzzle);
            Console.ReadLine();
        }

        private static void solve(int[][] puzzle)
        {
            solve(puzzle, 0, 0);
        }

        private static void printGrid(int[][] grid)
        {
            int vectorLength = grid[0].Length;
            for (int r = 0; r < vectorLength; ++r)
            {
                for (int c = 0; c < vectorLength; ++c)
                {
                    if (grid[r][c] == 0)
                        Console.Write(" _");
                    else
                    {
                        Console.Write(" " + grid[r][c]);
                    }
                }
                Console.WriteLine("");
            }
        }

        private static void printGrid(int[][] grid, int row, int col)
        {
            int vectorLength = grid[0].Length;
            for (int r = 0; r < vectorLength; ++r)
            {
                for (int c = 0; c < vectorLength; ++c)
                {
                    if (grid[r][c] == 0)
                        Console.Write(" _");
                    else
                    {
                        if (r == row && col == c)
                            Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" " + grid[r][c]);
                        Console.ResetColor();
                    }
                }
                Console.WriteLine("");
            }
        }

        private static bool solve(int[][] puzzle, int row, int col)
        {
            if (row == 9)
                return true;

            if (puzzle[row][col] != 0)
                if (solve(puzzle, col == 8 ? (row + 1) : row, (col + 1) % 9))
                    return true;

            for (int number = 1; number < 10; number++)
            {
                bool val = isValidMove(puzzle, row, col, number);
                if (val)
                {
                    puzzle[row][col] = number;
                    if (solve(puzzle, col == 8 ? (row + 1) : row, (col + 1) % 9))
                        return true;
                    else
                        puzzle[row][col] = 0;
                }
            }
            return false;
        }

        private static bool isValidMove(int[][] puzzle, int row, int col, int number)
        {
            if (puzzle[row][col] == 0 && isRowUnique(puzzle, row, number) && isColumnUnique(puzzle, col, number) && isCubeUnique(puzzle, row, col, number))
                return true;
            return false;
        }

        private static bool isCubeUnique(int[][] puzzle, int row, int col, int number)
        {
            int rowStart = -1;
            int colStart = -1;

            if (row <= 2)
                rowStart = 0;
            else if (row >= 3 && row <= 5)
                rowStart = 3;
            else
                rowStart = 6;
            if (col <= 2)
                colStart = 0;
            else if (col >= 3 && col <= 5)
                colStart = 3;
            else
                colStart = 6;

            for (int i = rowStart; i < rowStart + 3; i++)
            {
                for (int j = colStart; j < colStart + 3; j++)
                {
                    if (puzzle[i][j] == number)
                        return false;
                }
            }
            return true;
        }

        private static bool isColumnUnique(int[][] puzzle, int col, int number)
        {
            int size = puzzle[0].Length;
            for (int i = 0; i < size; i++)
            {
                if (puzzle[i][col] == number)
                    return false;
            }
            return true;
        }

        private static bool isRowUnique(int[][] puzzle, int row, int number)
        {
            int size = puzzle[0].Length;
            for (int i = 0; i < size; i++)
            {
                if (puzzle[row][i] == number)
                    return false;
            }
            return true;
        }
    }
}