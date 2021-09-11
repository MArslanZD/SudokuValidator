using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku_AlgoTest
{
    class Program
    {
        static void Main(string[] args)
        {

            // Test Inputs
            #region Inputs
            int[][] goodSudoku1 = {
                new int[] {7,8,4,  1,5,9,  3,2,6},
                new int[] {5,3,9,  6,7,2,  8,4,1},
                new int[] {6,1,2,  4,3,8,  7,5,9},

                new int[] {9,2,8,  7,1,5,  4,6,3},
                new int[] {3,5,7,  8,4,6,  1,9,2},
                new int[] {4,6,1,  9,2,3,  5,8,7},

                new int[] {8,7,6,  3,9,4,  2,1,5},
                new int[] {2,4,3,  5,6,1,  9,7,8},
                new int[] {1,9,5,  2,8,7,  6,3,4}
            };


            int[][] goodSudoku2 = {
                new int[] {1,4, 2,3},
                new int[] {3,2, 4,1},

                new int[] {4,1, 3,2},
                new int[] {2,3, 1,4}
            };

            int[][] badSudoku1 =  {
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9}
            };

            int[][] badSudoku2 = {
                new int[] {1,2,3,4,5},
                new int[] {1,2,3,4},
                new int[] {1,2,3,4},
                new int[] {1}
            };

            #endregion

            // Injecting Sudoku arrays onto the SudokuValidation class.
            var _SudokuValidate1 = new SudokuValidation(goodSudoku1);
            var _SudokuValidate2 = new SudokuValidation(goodSudoku2);
            var _SudokuValidate3 = new SudokuValidation(badSudoku1);
            var _SudokuValidate4 = new SudokuValidation(badSudoku2);

            // Calling the validation function against each sudoku instance.
            Console.WriteLine(_SudokuValidate1.StartValidation() ? "This is a valid Sudoku." : "This is invalid Sudoku.");
            Console.WriteLine(_SudokuValidate2.StartValidation() ? "This is a valid Sudoku." : "This is invalid Sudoku.");
            Console.WriteLine(_SudokuValidate3.StartValidation() ? "This is a valid Sudoku." : "This is invalid Sudoku.");
            Console.WriteLine(_SudokuValidate4.StartValidation() ? "This is a valid Sudoku." : "This is invalid Sudoku.");
            Console.ReadLine();
        }
    }

    /// <summary>
    ///  Exelia Tech Sudoku Test
    ///  Class that finds wether a given jagged array is a proper Sudoku or not.
    ///  Created by: Arslan Zahid
    /// </summary>

    public class SudokuValidation
    {
        int[][] _SudokoGrid;

        public SudokuValidation(int[][] sudokoGrid) => _SudokoGrid = sudokoGrid;

        public bool StartValidation() =>
                IsSudokuGridValid()
                && IsRowValid()
                && IsColumnValid()
                && IsSquareValid();


        // Check Sudoko Grid's Length to be 9x9 or 4x4
        bool IsSudokuGridValid() =>
            _SudokoGrid.Length.Equals(9) && _SudokoGrid[0].Length.Equals(9)  // Return true when length is 9x9
            || (_SudokoGrid.Length.Equals(4) && _SudokoGrid[0].Length.Equals(4));  // Return true when length is 4x4

        // Validate Row
        # region Row
        bool IsRowValid() =>
                 Validate(FetchRowNum);

        int FetchRowNum(int row, int column) =>
                _SudokoGrid[row][column];
        #endregion

        // Validate Column
        #region Column
        bool IsColumnValid() =>
                Validate(FetchColNum);
        int FetchColNum(int row, int column) =>
                _SudokoGrid[column][row];
        #endregion

        // Validate Square
        #region Squares
        bool IsSquareValid() =>
            Validate(FetchSquareNum);
        int FetchSquareNum(int block, int index)
        {
            var column = _SudokoGrid.Length.Equals(9) ? 3 * (block % 3) + index % 3 : 2 * (block % 2) + index % 2;
            var row = _SudokoGrid.Length.Equals(9) ? index / 3 + 3 * (block / 3) : index / 2 + 2 * (block / 2);
            return _SudokoGrid[row][column];
        }
        #endregion

        // Validate Sudoku
        bool Validate(Func<int, int, int> _getNumber)
        {
            for (int row = 0; row < _SudokoGrid.Length; row++)
            {
                bool[] usedNumbers = new bool[10];
                for (int column = 0; column < _SudokoGrid.Length; column++)
                {
                    int number = _getNumber(row, column);
                    if (!number.Equals(0) && usedNumbers[number].Equals(true))
                        return false;

                    usedNumbers[number] = true;
                }
            }

            return true;
        }


    }
}
