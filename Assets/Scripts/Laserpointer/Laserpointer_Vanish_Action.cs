using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

public class Laserpointer_Vanish_Action : MonoBehaviour
{
    GameObject selectedGameObject;
    public SteamVR_LaserPointer laserPointer;
    public SteamVR_Input_Sources hand;
    public SteamVR_Action_Boolean vanish;


    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
    }


    // Start is called before the first frame update
    void Start()
    {
        vanish.AddOnStateDownListener(TriggerPressed, hand);
    }



    public void TriggerPressed(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger down");
        selectedGameObject.GetComponent<MeshRenderer>().enabled = !selectedGameObject.GetComponent<MeshRenderer>().enabled;

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
