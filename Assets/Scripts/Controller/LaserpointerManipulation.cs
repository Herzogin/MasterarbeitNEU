using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

//Script to change color, size, rotation, position of an object and delete it.
//Inspired by: https://setzeus.medium.com/tutorial-steamvr-2-0-laser-pointer-bbc816ebeec5
public class LaserpointerManipulation : MonoBehaviour
{
    GameObject selectedGameObject;
    GameObject defaultObject;
    bool readyToChangeSize = false;
    ManipulationSucceeded manipulationSucceded;


    [Header("Right Controller")]
    public SteamVR_LaserPointer rightLaserPointer;
    public SteamVR_Input_Sources rightHand;
    public GameObject rightController;

    [Header("Actions Right Controller")]
    public SteamVR_Action_Boolean start_change_size = null;
    public SteamVR_Action_Boolean stop_change_size;
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
        manipulationSucceded = FindObjectOfType(typeof(ManipulationSucceeded)) as ManipulationSucceeded;
        selectedGameObject = defaultObject;

        positionAction.AddOnStateDownListener(TriggerDown, rightHand);
        positionAction.AddOnStateUpListener(TriggerUp, rightHand);

        red.AddOnStateDownListener(DPadNorth, leftHand);
        blue.AddOnStateDownListener(DPadEast, leftHand);
        green.AddOnStateDownListener(DPadSouth, leftHand);
        yellow.AddOnStateDownListener(DPadWest, leftHand);
        white.AddOnStateDownListener(DPadCenter, leftHand);

        vanish.AddOnStateDownListener(TriggerPressed, leftHand);

        manipulationSucceded.IncreaseRotatedCount();
        manipulationSucceded.IncreaseRotatedCount();
    }

    void Update()
    {
        // TRACKPAD RIGHT.....change size:
        if (start_change_size.GetLastStateDown(SteamVR_Input_Sources.Any))
        {
            readyToChangeSize = true;
            manipulationSucceded.IncreaseChangedSizeCount();
        }

        if (stop_change_size.GetLastStateUp(SteamVR_Input_Sources.Any))
        {
            readyToChangeSize = false;
        }

        if (readyToChangeSize)
        {
            manipulation.ChangeSize(sizeAction, rightHand, selectedGameObject);
            
        }
    }

    // TRIGGER RIGHT HAND..... move object:
    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) => manipulation.AttachObject(selectedGameObject, rightController);

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        manipulation.DetachObject(selectedGameObject);
        manipulationSucceded.IncreaseChangedPositionCount();
    }


    //TRIGGER LEFT.....delete object:
    public void TriggerPressed(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        manipulation.DeleteObject(selectedGameObject);
        manipulationSucceded.IncreaseDeletedCount();
    }


    // TRACKPAD LEFT (NORTH, SOUTH, WEST, EAST).....change objects color:
    public void DPadNorth(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        manipulation.PaintObject(selectedGameObject, Color.red);
        manipulationSucceded.IncreaseChangedColorCount();
    }

    public void DPadEast(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    { 
        manipulation.PaintObject(selectedGameObject, Color.blue);
        manipulationSucceded.IncreaseChangedColorCount();
    }
    
    public void DPadWest(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        manipulation.PaintObject(selectedGameObject, Color.yellow);
        manipulationSucceded.IncreaseChangedColorCount();
    }

    public void DPadSouth(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        manipulation.PaintObject(selectedGameObject, Color.green);
        manipulationSucceded.IncreaseChangedColorCount();
    }
    
    public void DPadCenter(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    { 
        manipulation.PaintObject(selectedGameObject, Color.white);
        manipulationSucceded.IncreaseChangedColorCount();
    }



    //select an object by laserpointer:
    public void PointerInside(object sender, PointerEventArgs e)
    {
        string name = e.target.name;
        print(name + " was entered for size change");
        if (e.target.tag == "manipulable")
        {
            print(name + " has tag `manipulable` for size change");
            selectedGameObject = e.target.gameObject;
        }
        else
        {
            print(name + " was clicked, but has no tag `manipulable`");
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
