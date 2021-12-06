using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

//Quelle: https://setzeus.medium.com/tutorial-steamvr-2-0-laser-pointer-bbc816ebeec5


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

    // Start is called before the first frame update
    void Start()
    {
        HelperObject = GameObject.Find("HelperObject");
        positionAction.AddOnStateDownListener(TriggerDown, hand);
        positionAction.AddOnStateUpListener(TriggerUp, hand);
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger down");
        attachedGameObject = selectedGameObject;
        print(attachedGameObject.name + " is new attachedGameObject");
        if (attachedGameObject.GetComponent<Rigidbody>() != null)
        {
            attachedGameObject.GetComponent<Rigidbody>().useGravity = false; //https://answers.unity.com/questions/767287/chow-to-disable-gravity-from-script.html
        }

        attachedGameObject.transform.parent = Controller.transform;
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger up");
        print(attachedGameObject.name + " ist noch attachedGameObject");
        attachedGameObject.transform.SetParent(null);
        if (attachedGameObject.GetComponent<Rigidbody>() != null)
        {
            attachedGameObject.GetComponent<Rigidbody>().useGravity = true; //https://answers.unity.com/questions/767287/chow-to-disable-gravity-from-script.html
        }
        attachedGameObject = null;
    }

    

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
}
