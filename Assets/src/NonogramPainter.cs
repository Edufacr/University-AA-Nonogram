using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class NonogramPainter : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private float animatedPaintSpeed;
    private Queue<int[]> toPaintQueue;
    
    void Start()
    {
    }
    
    public void InitializePainter(Nonogram pNonogram)
    {
        gridManager.InitializeGrid(pNonogram.Rows,pNonogram.Columns);
        toPaintQueue = new Queue<int[]>();
    }
    
    public void PaintNonogram(Nonogram pNonogram)
    {
        gridManager.CreateGrid();
        PaintGrid(pNonogram);
    }

    public void AddToPaintQueue(int pRow, int pColumn,int pColorNum)
    {
        toPaintQueue.Enqueue(new[]{pRow,pColumn,pColorNum});
    }

    public void AnimatedPaint()
    {
        gridManager.CreateGrid();
        StartCoroutine(PaintFromQueue(toPaintQueue));
    }

    private IEnumerator PaintFromQueue(Queue<int[]> pQueue)
    {
        foreach (int[] coordinates in pQueue)
        {
            yield return new WaitForSeconds(animatedPaintSpeed);
            PaintGridCell(coordinates[0], coordinates[1],coordinates[2]);
        }
    }

    private void PaintGrid(Nonogram pNonogram)
    {
        int[,] matrix = pNonogram.Matrix;
        for (int row = 0; row < pNonogram.Rows; row++)
        {
            for (int column = 0; column < pNonogram.Columns; column++)
            {
                if (matrix[row, column] == 1)
                {
                    PaintGridCell(row,column,1);
                }
            }
        }
    }

    private void PaintGridCell(int pRow, int pColumn,int pColorNum)
    {
        gridManager.ChangeCellSprite(pRow,pColumn,pColorNum);
    }
}
