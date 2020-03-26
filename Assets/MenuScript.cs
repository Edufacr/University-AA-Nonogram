using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuScript : MonoBehaviour
{
    public void Solution()
    {
        //SceneManager.LoadScene("");
        //instead of specifying scene name, one can do SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log("Imagine solution here");
        NonogramCreator.GetInstance();
        Nonogram nono = Nonogram.GetInstance();
        string cols = "";
        string rows = "";
        string nonogram = "";
        int rowNum = nono.RowSpecs.Length;
        int colNum = nono.ColumnSpecs.Length;
        int[,] nonoArr = nono.Matrix;
        foreach (int[] i in nono.ColumnSpecs)
        {
            foreach (int j in i)
            { 
                cols += j.ToString() + ", ";
            }
            cols += "\n";
        }

        foreach (int[] i in nono.RowSpecs)
        {
            foreach (int j in i)
            {
                rows += j.ToString() + ", ";
            }
            rows += "\n";
        }

        for (int i = 0; i < rowNum; i++)
        {
            for (int j = 0; j < colNum; j++)
            {
                nonogram += nonoArr[i, j] + "  ";
            }
            nonogram += "\n";
        }

        Debug.Log(rows);
        Debug.Log(cols);
        Debug.Log(nonogram);
    }


    public void AnimateSolution()
    {
        Debug.Log("Imagine animated solution here");
    }


    public void SelectPuzzle()
    {
        string path = EditorUtility.OpenFilePanel("", "", "txt");
        Debug.Log(path);
    }
}
