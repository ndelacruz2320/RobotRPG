using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private Variables Var = Variables.getVariable();

    void OnMouseDown()
    {
        Var = Variables.remake();
        SceneManager.LoadScene("Project");
    }
}