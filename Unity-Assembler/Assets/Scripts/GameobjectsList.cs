using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.EventSystems;

public class GameobjectsList : MonoBehaviour
{
    private bool isListCreated=false;
    private GameObject klmodell ;
    private Button exit_button;
    private Transform  ContentView, gridList, bicycle;
    public Camera world_camera;
    private GameObject itemPrefab;
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
            klmodell = Instantiate((GameObject)Resources.Load("KLModell"), new Vector3(this.transform.position.x, this.transform.position.y,8), Quaternion.identity);
            gridList = klmodell.transform.Find("GridList");
            //edit canvas setting of girdlist ui
            gridList.GetComponent<Canvas>().worldCamera = world_camera;
            gridList.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            gridList.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            gridList.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            gridList.GetComponent<CanvasScaler>().referencePixelsPerUnit = 100;

            //refrence bicycle from Resources folder
            bicycle = klmodell.transform.Find("Modell").Find("Cyclev2");

            //When an Exit button is Clicked
            exit_button = gridList.transform.Find("Header").Find("X_button").GetComponent<Button>();
            exit_button.onClick.AddListener((UnityEngine.Events.UnityAction)DestroyGridList);


            ContentView = gridList.transform.Find("Scroll View").Find("Viewport").Find("Content").GetComponent<Transform>();
            // gridList.transform.localScale = new Vector3(Screen.width / 9.0f, Screen.width / 9.0f, Screen.width / 9.0f);

 

            foreach (Transform childs in this.transform)
            {
                if (childs.childCount > 0)
                    foreach (Transform child in childs)
                    {

                        Transform part = FindChild(bicycle, child.name);
                        GenerateItem(ContentView, FindChild(bicycle.transform, child.name));

                    }
                else
                {
                    Transform part = FindChild(bicycle, childs.name);
                    GenerateItem(ContentView, part);


                }
            }
            

        }


    }
    

    public void DestroyGridList()
    {
        isListCreated = false;
        Debug.Log("clicked....");
       // Destroy(gridList);
       // Destroy(bicycle);
        Destroy(klmodell);
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

    private Transform FindChild(Transform parent, string childName)
    {   
        foreach (Transform childs in parent)
        {
            //Debug.Log("name: "+childs.name);
            if (childs.childCount > 0)
                foreach (Transform child in childs)
                {
                    if (child.name== childName)
                        return child;
                }
            else
            {
                if (childs.name == childName)
                    return childs;
            }


        }

        return null;


    }


}
