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
            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 1 << 8;

            // This would cast rays only against colliders in layer 8.
            // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
            layerMask = ~layerMask;

            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, cameraView.transform.forward, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                tempObject = hit.collider.gameObject;
                if (tempObject.GetComponent<ObjectColorHandler>().isNormal == true)
                {
                    ChangeMaterial(tempObject);
                }    
                if(tempObject.GetComponent<ObjectColorHandler>().isSpecial == true)
                {
                    //Do something with it.
                }
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
        }
    }

    private void ChangeMaterial(GameObject gameObject)
    {
        if (gameObject.GetComponent<ObjectColorHandler>().isWhite == true)
        {
            gameObject.GetComponent<MeshRenderer>().material = blackReference;
        }
        if (gameObject.GetComponent<ObjectColorHandler>().isWhite == false)
        {
            gameObject.GetComponent<MeshRenderer>().material = whiteReference;
        }
    }
}