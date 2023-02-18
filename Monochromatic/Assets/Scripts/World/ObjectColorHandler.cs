using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectColorHandler : MonoBehaviour
{
    //Variables
    public bool isNormal;
    public string materialType;
    UnityEngine.Material materialReference;
    public string materialName;

    public enum blockType
    {
        NormalWhite,
        NormalBlack,
        Blue,
        Red
    }

    public blockType type;

    private void Start()
    {
        NormalStateReload();
    }
    private void Update()
    {
        if (materialName == "WhiteMaterial (Instance)" || materialName == "BlackMaterial (Instance)")
        {
            isNormal = true;
        }
    }

    public void NormalStateReload()
    {
        materialReference = this.GetComponent<MeshRenderer>().material;
        materialName = materialReference.name;
        if (materialName == "WhiteMaterial (Instance)")
        {
            type = blockType.NormalWhite;
        }
        else if(materialName == "BlackMaterial (Instance)")
        {
            type = blockType.NormalBlack;
        }
    }
}
