using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;


public class LaserpointerTidyUpMultiple : MonoBehaviour
{
    GameObject selectedGameObject;
    GameObject attachedGameObject;
    List<GameObject> attachedObjectsList;
    GameObject HelperObject;
    bool multiMode = false;
    public SteamVR_LaserPointer laserPointer;
    public SteamVR_Input_Sources hand;
    public SteamVR_Action_Boolean positionAction;
    public SteamVR_Action_Boolean multiple_objects_selection_action;
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

    public void ButtonPressed(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        
        if (multiMode)
        {

        }
        else if (!multiMode)
        {

        }
        multiMode = !multiMode;
        print("multiMode: " + multiMode);
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (!multiMode)
        {
            Debug.Log("Trigger down in single mode");
            attachedGameObject = selectedGameObject;
            print(attachedGameObject.name + " is new attachedGameObject");
            if (attachedGameObject.GetComponent<Rigidbody>() != null)
            {
                attachedGameObject.GetComponent<Rigidbody>().useGravity = false;
            }

            attachedGameObject.transform.parent = Controller.transform;
        }
        else if (multiMode)
        {
            Debug.Log("Trigger down in multi mode");
            attachedGameObject = selectedGameObject;
            attachedGameObject.transform.localScale *= 1.5f;
            attachedObjectsList.Add(attachedGameObject);
        }
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (!multiMode)
        {
            Debug.Log("Trigger up in single mode");
            print(attachedGameObject.name + " ist noch attachedGameObject");
            attachedGameObject.transform.SetParent(null);
            if (attachedGameObject.GetComponent<Rigidbody>() != null)
            {
                attachedGameObject.GetComponent<Rigidbody>().useGravity = true;
            }
            attachedGameObject = null;
        }
        else if (multiMode)
        {
            print("Trigger up in multi mode");
            print("Liste " + attachedObjectsList + " hat folgende items:");
            foreach (GameObject item in attachedObjectsList)
            {
                print("");
                print(item.name);
            }
        }
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








    //GameObject pointerInsideObject;
    //GameObject HelperObject;
    //GameObject HelperObjectGroup;
    //GameObject attachedObject;
    //GameObject parentOfAttachedObject;
    //List<GameObject> selectedObjectsList;

    ////GameObject Capsules;
    ////GameObject Cubes;
    ////GameObject Cylinder;
    ////GameObject Spheres;
    //public SteamVR_LaserPointer laserPointer;
    //public SteamVR_Input_Sources hand;
    //public SteamVR_Action_Boolean multiple_objects_selection_action;
    ////public SteamVR_Action_Boolean forceDrop = null;
    //public GameObject Controller;

    //void Awake()
    //{
    //    laserPointer.PointerIn += PointerInside;
    //    laserPointer.PointerOut += PointerOutside;
    //}


    //void Start()
    //{
    //    HelperObject = GameObject.Find("HelperObject");
    //    HelperObjectGroup = GameObject.Find("HelperObjectGroup");
    //    //Capsules = GameObject.Find("Capsules");
    //    //Cubes = GameObject.Find("Cubes");
    //    //Cylinder = GameObject.Find("Cylinder");
    //    //Spheres = GameObject.Find("Spheres");
    //    pointerInsideObject = HelperObject;
    //    attachedObject = HelperObject;
    //    parentOfAttachedObject = HelperObjectGroup;
    //    multiple_objects_selection_action.AddOnStateDownListener(ButtonPressed, hand);
    //    multiple_objects_selection_action.AddOnStateUpListener(ButtonReleased, hand);
    //}

    ////private void Update()
    ////{
    ////    if (forceDrop.GetLastStateDown(SteamVR_Input_Sources.Any))
    ////    {
    ////        print("Grip pressed ");
    ////        foreach (GameObject child in Utils.ChildrenToList(Controller))
    ////        {
    ////            if (child.CompareTag("manipulable"))
    ////            {
    ////                if (child.name == "Capsule")
    ////                {
    ////                    child.transform.parent = Capsules.transform;
    ////                }
    ////                else if (child.name == "Cube")
    ////                {
    ////                    child.transform.parent = Cubes.transform;
    ////                }
    ////                else if (child.name == "Cylinder")
    ////                {
    ////                    child.transform.parent = Cylinder.transform;
    ////                }
    ////                else if (child.name == "Sphere")
    ////                {
    ////                    child.transform.parent = Spheres.transform;
    ////                }
    ////            }
    ////        }
    ////    }
    ////}

    //public void ButtonPressed(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    //{
    //    print("Button pressed");
    //    attachObjectGroup();
    //}


    //public void ButtonReleased(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    //{
    //    print("Button released");
    //    detachObjectGroup();
    //}


    //public void PointerInside(object sender, PointerEventArgs e)
    //{
    //    string name = e.target.name;
    //    Debug.Log(name + " was entered");
    //    if (e.target.tag == "manipulable")
    //    {
    //        pointerInsideObject = e.target.gameObject;
    //    }
    //    else
    //    {
    //        Debug.Log(name + " was entered, but has no tag `manipulable`");
    //    }
    //}

    //public void PointerOutside(object sender, PointerEventArgs e)
    //{
    //    string name = e.target.name;
    //    Debug.Log(name + " was exited");
    //    if (e.target.tag == "manipulable")
    //    {
    //        pointerInsideObject = HelperObject;
    //    }
    //    else
    //    {
    //        Debug.Log(name + " was exited, but has no tag `manipulable`");
    //    }
    //}


    //void attachObjectGroup()
    //{
    //    attachedObject = pointerInsideObject;
    //    parentOfAttachedObject = attachedObject.transform.parent.gameObject;

    //    selectedObjectsList = Utils.ChildrenToList(parentOfAttachedObject);
    //    foreach (GameObject child in selectedObjectsList)
    //    {
    //        if (child.GetComponent<Rigidbody>() != null)
    //        {
    //            child.GetComponent<Rigidbody>().useGravity = false;
    //            child.transform.parent = Controller.transform;
    //        }
    //    }
    //}


    //void detachObjectGroup()
    //{
    //    //get parent gameObject:
    //    foreach (GameObject child in selectedObjectsList)
    //    {
    //        child.transform.SetParent(parentOfAttachedObject.transform);
    //        child.GetComponent<Rigidbody>().useGravity = true; //https://answers.unity.com/questions/767287/chow-to-disable-gravity-from-script.html
    //    }
    //    parentOfAttachedObject = HelperObjectGroup;
    //    attachedObject = HelperObject;
    //}
}
