using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectColorHandler : MonoBehaviour
{
    //Variables
    [Header("Manual variables")]
    public bool hasFamilyBlock;
    public string familyID;

    [Header("Automatic variables")]
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

    public blockType typeReference;

    private void Start()
    {
        NormalStateReload();
    }

    public void NormalStateReload()
    {

        materialReference = this.GetComponent<MeshRenderer>().material;
        materialName = materialReference.name;

        if (materialName == "WhiteMaterial (Instance)" || materialName == "BlackMaterial (Instance)")
        {
            isNormal = true;
        }

        if (isNormal == true)
        {
            if (materialName == "WhiteMaterial (Instance)")
            {
                typeReference = blockType.NormalWhite;
            }
            if(materialName == "BlackMaterial (Instance)")
            {
                typeReference = blockType.NormalBlack;
                
            }
        }
        else if (isNormal == false)
        {
            if (materialName == "BlueMaterial (Instance)")
            {
                typeReference = blockType.Blue;
            }
        }
    }
}
