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

    private Vector3 cameraOrientation;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cameraOrientation = cameraView.transform.forward;
            ShootNormalRaycast(cameraOrientation);

        }
    }

    private void ShootNormalRaycast(Vector3 cameraOrientation)
    {
        int layerMask = 1 << 7;
        layerMask = ~layerMask; //Layermask ignore player.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, cameraOrientation, out hit, Mathf.Infinity, layerMask))
        {
            tempObject = hit.collider.gameObject;
            HitLogic(tempObject, hit);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
            Debug.Log("Did not Hit");
        }
    }

    private void ShootPortalRaycast(Vector3 portalPos, Vector3 cameraOrientation)
    {
        int layerMask = 1 << 6 | 1 << 7;
        layerMask = ~layerMask; //Layermask ignore player and portals.
        RaycastHit hit;
        if (Physics.Raycast(portalPos, cameraOrientation, out hit, Mathf.Infinity, layerMask))
        {
            tempObject = hit.collider.gameObject;
            HitLogic(tempObject, hit);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
            Debug.Log("Did not Hit");
        }
    }

    private void HitLogic(GameObject tempObject, RaycastHit hit)
    {
        if (tempObject.GetComponent<ObjectColorHandler>().isPortal == true) //Handles portal warping shenanigans.
        {
            string portalID = tempObject.GetComponent<ObjectColorHandler>().portalID;
            ObjectColorHandler[] objectResource = Resources.FindObjectsOfTypeAll<ObjectColorHandler>();
            for (int i = 0; i < objectResource.Length; i++)
            {
                if (objectResource[i].isPortal == true)
                {
                    if (objectResource[i].portalID == portalID)
                    {
                        Transform locationPortal = objectResource[i].gameObject.GetComponent<BoxCollider>().transform;
                        if (locationPortal != tempObject.GetComponent<BoxCollider>().transform)
                        {
                            Vector3 relativePos = tempObject.GetComponent<BoxCollider>().transform.position - hit.point;
                            Vector3 newPos = locationPortal.position - relativePos;
                            locationPortal.GetComponent<BoxCollider>().enabled = false;
                            ShootPortalRaycast(newPos, cameraOrientation);
                            locationPortal.GetComponent<BoxCollider>().enabled = true;
                        }
                    }
                }
            }
        }
        else if (tempObject.GetComponent<ObjectColorHandler>().isNormal == true) //If object shot is white/black handle normal procedure.
        {
            ChangeMaterial(tempObject);
        }
        else if (tempObject.GetComponent<ObjectColorHandler>().isNormal == false) //Add special block handlers below here.
        {
            if (tempObject.GetComponent<ObjectColorHandler>().typeReference == ObjectColorHandler.blockType.Blue) //Blue object handler.
            {
                tempObject.GetComponent<BlueCubeBehavior>().BlueSwap();
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

            if(gameObject.GetComponent<ObjectColorHandler>().hasFamilyBlock == true) //Family swap handling
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

            if (gameObject.GetComponent<ObjectColorHandler>().hasFamilyBlock == true) //Family swap handling
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