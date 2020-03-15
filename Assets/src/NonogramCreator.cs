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
        
        
        /*foreach (int[] spec in columnSpecs)
        {
            foreach (int vari in spec)
            {
                Debug.Log(vari);
            }
        }
        foreach (int[] spec in rowSpecs)
        {
            foreach (int vari in spec)
            {
                Debug.Log(vari);
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int[][] SpecsConvert(List<string> pSpecList) //falta revisar si esta sirviendo
    {
        List<int[]> retSpecs = new List<int[]>();
        foreach (string lineSpec in pSpecList) {
            Debug.Log("New Line");
            Debug.Log(lineSpec);
            string[] lineSpecGroups = lineSpec.Split(',');
            
            List<int> specGroup = new List<int>();
            Debug.Log("Group");
            foreach (string num in lineSpecGroups)
            {
                Debug.Log("Itera");
                Debug.Log(num);
                //specGroup.Add(int.Parse(num));
            }
            
            retSpecs.Add(specGroup.ToArray());
        }
        return retSpecs.ToArray();
    }
}
