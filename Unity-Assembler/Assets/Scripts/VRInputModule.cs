using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInputModule : BaseInputModule
{
    public Camera mCamera;
    public GameObject mUiPointer;
    public SteamVR_Input_Sources mTargetSource;
    public SteamVR_Action_Boolean mClickAction;
    public SteamVR_Action_Boolean mTogglePointerAction;

    private GameObject mCurrentObject = null;
    private PointerEventData mData;

    protected override void Awake()
    {
        base.Awake();
        mData = new PointerEventData(eventSystem);
    }

    public override void Process()
    {
        mData.Reset();
        mData.position = new Vector2(mCamera.pixelWidth / 2, mCamera.pixelHeight / 2);

        eventSystem.RaycastAll(mData, m_RaycastResultCache);
        mData.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        mCurrentObject = mData.pointerCurrentRaycast.gameObject;

        m_RaycastResultCache.Clear();

        HandlePointerExitAndEnter(mData, mCurrentObject);

        if (mClickAction.GetStateDown(mTargetSource))
        {
            ProcessPress(mData);
        }

        if (mClickAction.GetStateUp(mTargetSource))
        {
            ProcessRelease(mData);
        }

        if (mTogglePointerAction.GetStateDown(mTargetSource))
        {
            ProcessTogglePointer();
        }
    }

    public PointerEventData GetData()
    {
        return mData;
    }

    private void ProcessPress(PointerEventData data)
    {
        // set raycast
        data.pointerPressRaycast = data.pointerCurrentRaycast;

        // check for object hit, get down handler and call it
        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(mCurrentObject, data, ExecuteEvents.pointerDownHandler);

        // if there is no downhandler, try click handler
        if (newPointerPress == null)
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(mCurrentObject);

        //set data
        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = mCurrentObject;
    }

    private void ProcessRelease(PointerEventData data)
    {
        // Execute pointer up
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

        // check for click handler
        GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(mCurrentObject);

        // check if object same as in press
        if (data.pointerPress == pointerUpHandler)
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);

        // clear selectet gameobject
        eventSystem.SetSelectedGameObject(null);

        //clear data
        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;
    }

    private void ProcessTogglePointer()
    {
        if (mUiPointer.activeSelf)
        {
            mUiPointer.SetActive(false);
        }
        else
        {
            mUiPointer.SetActive(true);
        }
    }

}
