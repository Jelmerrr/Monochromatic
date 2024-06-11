using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroductionScript : MonoBehaviour
{
    public TextMeshProUGUI introText;
    public TextMeshProUGUI exploreText;
    public TextMeshProUGUI endingText;
    private int introState = 0;

    public Camera cameraView;
    GameObject tempObject;
    private Vector3 cameraOrientation;
    public GameObject playerController;
    private float storeSpeed;

    void Start()
    {
        introText.gameObject.SetActive(true);
        storeSpeed = playerController.GetComponent<MovementController>().movementSpeed;
        playerController.GetComponent<MovementController>().movementSpeed = 0f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(introState == 0)
            {
                introText.gameObject.SetActive(false);
                introState = 1;
                playerController.GetComponent<MovementController>().movementSpeed = storeSpeed;
            }
            else if(introState == 2)
            {
                cameraOrientation = cameraView.transform.forward;
                ShootNormalRaycast(cameraOrientation);
            }
            else if(introState == 3)
            {

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(introState == 1)
        {
            exploreText.gameObject.SetActive(true);
            introState = 2;
        }
    }

    private void ShootNormalRaycast(Vector3 cameraOrientation)
    {
        int layerMask = 1 << 7;
        layerMask = ~layerMask; //Layermask ignore player.
        RaycastHit hit;
        if (Physics.Raycast(playerController.transform.position, cameraOrientation, out hit, Mathf.Infinity, layerMask))
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
        if (tempObject.GetComponent<IntroductionObjectHandler>().isSecret == true)
        {
            exploreText.gameObject.SetActive(false);
            introState = 3;
            this.enabled = false;
        }
    }
}
