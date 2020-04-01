using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonogramPainter : MonoBehaviour
{

    public GameObject squarePrefab;
    public GameObject squareHolder; //Hay que pensar en el resto de la interfaz    
    public float squareOffset; //offset entre cuadros
    
    private int _columns;
    private int _rows;
    private GameObject[,] _grid;
    
    public void SetGridSize(int pRows, int pColumns)
    {
        _rows = pRows;
        _columns = pColumns;
    }

    public void CreateGrid()
    {
        _grid = new GameObject[_rows,_columns];
        for (int row = _rows; row > 0; row--)
        {
            for (int column = 0; column < _columns; column++)
            {
                _grid[row, column] = InstantiateSquare(row, column);
            }
        }
    }

    private GameObject InstantiateSquare(int pRow, int pColumn)
    {
        GameObject square = Instantiate(squarePrefab, GetPosition(pRow,pColumn),Quaternion.identity);
        square.transform.parent = squareHolder.transform;
        string squareName = "Square: " + pRow + "," + pColumn;
        square.transform.name = squareName;

        return square;
    }

    private Vector3 GetPosition(int pRow, int pColumn)
    {
        Vector3[] holderCorners = new Vector3[4];
        squareHolder.GetComponent<RectTransform>().GetWorldCorners(holderCorners);

        return new Vector3(pColumn, pRow);//+holder corners
    }
    
    

// Start is called before the first frame update
    void Start()
    {
        SetGridSize(2,4);
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
