using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// controls the laserpointer for teleportation
/// </summary>
public class LaserTeleport : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean teleportAction;

    public GameObject laserPrefab;
    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;

    public Transform cameraRigTransform;
    public GameObject teleportReticlePrefab;
    private GameObject reticle;
    private Transform teleportReticleTransform;
    public Transform headTransform;
    public Vector3 teleportReticleOffset;
    public LayerMask teleportMask;
    private bool shouldTeleport;

    void Start()
    {
        // setup laser
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
        reticle = Instantiate(teleportReticlePrefab);
        teleportReticleTransform = reticle.transform;
    }

    void Update()
    {
        if (teleportAction.GetState(handType))
        {
            RaycastHit hit;
            // look up where the ray hits a teleportable surface and display laser
            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100, teleportMask))
            {
                hitPoint = hit.point;
                ShowLaser(hit);
                reticle.SetActive(true);
                teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                shouldTeleport = true;
            }
        }
        else
        {
            // if teleport button is not pressed, do not display laser
            laser.SetActive(false);
            reticle.SetActive(false);
        }
        // check if there is a valid teleport point when teleport-button us released
        if (teleportAction.GetStateUp(handType) && shouldTeleport)
        {
            Teleport();
        }

    }

    /// <summary>
    /// shows laser at given hitpoint
    /// </summary>
    /// <param name="hit">lasers hitpoint</param>
    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
        laserTransform.position = Vector3.Lerp(controllerPose.transform.position, hitPoint, .5f);
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x,
                                                laserTransform.localScale.y,
                                                hit.distance);
    }

    /// <summary>
    /// moves the camerrig to the teleport position
    /// </summary>
    private void Teleport()
    {
        shouldTeleport = false;
        reticle.SetActive(false);
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        difference.y = 0;
        cameraRigTransform.position = hitPoint + difference;
    }

}