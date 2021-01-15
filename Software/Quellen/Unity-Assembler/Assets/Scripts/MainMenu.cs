using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mUIPointer;
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
        choserScreen.transform.localPosition = transform.position;
        FileChooseScript tmp = choserScreen.GetComponent<FileChooseScript>();
        tmp.mUIPointer = mUIPointer;
        */
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
