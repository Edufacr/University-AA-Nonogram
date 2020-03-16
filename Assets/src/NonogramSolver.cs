using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonogramSolver
{
    private static NonogramSolver _instance;

    private NonogramSolver()
    {

    }

    public static NonogramSolver GetInstance()
    {
        return _instance ?? (_instance = new NonogramSolver());
    }

    public void Solve(Nonogram pNonogram, bool pAnimated)
    {
        if (!pAnimated)
        {
            RegularSolve(pNonogram);
        }
        else AnimatedSolve(pNonogram);
    }

    private void RegularSolve(Nonogram pNonogram)
    {

    }

    private void AnimatedSolve(Nonogram pNonogram)
    {

    }
}
