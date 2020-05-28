using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuClicker : MonoBehaviour
{

    public Material On;
    public Material Off;
    public int FireMode=-1;

    private Renderer rend;
    private bool Selected;

    private Variables Var = Variables.getVariable();

    private int WeaponsSelected = 0;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.sharedMaterial = Off;
        Selected = false;
        if(Var.VarArray[0,FireMode]==2)
        {
            rend.sharedMaterial = On;
            Selected = true;
        }
    }

    void OnMouseDown()
    {
        WeaponsSelected = 0;
        for (int i=0; i < 10; i++)
        {
            if(Var.VarArray[0,i]==2)
            {
                WeaponsSelected++;
            }
        }

        if (WeaponsSelected <= 4)
        {
            if (Var.VarArray[0, FireMode] != 0)
            {
                if (Selected)
                {
                    rend.sharedMaterial = Off;
                    Selected = false;
                    Var.VarArray[0, FireMode] = 1;
                }
                else if(WeaponsSelected<4)
                {
                    rend.sharedMaterial = On;
                    Selected = true;
                    Var.VarArray[0, FireMode] = 2;
                }
            }
        }
    }
}
