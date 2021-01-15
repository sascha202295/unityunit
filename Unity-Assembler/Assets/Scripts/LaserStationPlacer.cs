using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// allows placement of stations
/// </summary>
public class LaserStationPlacer : MonoBehaviour
{
    public SteamVR_Input_Sources mTargetSource;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean mPlaceStationAction;
    public SteamVR_Action_Boolean mRotateStationRight;
    public SteamVR_Action_Boolean mRotateStationLeft;
    public GameObject mUIPointer;
    public float StationPlacementRotationSpeed = 1.0f;
    private bool enableStationPlacement = false;

    public GameObject laserPrefab;
    private GameObject laser;
    private Transform laserTransform;

    public Vector3 stationReticleOffset;
    public LayerMask StationPlacementMask;

    private GameObject stationPreview;
    private Station station;

    void Start()
    {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
        laser.GetComponent<Renderer>().material.color = Color.green;
    }

    void Update()
    {
        if (enableStationPlacement)
        {
            RaycastHit hit;

            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100, StationPlacementMask))
            {
                ShowLaser(hit);
                stationPreview.SetActive(true);
                stationPreview.transform.position = hit.point + stationReticleOffset;

                // allow rotation station 
                if (mRotateStationRight.GetState(mTargetSource))
                {
                    stationPreview.transform.Rotate(0, StationPlacementRotationSpeed, 0);
                }
                else if (mRotateStationLeft.GetState(mTargetSource))
                {
                    stationPreview.transform.Rotate(0, -StationPlacementRotationSpeed, 0);
                }
            }
            // if laser hits no valid surface, make is and the stationpreview invisible
            else
            {
                laser.SetActive(false);
                stationPreview.SetActive(false);
            }
            // set stations final placement once placement-button has been pressed
            if (mPlaceStationAction.GetStateDown(mTargetSource))
            {
                enableStationPlacement = false;
                laser.SetActive(false);
                station.Position = stationPreview.transform.position;
                station.Rotation = stationPreview.transform.rotation;
            }
        }
    }

    /// <summary>
    /// enables the placement of a station
    /// </summary>
    /// <param name="station">station to be placed</param>
    public void EnableStationPlacer(Station station)
    {
        enableStationPlacement = true;
        // create station
        StationFactory stationFactory = new StationFactory();
        stationPreview = stationFactory.CreateStation(station, mUIPointer);
        this.station = station;
        // don't show station for now
        stationPreview.SetActive(false);
    }

    /// <summary>
    /// shows laser at given hitpoint
    /// </summary>
    /// <param name="hit">lasers hitpoint</param>
    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
        laserTransform.position = Vector3.Lerp(controllerPose.transform.position, hit.point, .5f);
        laserTransform.LookAt(hit.point);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x,
                                                laserTransform.localScale.y,
                                                hit.distance);
    }

}