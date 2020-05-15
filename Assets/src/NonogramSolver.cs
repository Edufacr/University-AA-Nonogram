using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
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

    public void Solve(Nonogram pNonogram, NonogramPainter painter)
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

        if (painter == null)
        {
            RegularPreSolve(pNonogram);
            if (RegularSolve(pNonogram))
            {
                Debug.Log(pNonogram.ToString());
            }
            else Debug.Log("No tiene solución");
        }
        else
        {
            painter.PaintNonogram(Nonogram.GetInstance());//Polish despues
            AnimatedPreSolve(pNonogram,painter);
            AnimatedSolve(pNonogram);
        }
    }

    // Regular solution section
    private bool RegularSolve(Nonogram pNonogram)
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
            if (RegularSolve(pNonogram)) // if the 1 worked, it moves to the next cell
            {
                return true; // if the above call returned true, it found a solution
            }
        }

        // checks to see if a 0 can be placed in the empty cell found above
        if (Coherent(0, row, column))
        {
            if (RegularSolve(pNonogram)) // if the 0 worked, it moves to the next cell
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

    private void RegularPreSolve(Nonogram pNonogram)
    {
        for (int clueIndex = 0; clueIndex < _columnSpecs.Length; clueIndex++)
        {
            int[] clue = _columnSpecs[clueIndex];

            if (clue.Length == 1 && clue[0] >= _colMin)
            {
                midColumnFill(clue[0], clueIndex);
            }
            else if ((clue.Length - 1) + clue.Sum() == _rows)
            {
                segmentedColumnFill(clue, clueIndex);
            }
        }

        for (int clueIndex = 0; clueIndex < _rowSpecs.Length; clueIndex++)
        {
            int[] clue = _rowSpecs[clueIndex];

            if (clue.Length == 1 && clue[0] >= _rowMin)
            {
                midRowFill(clue[0], clueIndex);
            }
            else if ((clue.Length - 1) + clue.Sum() == _columns)
            {
                segmentedRowFill(clue, clueIndex);
            }
        }
    }

    private void midColumnFill(int pClue, int pColNum)
    {
        int initialIndex = _rows - pClue;
        int finalIndex = _rows - initialIndex - 1;

        while (initialIndex <= finalIndex)
        {
            _matrix[initialIndex, pColNum] = 1;
            initialIndex++;
        }
    }

    private void midRowFill(int pClue, int pRowNum)
    {
        int initialIndex = _columns - pClue;
        int finalIndex = _columns - initialIndex - 1;

        while (initialIndex <= finalIndex)
        {
            _matrix[pRowNum, initialIndex] = 1;
            initialIndex++;
        }
    }

    private void segmentedColumnFill(int[] pClue, int pColNum)
    {
        int cellIndex = 0;
        for (int segmentIndex = 0; segmentIndex < pClue.Length; segmentIndex++)
        {
            for (int segmentCount = 0; segmentCount < pClue[segmentIndex]; segmentCount++)
            {
                _matrix[cellIndex, pColNum] = 1;
                cellIndex++;
            }
            if (cellIndex < _rows)
            {
                _matrix[cellIndex, pColNum] = 0;
                cellIndex++;
            }
        }
    }

    private void segmentedRowFill(int[] pClue, int pRowNum)
    {
        int cellIndex = 0;
        for (int segmentIndex = 0; segmentIndex < pClue.Length; segmentIndex++)
        {
            for (int segmentCount = 0; segmentCount < pClue[segmentIndex]; segmentCount++)
            {
                _matrix[pRowNum, cellIndex] = 1;
                cellIndex++;
            }
            if (cellIndex < _columns)
            {
                _matrix[pRowNum, cellIndex] = 0;
                cellIndex++;
            }
        }
    }

    // Animated solution
    private void AnimatedPreSolve(Nonogram pNonogram,NonogramPainter pPainter)
    {
        for (int i = 0; i < pNonogram.Rows; i++)
        {
            for (int j = 0; j < pNonogram.Columns; j++)
            {
                pPainter.AddToPaintQueue(i,j);
            }
        }
    }
    private void AnimatedSolve(Nonogram pNonogram)
    {

    }
}
