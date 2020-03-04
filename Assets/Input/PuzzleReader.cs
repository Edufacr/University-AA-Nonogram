using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PuzzleReader : MonoBehaviour
{

    private int rows, columns;
    private List<string> rowSpecs, columnSpecs;

    public PuzzleReader()
    {
        rowSpecs = new List<string>();
        columnSpecs = new List<string>();
    }

    void Start()
    {
        string path = "Assets/Input/puzzle.txt";
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            string[] size = lines[0].Split(',');

            this.rows = Int32.Parse(size[0].Trim());
            this.columns = Int32.Parse(size[1].Trim());

            Debug.Log(this.rows + this.columns);
        } else Debug.Log("No se encontró el archivo necesario de nivel");
    }
}