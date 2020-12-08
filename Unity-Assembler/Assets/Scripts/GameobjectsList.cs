using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.EventSystems;

public class GameobjectsList : MonoBehaviour
{
    private bool isListCreated=false;
    private GameObject gridList;
    private Button exit_button;
    private Transform item, ContentView;
    public Camera world_camera;
    private GameObject itemPrefab;
    private GameObject bicycle; 
    // Start is called before the first frame update
    void Start()
    {   
        if(world_camera==null) { 
        world_camera = GameObject.Find("Camera3_").GetComponent<Camera>();
        }
      
        //gridList = AssetDatabase.LoadAssetAtPath("Prefabs/GridList.prefab", typeof(GameObject)) as GameObject;

        //Instantiate(gridList, transform.position, Quaternion.identity);
        // myButton.onClick.AddListener ((UnityEngine.Events.UnityAction) this.DestroyGridList);
    }
    public void OnClick()
    {
        Debug.Log("Clicked!");
    }

    // Update is called once per frame
    void Update()
    {
   
    }
    private void OnMouseDown()
    {

        Debug.Log("mausdown....");
        if (!isListCreated) {
            isListCreated = true;
            //refrence ListItem from Resources folder
            gridList = Instantiate((GameObject)Resources.Load("GridList"), new Vector3(this.transform.position.x, this.transform.position.y,8), Quaternion.identity);
            //edit canvas setting of girdlist ui
            gridList.GetComponent<Canvas>().worldCamera = world_camera;
            gridList.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            gridList.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            gridList.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            gridList.GetComponent<CanvasScaler>().referencePixelsPerUnit = 100;

            //refrence bicycle from Resources folder
            bicycle = Instantiate((GameObject)Resources.Load("Cyclev2"), new Vector3(this.transform.position.x-1f, this.transform.position.y, 1.3f), Quaternion.identity);

            //When an Exit button is Clicked
            exit_button = gridList.transform.Find("Header").Find("X_button").GetComponent<Button>();
            exit_button.onClick.AddListener((UnityEngine.Events.UnityAction)DestroyGridList);


            ContentView = gridList.transform.Find("Scroll View").Find("Viewport").Find("Content").GetComponent<Transform>();
            // gridList.transform.localScale = new Vector3(Screen.width / 9.0f, Screen.width / 9.0f, Screen.width / 9.0f);

 

            foreach (Transform childs in this.transform)
            {
                if (childs.childCount > 0)
                    foreach (Transform child in childs)
                        GenerateItem(ContentView, bicycle.GetComponent<Transform>().Find(childs.name).Find(child.name));
                       // GenerateItem(ContentView, child.GetComponent<MeshFilter>().mesh);
                else
                    GenerateItem(ContentView, bicycle.GetComponent<Transform>().Find(childs.name));
                   // GenerateItem(ContentView, childs.GetComponent<MeshFilter>().mesh);
            }
            

        }


    }
    

    public void DestroyGridList()
    {
        isListCreated = false;
        Debug.Log("clicked....");
        Destroy(gridList);
        Destroy(bicycle);
    }

    void GenerateItem(Transform contentView, Transform bicyclePart) {
       GameObject itemPrefab = Instantiate((GameObject)Resources.Load("Item"));
       ItemHandler itemHandler = itemPrefab.AddComponent<ItemHandler>();
       itemHandler.bicycle = bicycle;
       itemHandler.bicyclePart = bicyclePart;

       //itemPrefab.transform.Find("meshItem").GetComponent<MeshFilter>().mesh = itemMesh;
        
        //meshPrefab.GetComponent<MeshFilter>().mesh = itemMesh;

        itemPrefab.transform.SetParent(contentView, false);

        //itemPrefab.GetComponent<Button>().onClick.AddListener(() => ItemClickHandler()) ;
      





    }

    private Transform RecursiveFindChild(Transform parent, string childName)
    {
        foreach (Transform childs in parent)
        {

           if (childs.childCount > 0)
                return childs.GetComponent<Transform>().Find(childName);
            else
                foreach (Transform child in childs)
                    return child.GetComponent<Transform>().Find(childName);

        }

        return null;


    }


}
