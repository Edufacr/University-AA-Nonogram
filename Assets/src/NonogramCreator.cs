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
        reader.ColumnSpecs.ForEach(Debug.Log);
        reader.RowSpecs.ForEach(Debug.Log);
        int[,] matrix = new int[reader.Rows, reader.Columns];
        int[][] columnSpecs = SpecsConvert(reader.ColumnSpecs);
        int[][] rowSpecs = SpecsConvert(reader.RowSpecs);
        foreach (int[] spec in columnSpecs)
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int[][] SpecsConvert(List<string> pList) //falta revisar si esta sirviendo
    {
        List<int[]> retSpecs = new List<int[]>();
        foreach (string stringSpecs in pList)
        {
            List<int> specContainer = new List<int>();
            foreach (char charSpec in stringSpecs)
            {
                if (char.IsDigit(charSpec)) //Hay que tomar en cuenta que pueden ser mas de 9...
                {
                    specContainer.Add((int)char.GetNumericValue(charSpec));
                }
            }
            retSpecs.Add(specContainer.ToArray());
        }
        return retSpecs.ToArray();
    }
}
