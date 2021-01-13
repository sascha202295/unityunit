using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StationScreenController : MonoBehaviour
{
    public GameObject mListItemSpawnpoint;
    public GameObject mListItem;
    public GameObject mUIPointer;
    public GameObject stationModel;
    public Station station;
    public Material mItemSpawnMaterial;
    public Material mItemBuildMaterial;
    public Vector3 mItemSpawnScale;

    private Vector3 mItemSpawnpoint = new Vector3(4.0f, 0.3f, -3.0f);
    private Vector3 mItemSpawnRotation = new Vector3(0, -90.0f, 0);

    private GameObject partHolder;

    private List<Part> partList;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Canvas>().worldCamera = mUIPointer.GetComponent<Camera>();
        partList = new List<Part>(station.PartList);
        UpdateScrollView();

        // create parent object for parts, to make deletion of spawned parts easier
        partHolder = new GameObject();
        partHolder.name = "partHolder";
        partHolder.transform.parent = transform.parent;
        partHolder.transform.position = transform.root.position;
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
            foreach (Part part in partList)
            {
                //instantiate item ans set parent
                GameObject spawnedItem = Instantiate(mListItem, mListItemSpawnpoint.transform, false);
                spawnedItem.GetComponentInChildren<Text>().text = part.Name;
                spawnedItem.GetComponent<Button>().onClick.AddListener(() => this.ItemClicked(spawnedItem));
            }
        }
    }

    public void ItemClicked(GameObject button)
    {
        string tmp = button.GetComponentInChildren<Text>().text;

        foreach (Part part in partList)
        {
            if (part.Name.Equals(tmp))
            {
                //spawn and position part
                GameObject tmpPart = Instantiate(stationModel.transform.GetChild(part.PartID).gameObject, partHolder.transform);
                tmpPart.transform.localScale = Vector3.Scale(tmpPart.transform.localScale, mItemSpawnScale);
                tmpPart.transform.localPosition = mItemSpawnpoint;
                // Apply rotation
                Quaternion tmpQuat = tmpPart.transform.localRotation;
                tmpQuat.eulerAngles = mItemSpawnRotation;
                tmpPart.transform.localRotation = tmpQuat;
                //add rigidbody and set meshcollider variables
                Rigidbody tmpRig = tmpPart.AddComponent<Rigidbody>();
                tmpRig.useGravity = true;
                tmpRig.isKinematic = true;
                foreach (MeshCollider col in tmpPart.GetComponents<MeshCollider>())
                {
                    col.isTrigger = false;
                }
                // set material
                Utils.SetObjectMaterial(tmpPart.transform, mItemSpawnMaterial);
                // destroy ProductAssemblyCollider, as it is not needed
                Destroy(tmpPart.GetComponent<ProductAssemblyCollider>());

                partList.Remove(part);

                UpdateScrollView();
                return;
            }
        }
    }

    public void PartPlaced(Part part)
    {
        station.PartPlaced(part);
    }

    public void ResetStation()
    {
        partList = new List<Part>(station.PartList);
        foreach (Part part in partList)
        {
            Utils.SetObjectMaterial(stationModel.transform.GetChild(part.PartID), mItemBuildMaterial);
            part.IsPlaced = false;
        }
        UpdateScrollView();
        foreach(Transform part in partHolder.transform)
        {
            Destroy(part.gameObject);
        }
    }
}
