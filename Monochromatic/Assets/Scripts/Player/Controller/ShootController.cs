using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootController : MonoBehaviour
{
    public Camera cameraView;
    GameObject tempObject;

    public Material whiteReference;
    public Material blackReference;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int layerMask = 1 << 8;
            layerMask = ~layerMask; //Layermask ignore player.
            RaycastHit hit;
            if (Physics.Raycast(transform.position, cameraView.transform.forward, out hit, Mathf.Infinity, layerMask))
            {
                tempObject = hit.collider.gameObject;
                if (tempObject.GetComponent<ObjectColorHandler>().isNormal == true) //If object shot is white/black handle normal procedure.
                {
                    ChangeMaterial(tempObject);
                }    
                else if(tempObject.GetComponent<ObjectColorHandler>().isNormal == false) //Add special block handlers below here.
                {
                    if(tempObject.GetComponent<ObjectColorHandler>().typeReference == ObjectColorHandler.blockType.Blue) //Blue object handler.
                    {
                        tempObject.GetComponent<BlueCubeBehavior>().BlueSwap();
                    }              
                }
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
        }
    }

    private void ChangeMaterial(GameObject gameObject) //Handles black and white swapping.
    {
        string typeRef = gameObject.GetComponent<ObjectColorHandler>().typeReference.ToString();
        
        if (typeRef == "NormalWhite")
        {
            if (gameObject.GetComponent<ObjectColorHandler>().hasFamilyBlock == false)
            {
                gameObject.GetComponent<MeshRenderer>().material = blackReference;
                gameObject.GetComponent<ObjectColorHandler>().NormalStateReload();
            }

            if(gameObject.GetComponent<ObjectColorHandler>().hasFamilyBlock == true)
            {
                string shotFamily = gameObject.GetComponent<ObjectColorHandler>().familyID;
                ObjectColorHandler[] yep = Resources.FindObjectsOfTypeAll<ObjectColorHandler>();         
                for (int i = 0; i < yep.Length; i++)
                {
                    if(yep[i].hasFamilyBlock == true)
                    {
                        if(yep[i].familyID == shotFamily)
                        {
                            yep[i].gameObject.GetComponent<MeshRenderer>().material = blackReference;
                            yep[i].gameObject.GetComponent<ObjectColorHandler>().NormalStateReload();
                        }
                    }
                }
            }
        }
        if (typeRef == "NormalBlack")
        {
            if (gameObject.GetComponent<ObjectColorHandler>().hasFamilyBlock == false)
            {
                gameObject.GetComponent<MeshRenderer>().material = whiteReference;
                gameObject.GetComponent<ObjectColorHandler>().NormalStateReload();
            }
            if (gameObject.GetComponent<ObjectColorHandler>().hasFamilyBlock == true)
            {
                string shotFamily = gameObject.GetComponent<ObjectColorHandler>().familyID;
                ObjectColorHandler[] yep = Resources.FindObjectsOfTypeAll<ObjectColorHandler>();
                for (int i = 0; i < yep.Length; i++)
                {
                    if (yep[i].hasFamilyBlock == true)
                    {
                        if (yep[i].familyID == shotFamily)
                        {
                            yep[i].gameObject.GetComponent<MeshRenderer>().material = whiteReference;
                            yep[i].gameObject.GetComponent<ObjectColorHandler>().NormalStateReload();
                        }
                    }
                }
            }
        }
    }
}