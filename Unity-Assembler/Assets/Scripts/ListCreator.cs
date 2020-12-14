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

        if (elapsedTime > 3.0f && elapsedTime < 3.4f)
        {
            addNewItemToList(g1);
            addNewItemToList(g2);
            addNewItemToList(g3);
            addNewItemToList(g4);
            addNewItemToList(g5);
            elapsedTime = 3.5f;
        }

        if (elapsedTime > 6.0f && elapsedTime < 6.4f)
        {
            removeItemFromList(g1);
            removeItemFromList(g3);
            removeItemFromList(g5);
            elapsedTime = 6.5f;
        }

        if (elapsedTime > 9.0f)
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
            AddItemToView(itemGameObjects[itemGameObjects.Count - 1]);
            addItem = false;
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

        int ii = 0;
        itemGameObjects.ForEach(delegate (GameObject gameObject)
        {
            if (itemGameObjects.Contains(gameObject))
            {
                float spawnY = (ii < 0 ? 0 : ii) * 60;
                ii++;
                Debug.Log("recreate " + gameObject + " i: " + ii + "y: " + spawnY);
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
                itemDetails.text.text = gameObject.name;
            }
        });
    }

    void AddItemToView(GameObject tmpObject)
    {
        // 60 width of item
        if (itemGameObjects.Contains(tmpObject))
        {
            int i = itemGameObjects.FindIndex(a => a.Equals(tmpObject));
            
            float spawnY = i * 60;
            spawnY = (spawnY < 0 ? 0 : spawnY);
            Debug.Log(tmpObject + " i: " + i + "y: " + spawnY);
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
