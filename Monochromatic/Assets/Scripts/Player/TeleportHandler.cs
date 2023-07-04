using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportHandler : MonoBehaviour
{
    public bool activeTeleporter;
    public bool hasActivated;
    public bool rotationDependant;
    [Header("If angle crosses 360 degree boundary -> higher rotation.y = minRotation")]
    public float minRotation;
    public float maxRotation;
    public Transform teleportTarget;
    public GameObject playerObject;
    public MovementController movementController;

    private void Start()
    {
        movementController = playerObject.GetComponent<MovementController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(activeTeleporter == true && movementController.hasTeleported == false)
        {
            if (rotationDependant == false)
            {
                playerObject.transform.position = teleportTarget.transform.position + getRelativePosition(this.transform, playerObject.transform.position);
                hasActivated = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (activeTeleporter == true && hasActivated == true)
        {
            movementController.hasTeleported = true;
        }

        if (activeTeleporter == true && movementController.hasTeleported == false)
        {
            if (rotationDependant == true && playerObject.transform.eulerAngles.y >= minRotation || playerObject.transform.eulerAngles.y <= maxRotation)
            {
                playerObject.transform.position = teleportTarget.transform.position + getRelativePosition(this.transform, playerObject.transform.position);
                hasActivated = true;
            }
        }

        if(rotationDependant == true && movementController.hasTeleported == true)
        {
            if(playerObject.transform.eulerAngles.y >= minRotation || playerObject.transform.eulerAngles.y <= maxRotation)
            {
                //This does nothing unless statement if false then reset teleporter status.
                //Yes this is really ugly but I have no clue how to otherwise make something trigger ONLY if the statement is false.
                //Negation does not give the desired result, yes I have tried it.
            }
            else
            {
                resetTeleporter();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (hasActivated == false)
        {
            resetTeleporter();
        }
        else hasActivated = false;
    }

    private void resetTeleporter()
    {
        movementController.hasTeleported = false;
    }

    public static Vector3 getRelativePosition(Transform origin, Vector3 position)
    {
        Vector3 distance = position - origin.position;
        Vector3 relativePosition = Vector3.zero;
        relativePosition.x = Vector3.Dot(distance, origin.right.normalized);
        relativePosition.y = Vector3.Dot(distance, origin.up.normalized);
        relativePosition.z = Vector3.Dot(distance, origin.forward.normalized);

        return relativePosition;
    }
}
