using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nonogram
{
    private int[,] _matrix;
    private int[][] _columnSpecs;
    private int[][] _rowsSpecs;
    private int rows;
    private int columns;

    private static Nonogram _instance;

    private Nonogram()
    {
        _matrix = null;
        _columnSpecs = _rowsSpecs = null;
        _instance = null;
    }

    private Nonogram(int[,] matrix, int[][] columnSpecs, int[][] rowsSpecs)
    {
        _matrix = matrix;
        _columnSpecs = columnSpecs;
        _rowsSpecs = rowsSpecs;
    }

    public static Nonogram GetInstance()
    {
        return _instance ?? (_instance = new Nonogram());
    }

    public int[,] Matrix
    {
        get => _matrix;
        set => _matrix = value;
    }

    public int[][] ColumnSpecs
    {
        get => _columnSpecs;
        set => _columnSpecs = value;
    }

    public int[][] RowSpecs
    {
        get => _rowsSpecs;
        set => _rowsSpecs = value;
    }

    public int Rows
    {
        get => rows;
        set => rows = value;
    }

    public int Columns
    {
        get => columns;
        set => columns = value;
    }

    public override string ToString()
    {
        string nonogram = "";
        for (int i = 0; i < _rowsSpecs.Length; i++)
        {
            for (int j = 0; j < _columnSpecs.Length; j++)
            {
                nonogram += _matrix[i, j] + "  ";
            }
            nonogram += "\n";
        }
        return nonogram;
    }
}
