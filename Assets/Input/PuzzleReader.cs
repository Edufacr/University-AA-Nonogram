using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PuzzleReader
{

    private int rows, columns;
    private List<string> rowSpecs, columnSpecs;
    private readonly string errorMsg = "No se encontró el archivo necesario de nivel";

    public PuzzleReader()
    {

    }

    public int Rows => rows;

    public int Columns => columns;

    public List<string> RowSpecs => rowSpecs;

    public List<string> ColumnSpecs => columnSpecs;

    public bool Read(string pPath)
    {
        if (File.Exists(pPath))
        {
            rowSpecs = new List<string>();
            columnSpecs = new List<string>();
            
            string[] lines = File.ReadAllLines(pPath);
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
            return true;
        }
        else
        {
            Debug.Log(errorMsg);
            return false;
        } 
    }
    
}