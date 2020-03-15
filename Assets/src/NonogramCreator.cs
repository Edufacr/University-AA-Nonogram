using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonogramCreator : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        PuzzleReader reader = new PuzzleReader();
        Nonogram puzzle = Nonogram.GetInstance();
        puzzle.Matrix = new int[reader.Rows, reader.Columns];
        puzzle.ColumnSpecs = SpecsConvert(reader.ColumnSpecs);
        puzzle.RowsSpecs = SpecsConvert(reader.RowSpecs);
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
