using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;



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
    }

    // Update is called once per frame
    void Update()
    {
        changeSize();
    }


    public void changeSize()
    {
        Debug.Log("inside change size");
        Vector2 touchPadValue = touchPadAction.GetAxis(hand);
        print("touchpad value: " + touchPadValue.x);
        if (touchPadValue != Vector2.zero)
        {
            Vector3 newScale = selectedGameObject.transform.localScale;
            newScale = new Vector3(touchPadValue.x + 1, touchPadValue.x + 1, touchPadValue.x + 1);
            selectedGameObject.transform.localScale = newScale;
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
}
