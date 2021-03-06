﻿using System.Collections;
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
        if (!puzzleSelected) {
            SelectPuzzle();
        }
        painter.ClearNonogram();
        solver.Solve(Nonogram.GetInstance(), painter);
        painter.PaintNonogram(Nonogram.GetInstance());
    }

    public void AnimateSolution()
    {
        if (!puzzleSelected) {
            SelectPuzzle();
        }
        painter.ClearNonogram();
        solver.Solve(Nonogram.GetInstance(), painter);
        painter.AnimatedPaint();
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
