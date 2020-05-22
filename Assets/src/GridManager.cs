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
    private SpriteRenderer[,] _grid;
    
    public void InitializeGrid(int pRows, int pColumns)
    {
        SetColRowNum(pRows,pColumns);
        InitializeCells();
    }

    public void CreateGrid()
    {
        CreateBasicGrid();
        MirrorGrid();
    }
        
    public void ChangeCellSprite(int pRow, int pColumn,int pColorNum)
    {
        if (pColorNum == 1)
        {
            _grid[pRow, pColumn].color = filledCellColor;
        }
        else
        {
            _grid[pRow, pColumn].color = emptyCellColor;
        }
        
    }

    public void DeleteGrid()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
    
    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, gridSize);
    }
    
    private void SetColRowNum(int pRows, int pColumns)
    {
        _rows = pRows;
        _columns = pColumns;
    }
    
    private void InitializeCells()
    {
        _cellSize = cellPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size;
        Vector2 newCellSize = new Vector2(gridSize.x / (float)_columns, gridSize.y / (float)_rows);
        cellPrefab.transform.localScale = new Vector2(newCellSize.x / _cellSize.x,newCellSize.y / _cellSize.y);
        _cellSize = newCellSize;
        _gridOffset.x = -(gridSize.x / 2) + _cellSize.x / 2;
        _gridOffset.y = -(gridSize.y / 2) + _cellSize.y / 2;
    }
    
    private void CreateBasicGrid()
    {
        _grid = new SpriteRenderer[_rows,_columns];
        for (int row = 0; row < _rows; row++) {
            for (int column = 0; column < _columns; column++) {
                _grid[row, column] = InstantiateCell(row, column,transform.position);
            }
        }
    }
    
    private SpriteRenderer InstantiateCell(int pRow, int pColumn, Vector2 pBasePosition)
    {
        Vector2 cellPos = new Vector2(pColumn * _cellSize.x + _gridOffset.x + pBasePosition.x, pRow * _cellSize.y + _gridOffset.y + pBasePosition.y);
        GameObject cell = Instantiate(cellPrefab, cellPos, Quaternion.identity);
        cell.transform.parent = transform;
        cell.transform.name = "Cell: " + pRow + "," + pColumn;
        return cell.GetComponent<SpriteRenderer>();
    }
    
    private void MirrorGrid()
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
    
    private void SwapGridElements(int pRow1, int pColumn1, int pRow2, int pColumn2)
    {
        SpriteRenderer temp = _grid[pRow1, pColumn1];
        _grid[pRow1, pColumn1] = _grid[pRow2, pColumn2];
        _grid[pRow2, pColumn2] = temp;
    }
}
