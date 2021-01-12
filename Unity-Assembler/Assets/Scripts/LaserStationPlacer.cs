using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class LaserStationPlacer : MonoBehaviour
{
    public SteamVR_Input_Sources mTargetSource;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean mPlaceStationAction;
    public SteamVR_Action_Boolean mRotateStationRight;
    public SteamVR_Action_Boolean mRotateStationLeft;
    public VR_UIPointer mUIPointer;
    private bool enableStationPlacement = false;

    public GameObject laserPrefab;
    private GameObject laser;
    private Transform laserTransform;

    public Vector3 teleportReticleOffset;
    public LayerMask StationPlacementMask;

    private GameObject stationPreview;
    private Station station;

    // Start is called before the first frame update
    void Start()
    {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
        laser.GetComponent<Renderer>().material.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        if (enableStationPlacement)
        {
            RaycastHit hit;

            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100, StationPlacementMask))
            {
                ShowLaser(hit);
                stationPreview.SetActive(true);
                stationPreview.transform.position = hit.point + teleportReticleOffset;
                //rotate station 
                if (mRotateStationRight.GetStateDown(mTargetSource))
                {
                    stationPreview.transform.Rotate(0, 5f, 0);
                }
                else if (mRotateStationLeft.GetStateDown(mTargetSource)) 
                {
                    stationPreview.transform.Rotate(0, -5f, 0);
                }
            }
            else
            {
                laser.SetActive(false);
                stationPreview.SetActive(false);
            }
            if (mPlaceStationAction.GetStateDown(mTargetSource))
            {
                enableStationPlacement = false;
                laser.SetActive(false);
            }
        }
    }

    public void EnableStationPlacer(Station station)
    {
        enableStationPlacement = true;
        StationFactory stationFactory = new StationFactory();
        stationPreview = stationFactory.CreateStation(station);
        this.station = station;
        stationPreview.SetActive(false);
    }

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