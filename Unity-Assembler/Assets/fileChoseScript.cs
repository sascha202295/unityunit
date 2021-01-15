using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;

public class fileChoseScript : MonoBehaviour
{
    public GameObject mListItemSpawnpoint;
    public GameObject mListItem;
    public GameObject mUIPointer;

    public Material mItemSpawnMaterial;
    public Material mItemBuildMaterial;
    public Vector3 mItemSpawnScale;

    private Vector3 mItemSpawnpoint = new Vector3(4.0f, 0.3f, -3.0f);
    private Vector3 mItemSpawnRotation = new Vector3(0, -90.0f, 0);
    private List<string> names;
   

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Canvas>().worldCamera = mUIPointer.GetComponent<Camera>();

        List<string> files  = new List<string>();
        string path = Application.persistentDataPath + "/saves/";

        DirectoryInfo d = new DirectoryInfo(@"path");


        string[] filenames = Directory.GetFiles(path);

        // saving file names in path in string list
        foreach (var fi in d.GetFiles())
        {    
            // remove json tag from file name
            string name = fi.Name;
            string name1 = name.Remove(name.Length - 6, 5);
            files.Add(name1);

        }

        if (names != null)
        {
            foreach (string s in names)
            {
               
                //instantiate item and set parent
                GameObject spawnedItem = Instantiate(mListItem, mListItemSpawnpoint.transform, false);
                spawnedItem.GetComponentInChildren<Text>().text = s;
                spawnedItem.GetComponent<Button>().onClick.AddListener(() => this.ItemClicked(spawnedItem));
            }

        }




    

    
    }

    public void ItemClicked(GameObject button)
    {
        string tmp = button.GetComponentInChildren<Text>().text;

        PlayerPrefs.SetString("fileName", "s");

    }


    
}
