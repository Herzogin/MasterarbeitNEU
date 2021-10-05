using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

//Quelle: https://setzeus.medium.com/tutorial-steamvr-2-0-laser-pointer-bbc816ebeec5


public class Laserpointer_Position_Action : MonoBehaviour
{
    GameObject selectedGameObject;
    public SteamVR_LaserPointer laserPointer;
    public SteamVR_Input_Sources hand;
    public SteamVR_Action_Boolean positionAction;
    public GameObject Controller;

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
    }

    // Start is called before the first frame update
    void Start()
    {
        positionAction.AddOnStateDownListener(TriggerDown, hand);
        positionAction.AddOnStateUpListener(TriggerUp, hand);
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger down");
        if (selectedGameObject.GetComponent<Rigidbody>() != null)
        {
            selectedGameObject.GetComponent<Rigidbody>().useGravity = false; //https://answers.unity.com/questions/767287/chow-to-disable-gravity-from-script.html
        }
            
        selectedGameObject.transform.parent = Controller.transform;
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger up");
        selectedGameObject.transform.SetParent(null);
        if (selectedGameObject.GetComponent<Rigidbody>() != null)
        {
            selectedGameObject.GetComponent<Rigidbody>().useGravity = true; //https://answers.unity.com/questions/767287/chow-to-disable-gravity-from-script.html
        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        string name = e.target.name;
        Debug.Log(name + " was entered");
        if (e.target.tag == "manipulable")
        {
            selectedGameObject = e.target.gameObject;
        }
        else
        {
            Debug.Log(name + " was clicked, but has no tag `manipulable`");
        }
    }
}
