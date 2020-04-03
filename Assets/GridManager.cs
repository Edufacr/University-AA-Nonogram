using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private GameObject squarePrefab;
    
    private int _columns;
    private int _rows;
    [SerializeField]
    private float offset;
    
    private Vector3 _squareSize;
    
    private GameObject[,] _grid;
    
    public void SetGridSize(int pRows, int pColumns)
    {
        _rows = pRows;
        _columns = pColumns;
        transform.position = new Vector3(_columns,_rows);
    }
    
    public void CreateGrid()
    {
        _grid = new GameObject[_rows,_columns];
        for (int row = 0; row < _rows; row++)
        {
            for (int column = 0; column < _columns; column++)
            {
                _grid[row, column] = InstantiateSquare(row, column);
            }
        }
    }

    private GameObject InstantiateSquare(int pRow, int pColumn)
    {
        GameObject square = Instantiate(squarePrefab,transform);
        square.transform.name = "Square: " + pRow + "," + pColumn;
        square.transform.position = GetPosition(pRow,pColumn);
        return square;
    }

    private Vector3 GetPosition(int pRow, int pColumn)
    {
        float x = pRow * offset;
        float y = pColumn * offset;
        return new Vector3(x,y);
    }
    // Start is called before the first frame update
    void Start()
    {
        SetGridSize(10,10);
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
