using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

//Script for moving an object with controllers laserpointer.
//Inspired by: https://setzeus.medium.com/tutorial-steamvr-2-0-laser-pointer-bbc816ebeec5
public class Laserpointer_Position_Action : MonoBehaviour
{
    GameObject selectedGameObject;
    GameObject attachedGameObject;
    GameObject HelperObject;
    public SteamVR_LaserPointer laserPointer;
    public SteamVR_Input_Sources hand;
    public SteamVR_Action_Boolean positionAction;
    public GameObject Controller;

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
    }

    void Start()
    {
        HelperObject = GameObject.Find("HelperObject");
        positionAction.AddOnStateDownListener(TriggerDown, hand);
        positionAction.AddOnStateUpListener(TriggerUp, hand);
    }

    //selecting object with tag "manipulable" with controllers laserpointer
    public void PointerInside(object sender, PointerEventArgs e)
    {        
        if (e.target.tag == "manipulable")
        {
            selectedGameObject = e.target.gameObject;
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.tag == "manipulable")
        {
            selectedGameObject = HelperObject;
        }
    }

    //When controllers trigger is pressed, object is attached to laserpointer and can be positioned
    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        print("Trigger down");
        attachedGameObject = selectedGameObject;
        print(attachedGameObject.name + " is new attachedGameObject");
        if (attachedGameObject.GetComponent<Rigidbody>() != null)
        {
            attachedGameObject.GetComponent<Rigidbody>().useGravity = false; 
        }

        attachedGameObject.transform.parent = Controller.transform;
    }

    //When trigger is released, object detaches from laserpointer.
    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        print("Trigger up");
        print(attachedGameObject.name + " is still attachedGameObject");
        attachedGameObject.transform.SetParent(null);
        if (attachedGameObject.GetComponent<Rigidbody>() != null)
        {
            attachedGameObject.GetComponent<Rigidbody>().useGravity = true; 
        }
        attachedGameObject = null;
    }
}
