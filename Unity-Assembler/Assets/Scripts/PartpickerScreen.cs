using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PartpickerScreen : MonoBehaviour
{
    public GameObject mUiPointer;

    public SteamVR_Input_Sources mTargetSource;
    public SteamVR_Action_Boolean mClickAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePartpicker()
    {
        VR_UIPointer tmp = mUiPointer.GetComponent<VR_UIPointer>();
        if (tmp.enablePartpicker)
        {
            tmp.enablePartpicker = false;
        }
        else
        {
            tmp.enablePartpicker = true;
        }
    }
}
