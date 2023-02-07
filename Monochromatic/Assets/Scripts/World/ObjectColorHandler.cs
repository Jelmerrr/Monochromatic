using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectColorHandler : MonoBehaviour
{
    public bool isWhite;
    public bool isNormal;
    public bool isSpecial;
    public UnityEngine.Material materialReference;
    public string materialName;
    

    private void Start()
    {
    }
    private void Update()
    {
        materialReference = this.GetComponent<MeshRenderer>().material;
        materialName = materialReference.name;

        if (materialName == "WhiteMaterial (Instance)" || materialName == "BlackMaterial (Instance)")
        {
            isNormal = true;
        }
        else
        {
            isSpecial = true;
        }

        if (materialName == "WhiteMaterial (Instance)")
        {
            isWhite = true;
        }
        else
        {
            isWhite = false;
        }
    }
}
