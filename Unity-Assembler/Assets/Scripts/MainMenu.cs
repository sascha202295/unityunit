using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    void Awake()
    {
        PlayerPrefs.SetString("fileName", null);
    }
    public void StartGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    
    }  
    
    public void LoadGame() {
        PlayerPrefs.SetString("fileName", "test");

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!!!!");

    }


}
