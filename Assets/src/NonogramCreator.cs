using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonogramCreator : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        PuzzleReader reader = new PuzzleReader();
        Debug.Log(reader.Columns.ToString());
        Debug.Log(reader.Rows.ToString());
        reader.ColumnSpecs.ForEach(Debug.Log);
        reader.RowSpecs.ForEach(Debug.Log);
        int[,] matrix = new int[reader.Rows, reader.Columns];
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
                if (charSpec != ',')
                {
                    specContainer.Add((int)char.GetNumericValue(charSpec));
                }
            }
            retSpecs.Add(specContainer.ToArray());
        }
        return retSpecs.ToArray();
    }
    
}
