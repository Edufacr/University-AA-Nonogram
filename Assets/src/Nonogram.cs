using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nonogram
{
    private int[,] _matrix;
    private int[][] _columnSpecs;
    private int[][] _rowsSpecs;

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

    public int[][] RowsSpecs
    {
        get => _rowsSpecs;
        set => _rowsSpecs = value;
    }
}
