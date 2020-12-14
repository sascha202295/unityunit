using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListCreator : MonoBehaviour
{
    [SerializeField]
    private Transform SpawnPoint = null;

    [SerializeField]
    private GameObject item = null;

    [SerializeField]
    private RectTransform content = null;

    [SerializeField]
    private int numberOfItems = 0;

    private float elapsedTime = 0;
    private bool addItem = true;

    public List<GameObject> itemGameObjects = null;

    private GameObject g1;
    private GameObject g2;
    private GameObject g3;
    private GameObject g4;
    private GameObject g5;

    // Start is called before the first frame update
    void Start()
    {
        g1 = new GameObject("Test1");
        g2 = new GameObject("Test2");
        g3 = new GameObject("Test3");
        g4 = new GameObject("Test4");
        g5 = new GameObject("Test5");
    }

    // Update is called once per frame
    void Update()
    {

        elapsedTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            addNewItemToList(g1);
            addNewItemToList(g2);
            addNewItemToList(g3);
            addNewItemToList(g4);
            addNewItemToList(g5);
            elapsedTime = 3.5f;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            removeItemFromList(g1);
            removeItemFromList(g3);
            removeItemFromList(g5);
            elapsedTime = 6.5f;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            removeItemFromList(g2);
            removeItemFromList(g4);
            elapsedTime = 0;
        }

    }

    void addNewItemToList(GameObject modelGameObject)
    {
        itemGameObjects.Add(modelGameObject);
        addItem = true;
        updateScrollView();
    }

    void removeItemFromList(GameObject modelGameObject)
    {
        itemGameObjects.Remove(modelGameObject);
        updateScrollView();
    }

    void updateScrollView()
    {
        //content.sizeDelta = new Vector2(0, numberOfItems * 60);
        if (addItem)
        {
            //AddItemToView(itemGameObjects[itemGameObjects.Count - 1]);
            //addItem = false;
            reCreateScrollview();
        }
        else
        {
            reCreateScrollview();
        }
    }

    void reCreateScrollview()
    {
        for (int i = 0; i < SpawnPoint.transform.childCount; i++)
        {
            Destroy(SpawnPoint.transform.GetChild(i).gameObject);
        }

        for (int ii = 0; ii < itemGameObjects.Count; ii++)
        {
            float spawnY = ii * 40;
            //newSpawn Position
            Vector3 pos = new Vector3(SpawnPoint.position.x, -spawnY, SpawnPoint.position.z);
            Debug.Log(itemGameObjects[ii] + ": " + spawnY + ": " + pos);
            //instantiate item
            item.transform.position = pos;
            GameObject SpawnedItem = Instantiate(item);
            //setParent
            SpawnedItem.transform.SetParent(SpawnPoint.transform, false);
            //get ItemDetails Component
            ItemDetails itemDetails = SpawnedItem.GetComponent<ItemDetails>();
            //set name
            itemDetails.text.text = itemGameObjects[ii].name;
        }
    }

    void AddItemToView(GameObject tmpObject)
    {
        // 40 width of item
        if (itemGameObjects.Contains(tmpObject))
        {
            int i = itemGameObjects.FindIndex(a => a.Equals(tmpObject));

            float spawnY = i * 40;
            //newSpawn Position
            Vector3 pos = new Vector3(SpawnPoint.position.x, -spawnY, SpawnPoint.position.z);
            //instantiate item
            item.transform.position = pos;
            GameObject SpawnedItem = Instantiate(item);
            //setParent
            SpawnedItem.transform.SetParent(SpawnPoint, false);
            //get ItemDetails Component
            ItemDetails itemDetails = SpawnedItem.GetComponent<ItemDetails>();
            //set name
            itemDetails.text.text = tmpObject.name;
        }
    }
}
