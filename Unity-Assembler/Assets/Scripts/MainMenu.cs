using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
