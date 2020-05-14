using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NonogramSolver
{
    private static NonogramSolver _instance;
    private static int[,] matrix;
    private static int[][] rowSpecs;
    private static int[][] columnSpecs;
    private static int rows;
    private static int columns;
    private static int rowMin;
    private static int colMin;
    private static int rowMid;
    private static int colMid;
    private static bool rowEven;
    private static bool colEven;

    private NonogramSolver()
    {

    }

    public static NonogramSolver GetInstance()
    {
        return _instance ?? (_instance = new NonogramSolver());
    }

    public void Solve(Nonogram pNonogram, bool pAnimated)
    {
        matrix = pNonogram.Matrix;
        rowSpecs = pNonogram.RowSpecs;
        columnSpecs = pNonogram.ColumnSpecs;

        rows = pNonogram.Rows;
        columns = pNonogram.Columns;

        rowMin = (columns / 2) + 1;
        colMin = (rows / 2) + 1;

        rowMid = rowMin + 1;
        colMid = colMin + 1;

        rowEven = (columns % 2 == 0 ? true : false);
        colEven = (rows % 2 == 0 ? true : false);

        if (!pAnimated)
        {
            RegularPreSolve(pNonogram);
            RegularSolve(pNonogram);
        }
        else
        {
            AnimatedPreSolve(pNonogram);
            AnimatedSolve(pNonogram);
        }

        Debug.Log(pNonogram.ToString());
    }


    // Regular solution section
    private bool RegularSolve(Nonogram pNonogram)
    {
        int [] coordinates = new int[2];
        if(!GetNextPos(coordinates)){
            return true;
        }
      
        for(int value = 1; value >= 0; value--){
            if(Coherent(value))
            {
                matrix[coordinates[0], coordinates[1]] = value;
                if(RegularSolve(pNonogram))
                {
                    return true;
                }
                matrix[coordinates[0], coordinates[1]] = -1;
            }
        }
        return false;
    }

    private bool Coherent(int pValue)
    {
        if(pValue == 1){
            //for
            // dos contadores uno para grupos y otro para los bloques 

        }

        return true;
    }

    private bool GetNextPos(int[] pCoordinates){
        for(int row = pCoordinates[0]; row < columns; row++)
        {
            for(int column = pCoordinates[1]; column < rows; columns++)
            {
                if(matrix[row,column] == -1)
                {
                    pCoordinates[0] = row;
                    pCoordinates[1] = column;
                    return true;
                }
            }
        }
        return false;
    }


    private void RegularPreSolve(Nonogram pNonogram)
    {
        for(int clueIndex = 0; clueIndex < columnSpecs.Length; clueIndex++)
        {
            int[] clue = columnSpecs[clueIndex];
            
            if (clue.Length == 1 && clue[0] >= colMin)
            {
                midColumnFill(clue[0], clueIndex);
            }
            else if ((clue.Length - 1) + clue.Sum() == rows)
            {
                segmentedColumnFill(clue, clueIndex);
            }
        }

        for (int clueIndex = 0; clueIndex < rowSpecs.Length; clueIndex++)
        {
            int[] clue = rowSpecs[clueIndex];

            if (clue.Length == 1 && clue[0] >= rowMin) 
            {
                midRowFill(clue[0], clueIndex);
            }
            else if ((clue.Length - 1) + clue.Sum() == columns) 
            {
                segmentedRowFill(clue, clueIndex);
            }
        }
    }

    private void midColumnFill(int pClue, int pColNum)
    {
        int initialIndex = rows - pClue;
        int finalIndex = rows - initialIndex - 1;

        while (initialIndex <= finalIndex)
        {
            matrix[initialIndex, pColNum] = 1;
            initialIndex++;
        }
    }

    private void midRowFill(int pClue, int pRowNum)
    {
        int initialIndex = columns - pClue;
        int finalIndex = columns - initialIndex - 1;

        while (initialIndex <= finalIndex)
        {
            matrix[pRowNum, initialIndex] = 1;
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
                matrix[cellIndex, pColNum] = 1;
                cellIndex++;
            }
            if (cellIndex < rows)
            {
                matrix[cellIndex, pColNum] = 0;
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
                matrix[pRowNum, cellIndex] = 1;
                cellIndex++;
            }
            if (cellIndex < columns)
            {
                matrix[pRowNum, cellIndex] = 0;
                cellIndex++;
            }
        }
    }

    // Animated solution
    private void AnimatedPreSolve(Nonogram pNonogram)
    {

    }


    private void AnimatedSolve(Nonogram pNonogram)
    {
       
    }
}
