using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PuzzleReader
{

    private int rows, columns;
    private List<string> rowSpecs, columnSpecs;
    private readonly string path = "Assets/Input/puzzle.txt";
    private readonly string errorMsg = "No se encontró el archivo necesario de nivel";

    public PuzzleReader()
    {
        rowSpecs = new List<string>();
        columnSpecs = new List<string>();
        
        Read();
    }

    public int Rows => rows;

    public int Columns => columns;

    public List<string> RowSpecs => rowSpecs;

    public List<string> ColumnSpecs => columnSpecs;

    void Read()
    {
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            string[] size = lines[0].Split(',');

            this.rows = Int32.Parse(size[0].Trim());
            this.columns = Int32.Parse(size[1].Trim());

            for (int index = 2; index < lines.Length; index++)
            {
                if (index < (2 + rows))
                {
                    rowSpecs.Add(lines[index]);
                }
                else if (index > (2 + rows))
                {
                    columnSpecs.Add(lines[index]);
                }
            }
        } else Debug.Log(errorMsg); 
    }
    
}