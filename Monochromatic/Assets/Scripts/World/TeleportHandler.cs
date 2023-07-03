using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportHandler : MonoBehaviour
{
    public bool activeTeleporter;
    public bool hasActivated;
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
            playerObject.transform.position = teleportTarget.transform.position + getRelativePosition(this.transform, playerObject.transform.position);
            hasActivated = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (activeTeleporter == true)
        {
            movementController.hasTeleported = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (hasActivated == false)
        {
            movementController.hasTeleported = false;
        }
        else hasActivated = false;
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
