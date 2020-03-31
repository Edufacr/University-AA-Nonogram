using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuScript : MonoBehaviour
{
    private void Start()
    {
        NonogramCreator.GetInstance();
    }

    public void Solution()
    {
        NonogramSolver.GetInstance().Solve(Nonogram.GetInstance(), false);
    }

    public void AnimateSolution()
    {
        Debug.Log("Imagine animated solution here");
        NonogramSolver.GetInstance().Solve(Nonogram.GetInstance(), true);
    }

    public void SelectPuzzle()
    {
        string path = EditorUtility.OpenFilePanel("", "", "txt");
        Debug.Log(path);
    }
}
