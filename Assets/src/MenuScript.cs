using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuScript : MonoBehaviour
{
    private bool puzzleSelected;
    private NonogramCreator creator;
    private NonogramSolver solver;
    [SerializeField] private NonogramPainter painter;

    private void Start()
    {
        creator = NonogramCreator.GetInstance();
        solver = NonogramSolver.GetInstance();
        puzzleSelected = false;
    }

    public void Solution()
    {
        if (!puzzleSelected) return;
        solver.Solve(Nonogram.GetInstance(), false);
        painter.PaintGrid(Nonogram.GetInstance());
    }

    public void AnimateSolution()
    {
        if (puzzleSelected)
        {
            solver.Solve(Nonogram.GetInstance(), true);
        }
    }

    public void SelectPuzzle()
    {
        string path = EditorUtility.OpenFilePanel("", "", "txt");
        creator.CreateNonogram(path);
        painter.InitializePainter(Nonogram.GetInstance());
    }
}
