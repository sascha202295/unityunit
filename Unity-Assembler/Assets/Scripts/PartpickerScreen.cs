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

    public List<GameObject> itemGameObjects = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addNewItemToList(GameObject modelGameObject)
    {
        itemGameObjects.Add(modelGameObject);
        updateScrollView();
    }

    public void removeItemFromList(GameObject modelGameObject)
    {
        itemGameObjects.Remove(modelGameObject);
        updateScrollView();
    }

    void updateScrollView()
    {
        for (int i = 0; i < mListItemSpawnpoint.transform.childCount; i++)
        {
            Destroy(mListItemSpawnpoint.transform.GetChild(i).gameObject);
        }

        for (int ii = 0; ii < itemGameObjects.Count; ii++)
        {
            float spawnY = ii * 40;
            //newSpawn Position
            Vector3 pos = new Vector3(mListItemSpawnpoint.position.x, -spawnY, mListItemSpawnpoint.position.z);
            Debug.Log(itemGameObjects[ii] + ": " + spawnY + ": " + pos);
            //instantiate item
            mListItem.transform.position = pos;
            GameObject SpawnedItem = Instantiate(mListItem);
            //setParent
            SpawnedItem.transform.SetParent(mListItemSpawnpoint.transform, false);
            //get ItemDetails Component
            ItemDetails itemDetails = SpawnedItem.GetComponent<ItemDetails>();
            //set name
            itemDetails.text.text = itemGameObjects[ii].name;
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
        laserStationPlacer.enableStationPlacement = true;
        VR_UIPointer tmpUiPointer = mUiPointer.GetComponent<VR_UIPointer>();
        tmpUiPointer.enablePartpicker = false;
        mUiPointer.SetActive(false);
        Toggle tmpTogglePartpicker = (Toggle) transform.Find("Toggle_Partpicker").GetComponent("Toggle");
        tmpTogglePartpicker.isOn = false;
    }
}
