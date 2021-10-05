using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

//Quelle: https://setzeus.medium.com/tutorial-steamvr-2-0-laser-pointer-bbc816ebeec5


public class LaserpointerManipulation : MonoBehaviour
{
    GameObject selectedGameObject;
    GameObject defaultObject;

    [Header("Right Controller")]
    public SteamVR_LaserPointer rightLaserPointer;
    public SteamVR_Input_Sources rightHand;
    public GameObject rightController;

    [Header("Actions Right Controller")]
    public SteamVR_Action_Vector2 sizeAction;
    public SteamVR_Action_Boolean positionAction;
 
    [Header("Left Controller")]
    public SteamVR_LaserPointer leftLaserPointer;
    public SteamVR_Input_Sources leftHand;

    [Header("Actions Left Controller")]
    public SteamVR_Action_Boolean vanish;
    public SteamVR_Action_Boolean red;
    public SteamVR_Action_Boolean blue;
    public SteamVR_Action_Boolean green;
    public SteamVR_Action_Boolean yellow;
    public SteamVR_Action_Boolean white;

    [Header("Script")]
    public ObjectManipulation manipulation;

    void Awake()
    {
        rightLaserPointer.PointerIn += PointerInside;
        rightLaserPointer.PointerOut += PointerOutside;
        leftLaserPointer.PointerIn += PointerInside;
        leftLaserPointer.PointerOut += PointerOutside;
    }

    void Start()
    {
        defaultObject = GameObject.Find("HelperObject");
        selectedGameObject = defaultObject;

        positionAction.AddOnStateDownListener(TriggerDown, rightHand);
        positionAction.AddOnStateUpListener(TriggerUp, rightHand);

        red.AddOnStateDownListener(DPadNorth, leftHand);
        blue.AddOnStateDownListener(DPadEast, leftHand);
        green.AddOnStateDownListener(DPadSouth, leftHand);
        yellow.AddOnStateDownListener(DPadWest, leftHand);
        white.AddOnStateDownListener(DPadCenter, leftHand);

        vanish.AddOnStateDownListener(TriggerPressed, leftHand);
    }

    // Update is called once per frame
    void Update()
    {
        manipulation.ChangeSize(sizeAction, rightHand, selectedGameObject);
    }

    // TRIGGER RIGHT HAND
    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) => manipulation.AttachObject(selectedGameObject, rightController);

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) => manipulation.DetachObject(selectedGameObject);


    //TRIGGER LEFT
    public void TriggerPressed(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) => manipulation.DeleteObject(selectedGameObject);


    // TRACKPAD LEFT NORTH SOUTH WEST EAST 
    public void DPadNorth(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) => manipulation.PaintObject(selectedGameObject, Color.red);
    
    public void DPadEast(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) => manipulation.PaintObject(selectedGameObject, Color.blue);
    
    public void DPadWest(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) => manipulation.PaintObject(selectedGameObject, Color.yellow);

    public void DPadSouth(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) => manipulation.PaintObject(selectedGameObject, Color.green);
    
    public void DPadCenter(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) => manipulation.PaintObject(selectedGameObject, Color.white);




    public void PointerInside(object sender, PointerEventArgs e)
    {
        string name = e.target.name;
        Debug.Log(name + " was entered for size change");
        if (e.target.tag == "manipulable")
        {
            Debug.Log(name + " has tag `manipulable` for size change");
            selectedGameObject = e.target.gameObject;
        }
        else
        {
            Debug.Log(name + " was clicked, but has no tag `manipulable`");
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (selectedGameObject != null)
        {
            selectedGameObject = defaultObject;
        }
    }
}
