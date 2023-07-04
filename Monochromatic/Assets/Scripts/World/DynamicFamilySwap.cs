using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicFamilySwap : MonoBehaviour
{
    public string insertFamilyName;

    private void OnTriggerEnter(Collider other)
    {
        dynamicFamilySwap(insertFamilyName);
    }

    public void dynamicFamilySwap(string familyName) //method to dynamically enable / disable family swap system.
    {
        ObjectColorHandler[] yep = Resources.FindObjectsOfTypeAll<ObjectColorHandler>();
        for (int i = 0; i < yep.Length; i++)
        {
            if (yep[i].hasFamilyBlock == true)
            {
                if (yep[i].familyID == familyName)
                {
                    yep[i].hasFamilyBlock = false;
                }
            }
        }
    }
}
