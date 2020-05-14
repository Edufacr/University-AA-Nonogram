using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonogramPainter : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    
    void Start()
    {
    }
    
    public void InitializePainter(Nonogram pNonogram)
    {
        gridManager.InitializeGrid(pNonogram.Rows,pNonogram.Columns);
    }
    
    public void PaintNonogram()
    {
        gridManager.CreateGrid();
    }

    public void PaintGrid(Nonogram pNonogram)
    {
        int[,] matrix = pNonogram.Matrix;
        for (int row = 0; row < pNonogram.Rows; row++)
        {
            for (int column = 0; column < pNonogram.Columns; column++)
            {
                PaintGridCell(row,column,matrix[row,column]);
            }
        }
    }

    public void PaintGridCell(int pRow, int pColumn,int pColorNum)
    {
        gridManager.ChangeCellSprite(pRow,pColumn,pColorNum);
    }
}
