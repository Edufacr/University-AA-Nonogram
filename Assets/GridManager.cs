using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Vector2 gridSize;
    private int rows;
    private int cols;
    private Vector2 gridOffset;
    private Vector2 cellSize;
    
    void Start () {
        SetColRowNum(6,6);
        InitializeCells();
        InstantiateCells();
	}

    void SetColRowNum(int pRows, int pColumns)
    {
        rows = pRows;
        cols = pColumns;
    }
    void InstantiateCells()
    {
        Vector2 transformPos = transform.position;
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < cols; col++) {
                Vector2 cellPos = new Vector2(col * cellSize.x + gridOffset.x + transformPos.x, row * cellSize.y + gridOffset.y + transformPos.y);
                GameObject cell = Instantiate(cellPrefab, cellPos, Quaternion.identity);
                cell.transform.parent = transform;
            }
        }
    }

    void InitializeCells()
    {
        cellSize = cellPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size;
        Vector2 newCellSize = new Vector2(gridSize.x / (float)cols, gridSize.y / (float)rows);
        cellPrefab.transform.localScale = new Vector2(newCellSize.x / cellSize.x,newCellSize.y / cellSize.y);
        cellSize = newCellSize;
        gridOffset.x = -(gridSize.x / 2) + cellSize.x / 2;
        gridOffset.y = -(gridSize.y / 2) + cellSize.y / 2;
    }
    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, gridSize);
    }
}
