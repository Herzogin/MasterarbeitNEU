using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerWalkingInput : MonoBehaviour
{
    //Quellen: 
    //https://www.youtube.com/watch?v=CPyaWkjo6Ss
    //https://www.youtube.com/watch?v=2SpegTl1wP8
    //https://www.youtube.com/watch?v=C7Lo3VoMHn0

    [Header("Actions")]
    public SteamVR_Action_Boolean touch = null;
    //public SteamVR_Action_Boolean press = null;
    public SteamVR_Action_Vector2 touchPosition = null;

    [Header("Scene Objects")]
    public ControllerWalking controllerWalking = null;


    private void Awake()
    {
        touch.onChange += Touch;
        //press.onStateUp += PressRelease;
        touchPosition.onAxis += Position;
    }

    private void OnDestroy()
    {
        touch.onChange -= Touch;
        //press.onStateUp -= PressRelease;
        touchPosition.onAxis -= Position;
    }

    private void Position(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
    {
        print("axis: " + axis);
        //controllerWalking.SetTouchPosition(axis);
    }

    private void Touch(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
    {
        print("new State: " + newState);
        //controllerWalking.Show(newState);
    }

    //private void PressRelease(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    //{
    //    print("released!");
    //    radialMenu.ActivateHighlightedSection();
    //}
}
