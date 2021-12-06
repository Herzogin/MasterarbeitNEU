using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

//Quelle: https://setzeus.medium.com/tutorial-steamvr-2-0-laser-pointer-bbc816ebeec5


public class Laserpointer_Size_Actions : MonoBehaviour
{
    GameObject selectedGameObject;
    public SteamVR_LaserPointer laserPointer;
    public SteamVR_Input_Sources hand;
    public SteamVR_Action_Vector2 touchPadAction;




    void Awake()
    {
        Debug.Log("Size action loaded");
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
    }

    // Update is called once per frame
    void Update()
    {
        changeSize();
    }

    public void changeSize()
    {
        Vector2 touchPadValue = touchPadAction.GetAxis(hand);
        if (touchPadValue != Vector2.zero & selectedGameObject != null)
        {
            selectedGameObject.transform.localScale = new Vector3(touchPadValue.x + 1, touchPadValue.x + 1, touchPadValue.x + 1);
        }
    }

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
        string name = e.target.name;
        if (selectedGameObject != null)
        {
            selectedGameObject = null;
        }
    }
}
