﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class VRPauseMenu : MonoBehaviour
{
    public SteamVR_Input_Sources mTargetSource;
    public SteamVR_Action_Boolean mPauseAction;
    public GameObject mUIPointer;
    public GameObject mVRCamera;
    public float mMenuDistance = 2.0f;

    private GameObject mPauseMenu;
    private bool mIsPaused = false;
    private bool mUiPointeractive;

    // Start is called before the first frame update
    void Start()
    {
        mPauseMenu = this.gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (mPauseAction.GetStateDown(mTargetSource))
        {
            if (mIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        // save if UIPointer was active before menu activation
        if (!mUIPointer.activeSelf)
        {
            mUIPointer.SetActive(true);
            mUiPointeractive = false;
        }
        else
        {
            mUiPointeractive = true;
        }
        // place menu infront of camera
        transform.position = mVRCamera.transform.position + (mVRCamera.transform.forward * mMenuDistance);
        transform.LookAt(mVRCamera.transform);
        mPauseMenu.SetActive(true);
        Time.timeScale = 0f;
        mIsPaused = true;
    }

    public void Resume()
    {
        // only deactivate UIPointer if it was not active before opening the menu
        if (!mUiPointeractive)
        {
            mUIPointer.SetActive(false);
        }
        mPauseMenu.SetActive(false);
        Time.timeScale = 1f;
        mIsPaused = false;
    }

    public void VRMainMenu()
    {
        SceneManager.LoadScene("MainMenuVr");
    }
}
