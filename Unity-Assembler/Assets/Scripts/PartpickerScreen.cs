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

    public SteamVR_Input_Sources mTargetSource;
    public SteamVR_Action_Boolean mClickAction;

    private List<GameObject> pickedpartsList = null;
    private Material disabledParts;

    void Start()
    {
        disabledParts = (Material)Resources.Load("CycleTransparent");
    }

    public void AddNewItemToList(GameObject modelGameObject)
    {
        if (pickedpartsList == null)
        {
            pickedpartsList = new List<GameObject>();
        }
        pickedpartsList.Add(modelGameObject);
        UpdateScrollView();
    }

    public void RemoveItemFromList(GameObject modelGameObject)
    {
        if (pickedpartsList != null)
        {
            pickedpartsList.Remove(modelGameObject);
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
            foreach (GameObject part in pickedpartsList)
            {
                //instantiate item ans set parent
                GameObject SpawnedItem = Instantiate(mListItem, mListItemSpawnpoint.transform, false);
                //get ItemDetails Component
                ItemDetails itemDetails = SpawnedItem.GetComponent<ItemDetails>();
                //set name
                itemDetails.text.text = part.name;
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
        laserStationPlacer.EnableStationPlacer(pickedpartsList);
        mUiPointer.SetActive(false);
        VR_UIPointer tmpUiPointer = mUiPointer.GetComponent<VR_UIPointer>();
        tmpUiPointer.DeselectParts();
        foreach (GameObject part in pickedpartsList)
        {
            VR_UIPointer.SetObjectMaterial(part.transform, disabledParts);
            VR_UIPointer.SetObjectColor(part.transform, new Color(0f, 1f, 0f, 0.1f));
            RemoveAllMeshColliders(part);
        }
        pickedpartsList = null;
        UpdateScrollView();
        // disable partpicker functionality
        /* 
        VR_UIPointer tmpUiPointer = mUiPointer.GetComponent<VR_UIPointer>();
        tmpUiPointer.enablePartpicker = false;
        Toggle tmpTogglePartpicker = (Toggle) transform.Find("Toggle_Partpicker").GetComponent("Toggle");
        tmpTogglePartpicker.isOn = false;
        */
    }

    public static void RemoveAllMeshColliders(GameObject mGameObject)
    {
        MeshCollider[] colliders = mGameObject.transform.GetComponents<MeshCollider>();
        foreach (MeshCollider collider in colliders)
        {
            Destroy(collider);
        }
    }
}
