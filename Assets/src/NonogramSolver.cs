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
            RegularSolve(pNonogram);
        }
        else AnimatedSolve(pNonogram);

        Debug.Log(pNonogram.ToString());
    }

    // Regular solution section
    private void RegularSolve(Nonogram pNonogram)
    {
        for(int clueIndex = 0; clueIndex < rows; clueIndex++)
        {
            int[] clue = columnSpecs[clueIndex];

            
            if (clue.Length == 1 && clue[0] >= colMin)
            {
                midFill(clue[0], clueIndex);
            }
            else if ((clue.Length - 1) + clue.Sum() == rows)
            {
                segmentedFill(clue, clueIndex);
            }
        }
    }

    private void midFill(int pClue, int pColNum)
    {
        int initialIndex = rows - pClue;
        int finalIndex = rows - initialIndex - 1;

        while (initialIndex <= finalIndex)
        {
            matrix[initialIndex, pColNum] = 1;
            initialIndex++;
        }
    }

    private void segmentedFill(int[] pClue, int pColNum)
    {
        int cellIndex = 0;
        for (int segmentIndex = 0; segmentIndex < pClue.Length; segmentIndex++)
        {
            for (int segmentCount = 0; segmentCount < pClue[segmentIndex]; segmentCount++)
            {
                matrix[cellIndex, pColNum] = 1;
                cellIndex++;
            }
            cellIndex++;
        }
    }

    // Animated solution
    private void AnimatedSolve(Nonogram pNonogram)
    {
        int[][] columnSpecs = pNonogram.ColumnSpecs;
        int[][] rowSpecs = pNonogram.RowSpecs;
        int[,] matrix = pNonogram.Matrix;
        int rows = pNonogram.Rows;
        int columns = pNonogram.Columns;
        int rowMin = (rows / 2) + 1;
        int colMin = (columns / 2) + 1;
    }
}
