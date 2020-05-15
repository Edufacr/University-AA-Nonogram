using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class NonogramSolver
{
    private static NonogramSolver _instance;
    private static int[,] _matrix;
    private static int[][] _rowSpecs;
    private static int[][] _columnSpecs;
    private static int _rows;
    private static int _columns;
    private static int _rowMin;
    private static int _colMin;
    private static int _rowMid;
    private static int _colMid;
    private static bool _rowEven;
    private static bool _colEven;
    

    private NonogramSolver()
    {

    }

    public static NonogramSolver GetInstance()
    {
        return _instance ?? (_instance = new NonogramSolver());
    }

    public void Solve(Nonogram pNonogram, NonogramPainter pPainter)
    {
        // calculates different parameters needed for presolve and/or backtracking solve
        _matrix = pNonogram.Matrix;
        _rowSpecs = pNonogram.RowSpecs;
        _columnSpecs = pNonogram.ColumnSpecs;

        _rows = pNonogram.Rows;
        _columns = pNonogram.Columns;

        _rowMin = (_columns / 2) + 1;
        _colMin = (_rows / 2) + 1;

        _rowMid = _rowMin + 1;
        _colMid = _colMin + 1;

        _rowEven = (_columns % 2 == 0 ? true : false);
        _colEven = (_rows % 2 == 0 ? true : false);
        
        PreSolve(pNonogram,pPainter);
        SolveAlg(pNonogram,pPainter);
    }

    //  solution section
    private bool SolveAlg(Nonogram pNonogram,NonogramPainter pPainter)
    {
        int row = -1; // starts with a placeholder
        int column = -1; // starts with a placeholder
        bool finished = true;

        for (int iRow = 0; iRow < _rows; iRow++)
        {
            for (int jCol = 0; jCol < _columns; jCol++)
            {
                if (_matrix[iRow, jCol] == -1) // both for's look for the next empty cell
                {
                    row = iRow;
                    column = jCol;
                    finished = false;
                    break;
                }
            }

            if (!finished) // if something was found, it needs to break out of this for loop too
            {
                break;
            }
        }

        // finished checking all of the cells
        if (finished)
        {
            return true;
        }

        // checks to see if a 1 can be placed in the empty cell found above
        if (Coherent(1, row, column))
        {
            if (SolveAlg(pNonogram,pPainter)) // if the 1 worked, it moves to the next cell
            {
                return true; // if the above call returned true, it found a solution
            }
        }

        // checks to see if a 0 can be placed in the empty cell found above
        if (Coherent(0, row, column))
        {
            if (SolveAlg(pNonogram,pPainter)) // if the 0 worked, it moves to the next cell
            {
                return true;
            }
        }

        _matrix[row, column] = -1; // if neither the 1 or the 0 worked, it restores the cell
        return false; // this activates the backtracking
    }

    private bool Coherent(int pValue, int pRow, int pCol)
    {
        // places the value (1 or 0) in the specified coordinate
        _matrix[pRow, pCol] = pValue;
        // checks if the move works for the row
        if (CheckLine(pCol, _columns, _rowSpecs[pRow], i => _matrix[pRow, i]))
        {
            // if it works, then it checks the column
            // if the column works, it means that the move works completely, so it automatically returns true. Else, it will return false
            return CheckLine(pRow, _rows, _columnSpecs[pCol], i => _matrix[i, pCol]);
        }
        // since the move didn't work for the row, we don't need to check the column, so we return false
        return false;
    }

    private bool CheckLine(int pCurrentPosition, int pSize, int[] specs, Func<int, int> getter)
    {
        int blockCount = 0; // keeps track of the amount of line blocks in the line
        int count = 0; // keeps track of the block size
        bool building = false; // true if the last move was a 1

        // checks everything up to (and including) pCurrentPosition
        for (int i = 0; i <= pCurrentPosition; i++)
        {
            if (getter(i) == 1) //if the cell has a value of one
            {
                count++; //we increase the size of the current block by one
                if (building) //if the cell at i-1 was a 1
                {
                    if (count > specs[blockCount]) //checks to see if this current 1 makes the block bigger than specified
                    {
                        return false;
                    }
                }
                else if (blockCount > specs.Length - 1) // if the previous move was a 0, we check to see if this new block being started makes more blocks in the line than specified
                {
                    return false;
                }
                building = true; // if everything seems allright, we keep log that we placed a 1
            }
            else
            {
                if (building) //if the cell at i-1 was a 1
                {
                    if (count != specs[blockCount]) // checks to see if we are making the block shorter than specified
                    {
                        return false;
                    }
                    count = 0; // reset the block counter, since we are placing a 0, hence ending the previous block
                    blockCount++; // we count the block that has been finished by this 0
                }
                building = false; // if everything seems allright, we keep log that we placed a 0
            }
        }

        // if the line has been finished, we check to see if its requirements were met
        // since some checking for the move i isn't checked until move 1+1, and there will be no i+1 at the end of the line
        // we need to manually do the final check here
        if (pCurrentPosition == pSize - 1)
        {
            if (building) //this means that the last piece placed was a 1
            {
                // checks if the last block equals the specifiec length and if we made enough blocks
                return (count == specs[blockCount]) & (blockCount == specs.Length - 1);
            }
            else // else, this means that the last piece placed was a 0
            {
                // checks to see if we made enough blocks. No -1 because an extra one was added by the last run of the for loop
                return blockCount == specs.Length;
            }
        }
        /*
        Si voy a poner un 1, tengo que chequear
            - que no esté haciendo el bloque más grande de la cuenta
            - no hacer más bloques de la cuenta
        Si voy a poner un 0, tengo que chequear
            - que no esté terminando un bloque antes de tiempo
        Si ya terminé, tengo que
            - revisar que todo se completó bien
            - si lo último fue un 1, reviso el tamaño del bloque y la cantidad de bloques
            - si lo último que puse fue un 0 (!building), tengo que revisar que tuve todos los bloques
        */

        return true; // if the row hasn't ended and it got to this point, it means that the move is coherent in the given context
    }

    private void PreSolve(Nonogram pNonogram,NonogramPainter pPainter)
    {
        for (int clueIndex = 0; clueIndex < _columnSpecs.Length; clueIndex++) // goes through all of the columns
        {
            int[] clue = _columnSpecs[clueIndex];

            if (clue.Length == 1 && clue[0] >= _colMin) // if there is only one block in the line and it is big enough, we can fill from the center
            {
                midFill(clue[0], _rows, i => {_matrix[i, clueIndex] = 1; pPainter.AddToPaintQueue(i,clueIndex);}); // we pass rows as length for filling columns, since the amount of cells in a column == number of rows
                // midFill only places 1s and uses a fixed column here, so only the parameter for the row is needed
                // the action passed as a parameter also adds the move to the painter in case the animated solution is wanted
            }
            else if ((clue.Length - 1) + clue.Sum() == _rows) // if the sum of the clues plus the spaces needed to separate the blocks equal the length of the column
            {
                segmentedFill(clue, _rows, (row, num) => {_matrix[row, clueIndex] = num; pPainter.AddToPaintQueue(row, clueIndex);}); // we pass rows as length for filling columns, since the amount of cells in a column == number of rows
                // num parameter depends on what segmentedFill needs to place (1 or 0)
                // column is fixed (clueIndex), the row (which is passed as a parameter in the above Action, changes
                // the action passed as a parameter also adds the move to the painter in case the animated solution is wanted
            }
        }

        for (int clueIndex = 0; clueIndex < _rowSpecs.Length; clueIndex++) // goes through all of the rows
        {
            int[] clue = _rowSpecs[clueIndex];

            if (clue.Length == 1 && clue[0] >= _rowMin) // if there is only one block in the line and it is big enough, we can fill from the center
            {
                midFill(clue[0], _columns, i => {_matrix[clueIndex, i] = 1; pPainter.AddToPaintQueue(clueIndex,i);}); // we pass columns as length for filling rows, since the amount of cells in a row == number of columns
                // midFill only places 1s and uses a fixed row here, so only the parameter for the column is needed
                // the action passed as a parameter also adds the move to the painter in case the animated solution is wanted
            }
            else if ((clue.Length - 1) + clue.Sum() == _columns) // if the sum of the clues plus the spaces needed to separate the blocks equal the length of the column
            {
                segmentedFill(clue, _columns, (column, num) => {_matrix[clueIndex, column] = num; pPainter.AddToPaintQueue(clueIndex,column);}); // we pass columns as length for filling rows, since the amount of cells in a row == number of columns
                // num parameter depends on what segmentedFill needs to place (1 or 0)
                // row is fixed (clueIndex), the column (which is passed as a parameter in the above Action, changes
                // the action passed as a parameter also adds the move to the painter in case the animated solution is wanted
            }
        }
    }

    private void midFill(int pClue, int pLineLength, Action<int> filler)
    {
        int initialIndex = pLineLength - pClue;
        int finalIndex = pLineLength - initialIndex - 1;
        // these two indices make up the length specified by the clue and places the block in the middle of the line

        // iterates through the line in the range specified above
        while (initialIndex <= finalIndex)
        {
            filler(initialIndex); // puts a 1 in the cell
            initialIndex++;
        }
    }

    private void segmentedFill(int[] pClue, int pLineLength, Action<int, int> filler)
    {
        int cellIndex = 0;
        // outer loop to iterate over the specs
        for (int segmentIndex = 0; segmentIndex < pClue.Length; segmentIndex++)
        {
            //nested loop to iterate over the line for the length specified by each clue
            for (int segmentCount = 0; segmentCount < pClue[segmentIndex]; segmentCount++)
            {
                filler(cellIndex, 1); // writes a one on the specified cell
                cellIndex++;
            }
            if (cellIndex < pLineLength) // to avoid IndexOutOfRange
            {
                filler(cellIndex, 0); // places a 0 to separate each block
                cellIndex++;
            }
        }
    }
}
