using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportHandler : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject playerObject;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(getRelativePosition(this.transform, playerObject.transform.position));
        playerObject.transform.position = teleportTarget.transform.position + getRelativePosition(this.transform, playerObject.transform.position);
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
