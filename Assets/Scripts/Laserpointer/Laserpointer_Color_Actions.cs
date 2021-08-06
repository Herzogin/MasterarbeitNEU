using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

public class Laserpointer_Color_Actions : MonoBehaviour
{
    public SteamVR_Action_Boolean red;
    public SteamVR_Action_Boolean blue;
    public SteamVR_Action_Boolean green;
    public SteamVR_Action_Boolean yellow;
    public SteamVR_Action_Boolean white;
    public SteamVR_Input_Sources hand;
    public SteamVR_LaserPointer laserPointer;

    GameObject selectedGameObject;

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
    }

    // Start is called before the first frame update
    void Start()
    {
        red.AddOnStateDownListener(DPadNorth, hand);
        blue.AddOnStateDownListener(DPadEast, hand);
        green.AddOnStateDownListener(DPadSouth, hand);
        yellow.AddOnStateDownListener(DPadWest, hand);
        white.AddOnStateDownListener(DPadCenter, hand);
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        string name = e.target.name;
        Debug.Log(name + " was entered");
        selectedGameObject = e.target.gameObject;

    }

    public void DPadNorth(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("DPad North");
        selectedGameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void DPadEast(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("DPad East");
        selectedGameObject.GetComponent<Renderer>().material.color = Color.blue;
    }

    public void DPadWest(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("DPad West");
        selectedGameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void DPadSouth(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("DPad South");
        selectedGameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    public void DPadCenter(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("DPad Center");
        selectedGameObject.GetComponent<Renderer>().material.color = Color.white;
    }


}
