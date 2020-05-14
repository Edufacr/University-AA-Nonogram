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
    private Vector3 _squareSize;
    private GameObject[,] _grid;
    
    public void SetGridSize(int pRows, int pColumns)
    {
        _rows = pRows;
        _columns = pColumns;
        _squareSize = GetSquareSize();
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
        GameObject square = Instantiate(squarePrefab, GetPosition(pRow,pColumn),Quaternion.identity);
        //GameObject square = Instantiate(squarePrefab, squareHolder.transform);
        //square.transform.parent = squareHolder.transform;
        square.transform.localScale = _squareSize;
        square.transform.name = "Square: " + pRow + "," + pColumn;

        return square;
    }

    private Vector3 GetSquareSize()
    {
        float holderRectWidth = squareHolder.GetComponent<RectTransform>().rect.width;
        float holderRectHeight = squareHolder.GetComponent<RectTransform>().rect.height;
        return new Vector3(holderRectWidth/_columns,holderRectHeight/_rows);
    }

    private Vector3 GetPosition(int pRow, int pColumn)
    {
        float squareRadiusX = (squareHolder.GetComponent<RectTransform>().rect.width / _columns)/2;
        float squareRadiusY = (squareHolder.GetComponent<RectTransform>().rect.height / _rows)/2;
        Vector3[] holderCorners = new Vector3[4];
        
        float x = squareRadiusX * (1 + 2 * pColumn);
        float y = squareRadiusY * (1 + 2 * pRow);
        squareHolder.GetComponent<RectTransform>().GetWorldCorners(holderCorners);

        Debug.Log("Radio X: "+squareRadiusX);
        Debug.Log("Radio Y: "+squareRadiusY);
        Debug.Log("X: "+x);
        Debug.Log("Y: "+y);
        Debug.Log(holderCorners[0].ToString());
        return new Vector3(x,y)+holderCorners[0]; //0 es la esquina izquierda abajo debe cambiarse cuando se invierta la posicion (0,0)
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
