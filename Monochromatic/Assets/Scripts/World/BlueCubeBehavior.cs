using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCubeBehavior : MonoBehaviour
{
    public bool isEnabled;
    // Start is called before the first frame update
    void Start()
    {
        RenderOutline();
    }

    public void BlueSwap()
    {

    }

    void RenderOutline()
    {
        if (isEnabled)
        {
            this.GetComponent<Outline>().enabled = true;
        }
        else
        {
            this.GetComponent<Outline>().enabled = false;
        }
    }

}
