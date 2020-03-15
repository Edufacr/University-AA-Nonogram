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
