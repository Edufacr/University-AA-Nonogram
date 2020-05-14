using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Vector2 gridSize;
    [SerializeField] private Color filledCellColor;
    [SerializeField] private Color emptyCellColor;
    private int _rows;
    private int _columns;
    private Vector2 _gridOffset;
    private Vector2 _cellSize;
    
    private GameObject[,] _grid;
   
    
    void Start () {
        SetColRowNum(6,6);
        InitializeCells();
        CreateGrid();
        ChangeCellSprite(0,0,1);
        ChangeCellSprite(2,2,1);
        ChangeCellSprite(3,3,0);
	}

    void MirrorGrid()
    {
        for (int row = 0; row < _rows/2; row++)
        {
            int rowToSwap = (_rows - 1) - row;
            for (int column = 0; column < _columns; column++)
            {
                SwapGridElements(row,column,rowToSwap,column);
            }
        }
    }

    void SwapGridElements(int pRow1, int pColumn1, int pRow2, int pColumn2)
    {
        GameObject temp = _grid[pRow1, pColumn1];
        _grid[pRow1, pColumn1] = _grid[pRow2, pColumn2];
        _grid[pRow2, pColumn2] = temp;
    }

    void ChangeCellSprite(int pRow, int pColumn, int pColorNum)
    {
        SpriteRenderer spriteRenderer = _grid[pRow, pColumn].GetComponent<SpriteRenderer>();
        if (pColorNum == 1)
        {
            spriteRenderer.color = filledCellColor;
        }
        else
        {
            spriteRenderer.color = emptyCellColor;
        }
            
    }
    void SetColRowNum(int pRows, int pColumns)
    {
        _rows = pRows;
        _columns = pColumns;
    }
    void CreateGrid()
    {
        _grid = new GameObject[_rows,_columns];
        for (int row = 0; row < _rows; row++) {
            for (int column = 0; column < _columns; column++) {
                _grid[row, column] = InstantiateCell(row, column,transform.position);
            }
        }
        MirrorGrid();
    }
    GameObject InstantiateCell(int pRow, int pColumn, Vector2 pBasePosition)
    {
        Vector2 cellPos = new Vector2(pColumn * _cellSize.x + _gridOffset.x + pBasePosition.x, pRow * _cellSize.y + _gridOffset.y + pBasePosition.y);
        GameObject cell = Instantiate(cellPrefab, cellPos, Quaternion.identity);
        cell.transform.parent = transform;
        cell.transform.name = "Cell: " + pRow + "," + pColumn;
        return cell;
    }
    void InitializeCells()
    {
        _cellSize = cellPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size;
        Vector2 newCellSize = new Vector2(gridSize.x / (float)_columns, gridSize.y / (float)_rows);
        cellPrefab.transform.localScale = new Vector2(newCellSize.x / _cellSize.x,newCellSize.y / _cellSize.y);
        _cellSize = newCellSize;
        _gridOffset.x = -(gridSize.x / 2) + _cellSize.x / 2;
        _gridOffset.y = -(gridSize.y / 2) + _cellSize.y / 2;
    }
    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, gridSize);
    }
}
