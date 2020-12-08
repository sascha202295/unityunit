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
    private int numberOfItems = 3;

    private bool listChanged = false;
    private float elapsedTime = 0;

    public List<string> itemNames = null;
    public List<Sprite> itemImages = null;
    public List<GameObject> itemGameObjects = null;

    // Start is called before the first frame update
    void Start()
    {
        createScrollView();
    }

    // Update is called once per frame
    void Update()
    {
        if(listChanged)
        {
            addToScrollView();
        }
        elapsedTime += Time.deltaTime;
        Debug.Log(elapsedTime);

        if (elapsedTime > 2.0f && numberOfItems < 20)
        {
            addNewItemToList("Item" + numberOfItems, itemImages[1], new GameObject("Test"+ numberOfItems));
            elapsedTime = 0;
        }
    }

    void addNewItemToList(string name, Sprite sprite, GameObject modelGameObject) 
    {
        itemNames.Add(modelGameObject.name);
        itemImages.Add(sprite);
        itemGameObjects.Add(modelGameObject);
        numberOfItems++;
        listChanged = true;
    }

    void createScrollView()
    {
        //setContent Holder Height;
        content.sizeDelta = new Vector2(0, numberOfItems * 60);

        for (int i = 0; i < numberOfItems; i++)
        {
            // 60 width of item
            float spawnY = i * 60;
            //newSpawn Position
            Vector3 pos = new Vector3(SpawnPoint.position.x, -spawnY, SpawnPoint.position.z);
            //instantiate item
            GameObject SpawnedItem = Instantiate(item, pos, SpawnPoint.rotation);
            //setParent
            SpawnedItem.transform.SetParent(SpawnPoint, false);
            //get ItemDetails Component
            ItemDetails itemDetails = SpawnedItem.GetComponent<ItemDetails>();
            //set name
            itemDetails.text.text = itemNames[i];
            //set image
            itemDetails.image.sprite = itemImages[i];
        }
    }

    void addToScrollView()
    {
        content.sizeDelta = new Vector2(0, numberOfItems * 60);

        int i =  numberOfItems - 1;
        
        // 60 width of item
        float spawnY = i * 60;
        //newSpawn Position
        Vector3 pos = new Vector3(SpawnPoint.position.x, -spawnY, SpawnPoint.position.z);
        //instantiate item
        GameObject SpawnedItem = Instantiate(item, pos, SpawnPoint.rotation);
        //setParent
        SpawnedItem.transform.SetParent(SpawnPoint, false);
        //get ItemDetails Component
        ItemDetails itemDetails = SpawnedItem.GetComponent<ItemDetails>();
        //set name
        itemDetails.text.text = itemNames[i];
        //set image
        itemDetails.image.sprite = itemImages[i];
        listChanged = false;
        Debug.Log("Add");
    }
}
