using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class PartpickerScreen : MonoBehaviour
{
    public GameObject mUiPointer;
    public LaserStationPlacer laserStationPlacer;

    public Transform mListItemSpawnpoint;
    public GameObject mListItem;
    public PreviousStationController previousStationController;
    public StationManager stationManager;
    public PreviousStationController PreviousStationController;

    public SteamVR_Input_Sources mTargetSource;
    public SteamVR_Action_Boolean mClickAction;

    private List<Part> pickedpartsList = null;
    private Material materialTransparent;

    void Start()
    {
        materialTransparent = (Material)Resources.Load("CycleTransparent");
        GetComponentInChildren<Canvas>().worldCamera = mUiPointer.GetComponent<Camera>();
    }

    public void AddNewItemToList(Part part)
    {
        if (pickedpartsList == null)
        {
            pickedpartsList = new List<Part>();
        }
        pickedpartsList.Add(part);
        UpdateScrollView();
    }

    public void RemoveItemFromList(Part part)
    {
        if (pickedpartsList != null)
        {
            pickedpartsList.Remove(part);
        }
        UpdateScrollView();
    }

    void UpdateScrollView()
    {
        // delete preexisting items
        foreach (Transform listItem in mListItemSpawnpoint.transform)
        {
            Destroy(listItem.gameObject);
        }
        // populate list if there are any
        if (pickedpartsList != null)
        {
            foreach (Part part in pickedpartsList)
            {
                //instantiate item ans set parent
                GameObject SpawnedItem = Instantiate(mListItem, mListItemSpawnpoint.transform, false);
                //get ItemDetails Component
                ItemDetails itemDetails = SpawnedItem.GetComponent<ItemDetails>();
                //set name
                itemDetails.text.text = part.Name;
            }
        }
    }

    public void TogglePartpicker()
    {
        VR_UIPointer tmp = mUiPointer.GetComponent<VR_UIPointer>();
        if (tmp.enablePartpicker)
        {
            tmp.enablePartpicker = false;
        }
        else
        {
            tmp.enablePartpicker = true;
        }
    }

    public void CreateStation()
    {
        //if (previousStationController.IsSelectionConfirmed())
        GameObject partPickerModel = transform.parent.GetComponent<StationManager>().PartPickerModel;

        Station station = new Station(pickedpartsList, previousStationController.GetChosenStations());
        laserStationPlacer.EnableStationPlacer(station);
        mUiPointer.SetActive(false);
        VR_UIPointer tmpUiPointer = mUiPointer.GetComponent<VR_UIPointer>();
        tmpUiPointer.DeselectParts();
        foreach (Part part in pickedpartsList)
        {
            Utils.SetObjectMaterial(partPickerModel.transform.GetChild(part.PartID), materialTransparent);
            Utils.SetObjectColor(partPickerModel.transform.GetChild(part.PartID), new Color(0f, 1f, 0f, 0.1f));
            RemoveAllMeshColliders(partPickerModel.transform.GetChild(part.PartID));
        }
        pickedpartsList = null;
        stationManager.AddStation(station);
        UpdateScrollView();
        // disable partpicker functionality
        /* 
        VR_UIPointer tmpUiPointer = mUiPointer.GetComponent<VR_UIPointer>();
        tmpUiPointer.enablePartpicker = false;
        Toggle tmpTogglePartpicker = (Toggle) transform.Find("Toggle_Partpicker").GetComponent("Toggle");
        tmpTogglePartpicker.isOn = false;
        */

    }

    public static void RemoveAllMeshColliders(Transform transform)
    {
        MeshCollider[] colliders = transform.GetComponents<MeshCollider>();
        foreach (MeshCollider collider in colliders)
        {
            Destroy(collider);
        }
    }
}
