using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuScript : MonoBehaviour
{
    private NonogramCreator creator;
    private NonogramSolver solver;

    private void Start()
    {
        creator = NonogramCreator.GetInstance();
        solver = NonogramSolver.GetInstance();
    }

    public void Solution()
    {
        solver.Solve(Nonogram.GetInstance(), false);
    }

    public void AnimateSolution()
    {
        Debug.Log("Imagine animated solution here");
        solver.Solve(Nonogram.GetInstance(), true);
    }

    public void SelectPuzzle()
    {
        string path = EditorUtility.OpenFilePanel("", "", "txt");
        creator.SetNonogramPath(path);
        Debug.Log(path);
    }
}
