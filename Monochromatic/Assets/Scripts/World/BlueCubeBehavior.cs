using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCubeBehavior : MonoBehaviour
{
    public bool isEnabled;
    public string blueCubeID;

    public Color normalBlue;
    public Color alphaBlue;

    UnityEngine.Material materialReference;
    // Start is called before the first frame update
    void Start()
    {
        materialReference = this.GetComponent<MeshRenderer>().material;

        normalBlue = materialReference.color;
        alphaBlue = normalBlue;
        alphaBlue.a = 0f;

        RenderOutline();
        blueCubeID = this.transform.name;
    }

    public void BlueSwap()
    {
        var parentObject = this.transform.parent.gameObject;
        if (isEnabled)
        {
            if (blueCubeID == "Blue1")
            {
                GameObject newCube = parentObject.transform.Find("Blue2").gameObject;
                newCube.GetComponent<BlueCubeBehavior>().SwapState();
                newCube.GetComponent<BlueCubeBehavior>().RenderOutline();
                SwapState();
                RenderOutline();
            }
            else if (blueCubeID == "Blue2")
            {
                GameObject newCube = parentObject.transform.Find("Blue1").gameObject;
                newCube.GetComponent<BlueCubeBehavior>().SwapState();
                newCube.GetComponent<BlueCubeBehavior>().RenderOutline();
                SwapState();
                RenderOutline();
            }
        }
    }

    public void SwapState()
    {
        if(isEnabled)
        {
            isEnabled = false;
        }
        else
        {
            isEnabled = true;
        }
    }

    public void RenderOutline()
    {
        if (isEnabled)
        {         
            this.GetComponent<Outline>().enabled = false;
            this.GetComponent<BoxCollider>().enabled = true;
            materialReference.color = normalBlue;
        }
        else
        {
            this.GetComponent<Outline>().enabled = true;
            this.GetComponent<BoxCollider>().enabled = false;
            materialReference.color = alphaBlue;

        }
    }

}
