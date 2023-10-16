using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCameraController : MonoBehaviour
{
    public Transform player_cam;
    public Transform portal;
    public Transform otherPortal;

    public bool neg;

    void LateUpdate()
    {
        Vector3 playerOffsetFromPrtal = player_cam.position - otherPortal.position; //Player camera syncing
        if (!neg)
            transform.position = portal.position + playerOffsetFromPrtal; //Negative movement
        else
            transform.position = new Vector3(portal.position.x, -portal.position.y, portal.position.z) - new Vector3(playerOffsetFromPrtal.x, -playerOffsetFromPrtal.y, playerOffsetFromPrtal.z);

        float angularDiff = Quaternion.Angle(portal.rotation, otherPortal.rotation); //Weird rotation thingy
        Quaternion portalRotDiff = Quaternion.AngleAxis(angularDiff, Vector3.up);
        Vector3 newCamDir = portalRotDiff * player_cam.forward;
        transform.rotation = Quaternion.LookRotation(newCamDir, Vector3.up);
    }
}
