using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

public class LaserpointerTidyUp : MonoBehaviour
{
    GameObject selectedGameObject;
    GameObject parent;
    List<GameObject> selectedObjectsList;
    public SteamVR_LaserPointer laserPointer;
    public SteamVR_Input_Sources hand;
    public SteamVR_Action_Boolean positionAction;
    public SteamVR_Action_Boolean multiple_objects_selection_action;
    public GameObject Controller;

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
    }


    void Start()
    {
        //positionAction.AddOnStateDownListener(TriggerDown, hand);
        //positionAction.AddOnStateUpListener(TriggerUp, hand);
        multiple_objects_selection_action.AddOnStateDownListener(ButtonPressed, hand);
        multiple_objects_selection_action.AddOnStateUpListener(ButtonReleased, hand);
    }


    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger down");
        attachSingleObject(selectedGameObject);
    }


    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger up");
        detachSingleObject(selectedGameObject);
    }


    public void ButtonPressed(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        print("Button pressed");
        attachObjectGroup(selectedGameObject);
    }


    public void ButtonReleased(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        print("Button released");
        detachObjectGroup();
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


    void attachSingleObject(GameObject gameObject)
    {
        parent = gameObject.transform.parent.gameObject;
        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false; //https://answers.unity.com/questions/767287/chow-to-disable-gravity-from-script.html
        }
        gameObject.transform.parent = Controller.transform;
    }


    void detachSingleObject(GameObject gameObject)
    {
        
        gameObject.transform.SetParent(parent.transform);
        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true; //https://answers.unity.com/questions/767287/chow-to-disable-gravity-from-script.html
        }
        parent = null;
    }


    void attachObjectGroup(GameObject childObject)
    {
        //get parent gameObject:
        parent = childObject.transform.parent.gameObject;
        selectedObjectsList = Utils.ChildrenToList(parent);
        foreach (GameObject child in selectedObjectsList)
        {
            if (selectedGameObject.GetComponent<Rigidbody>() != null)
            {
                child.GetComponent<Rigidbody>().useGravity = false; 
                child.transform.parent = Controller.transform;
            }
        }
    }


    void detachObjectGroup()
    {
        //get parent gameObject:
        //GameObject parent = childObject.transform.parent.gameObject;
        //parent.transform.SetParent(null);
        foreach (GameObject child in selectedObjectsList)
        {
            child.transform.SetParent(parent.transform);
            child.GetComponent<Rigidbody>().useGravity = true; //https://answers.unity.com/questions/767287/chow-to-disable-gravity-from-script.html
        }
        parent = null;
    }
}
