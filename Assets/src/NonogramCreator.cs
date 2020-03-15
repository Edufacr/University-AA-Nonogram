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

        int[,] matrix = new int[reader.Rows, reader.Columns];
        int[][] columnSpecs = SpecsConvert(reader.ColumnSpecs);
        int[][] rowSpecs = SpecsConvert(reader.RowSpecs);
        //Hay que definir de que manera se inyectan estas variables
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
