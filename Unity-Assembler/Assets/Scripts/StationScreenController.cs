using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StationScreenController : MonoBehaviour
{
    public GameObject mListItemSpawnpoint;
    public GameObject mListItem;
    public GameObject mUIPointer;
    public Vector3 mItemSpawnpoint;
    public Quaternion mItemSpawnRotation;

    private List<GameObject> partList;

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

    public void SetPartList(List<GameObject> partList)
    {
        this.partList = partList;
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
        if (partList != null)
        {
            foreach (GameObject part in partList)
            {
                //instantiate item ans set parent
                GameObject spawnedItem = Instantiate(mListItem, mListItemSpawnpoint.transform, false);
                spawnedItem.GetComponentInChildren<Text>().text = part.name.Replace("(clone)", "");
                spawnedItem.GetComponent<Button>().onClick.AddListener(() => this.ItemClicked(spawnedItem));
            }
        }
    }

    public void ItemClicked(GameObject button)
    {
        string tmp = button.GetComponentInChildren<Text>().text;

        foreach (GameObject part in partList)
        {
            if(part.name == tmp)
            {
                Instantiate(part, mItemSpawnpoint, mItemSpawnRotation, transform.root);
                partList.Remove(part);
                UpdateScrollView();
                return;
            }
        }
    }
}
