using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonogramCreator : MonoBehaviour
{
    private PuzzleReader _reader;
    // Start is called before the first frame update
    void Start()
    {
        _reader = new PuzzleReader();
        CreateNonogram("Assets/Input/puzzle.txt"); //temp hasta que se vea como se llama a crear el nonogram
    }

    public bool CreateNonogram(string pPath)
    {
        if (!_reader.Read(pPath)) return false;
        
        Nonogram puzzle = Nonogram.GetInstance();
        puzzle.Matrix = new int[_reader.Rows, _reader.Columns];
        puzzle.ColumnSpecs = SpecsConvert(_reader.ColumnSpecs);
        puzzle.RowsSpecs = SpecsConvert(_reader.RowSpecs);
        return true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int[][] SpecsConvert(List<string> pSpecList)
    {
        List<int[]> retSpecs = new List<int[]>();
        foreach (string lineSpec in pSpecList) {
            string[] lineSpecGroups = lineSpec.Split(',',' ');
            List<int> specGroup = new List<int>();
            foreach (string num in lineSpecGroups)
            {
                if (int.TryParse(num, out int intNum))
                {
                    specGroup.Add(int.Parse(num));
                }
            }
            retSpecs.Add(specGroup.ToArray());
        }
        return retSpecs.ToArray();
    }
}
