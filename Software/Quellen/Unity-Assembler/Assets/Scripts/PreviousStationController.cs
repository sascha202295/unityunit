using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviousStationController : MonoBehaviour
{
    public GameObject mListItemSpawnpoint;
    public GameObject mListItem;

    private List<Station> stationList;
    private List<Station> chosenStations = new List<Station>();
    private bool selectionConfirmed = false;

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
                if(chosenStations.Contains(station))
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
            if (station.Name.Equals(tmp))
            {
                if (chosenStations.Contains(station))
                {
                    chosenStations.Remove(station);
                    UpdateScrollView();
                    return;
                }
                else
                {
                    chosenStations.Add(station);
                    UpdateScrollView();
                    return;
                }
            }
        }
    }

    public void DeleteChosenStations()
    {
        chosenStations = new List<Station>();
        UpdateScrollView();
    }

    public void ToggleSelection()
    {
        selectionConfirmed = !selectionConfirmed;
    }

    public bool IsSelectionConfirmed()
    {
        return selectionConfirmed;
    }

    public List<Station> GetChosenStations()
    {
        return chosenStations;
    }
}
