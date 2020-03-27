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
        //SceneManager.LoadScene("");
        //instead of specifying scene name, one can do SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log("Imagine solution here");
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
