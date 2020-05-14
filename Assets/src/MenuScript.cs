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
        solver.Solve(Nonogram.GetInstance(), null);
        painter.PaintGrid(Nonogram.GetInstance());
    }

    public void AnimateSolution()
    {
        if (puzzleSelected)
        {
            solver.Solve(Nonogram.GetInstance(), painter);
        }
    }

    public void SelectPuzzle()
    {
        string path = EditorUtility.OpenFilePanel("", "", "txt");
        if (creator.CreateNonogram(path))
        {
            painter.InitializePainter(Nonogram.GetInstance());
            puzzleSelected = true;
        }
        
    }
}
