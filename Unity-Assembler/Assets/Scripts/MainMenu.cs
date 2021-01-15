using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //material and scale for filechoser screen
    /* public Material materialTransparent;
      public Material materialOpaque;
      private Vector3 scale = new Vector3(1.5f, 1.5f, 1.5f);
    */
    public Text statusText;

    void Awake()
    {
        PlayerPrefs.SetString("fileName", null);
    }
    public void StartUnityAssembler()
    {
        statusText.text = "Loading Scene...";
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void LoadProject()
    {
        statusText.text = "Loading Project...";
        PlayerPrefs.SetString("fileName", "test");
        SceneManager.LoadScene("Game");


        /*
        //place filechoser screen
        GameObject choserScreen = GameObject.Instantiate((GameObject)Resources.Load("FileChoser"), this.transform);
        choserScreen.transform.localPosition = this.transform;
        fileChosescript tmp = choserScreen.GetComponent<fileChosescript>();
        tmp.mUIPointer = ui_Pointer;
        tmp.mItemSpawnScale = scale;
        tmp.mItemSpawnMaterial = materialOpaque;
        tmp.mItemBuildMaterial = materialTransparent;
        */
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
