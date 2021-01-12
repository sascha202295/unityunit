using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviousStationController : MonoBehaviour
{
    public GameObject mListItemSpawnpoint;
    public GameObject mListItem;
    public GameObject mUIPointer;
    public Vector3 mItemSpawnpoint;
    public Quaternion mItemSpawnRotation;

    private List<Station> stationList;
    private List<Station> choosenStations = new List<Station>();
    private bool selectionConfirmed = false;

    // Start is called before the first frame update
    void Start()
    {
        /*
        partList = new List<GameObject>();
        partList.Add(Instantiate((GameObject) Resources.Load("Pref_Station_Small"), new Vector3(0,-10f,0), mItemSpawnRotation));
        UpdateScrollView();
        */
        GetComponentInChildren<Canvas>().worldCamera = mUIPointer.GetComponent<Camera>();
    }

    public void SetStationList(List<Station> stationList)
    {
        this.stationList = stationList;
        UpdateScrollView();
    }

    private void UpdateScrollView()
    {
        // delete preexisting items
        foreach (Transform listItem in mListItemSpawnpoint.transform)
        {
            Destroy(listItem.gameObject);
        }
        // populate list if there are any
        if (stationList != null)
        {
            foreach (Station station in stationList)
            {
                //instantiate item ans set parent
                GameObject spawnedItem = Instantiate(mListItem, mListItemSpawnpoint.transform, false);
                spawnedItem.GetComponentInChildren<Text>().text = station.Name;
                spawnedItem.GetComponent<Button>().onClick.AddListener(() => this.ItemClicked(spawnedItem));
                if(choosenStations.Contains(station))
                {
                    spawnedItem.GetComponent<Button>().GetComponent<Image>().color = Color.green;
                }
            }
        }
    }

    public void ItemClicked(GameObject button)
    {
        string tmp = button.GetComponentInChildren<Text>().text;

        foreach (Station station in stationList)
        {
            if (station.Name == tmp)
            {
                if (choosenStations.Contains(station))
                {
                    choosenStations.Remove(station);
                    UpdateScrollView();
                    return;
                }
                else
                {
                    choosenStations.Add(station);
                    UpdateScrollView();
                    return;
                }
            }
        }
    }

    public void DeleteChoosenStations()
    {
        choosenStations = new List<Station>();
    }

    public void ToogleSelection()
    {
        selectionConfirmed = !selectionConfirmed;
    }

    public bool IsSelectionConfirmed()
    {
        return selectionConfirmed;
    }

    public List<Station> GetChosenStations()
    {
        return choosenStations;
    }
}
