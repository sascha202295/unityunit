using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameisPaused = false;
    public GameObject PManue;
    private GameObject Camera;
   
    
    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find("Camera2_");
      //  PManue = GameObject.Find("PManue");
       // Debug.Log("meue: " );
    }   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { 
            if (GameisPaused) {
                Resume();
            } else {
                Pause();
        
            }
        }

    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Camera.GetComponent<CameraController>().enabled = false;
        PManue.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;
    }

    public void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PManue.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
        if (!Camera.GetComponent<CameraController>().enabled)
            Camera.GetComponent<CameraController>().enabled = true;
    }

    public void MainMenu() {
        SceneManager.LoadScene("Menu");
    }
}
