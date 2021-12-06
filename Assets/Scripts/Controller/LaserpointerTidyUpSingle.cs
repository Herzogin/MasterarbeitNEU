using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

public class LaserpointerTidyUpSingle : MonoBehaviour
{
    GameObject pointerInsideObject;
    GameObject HelperObject;
    GameObject HelperObjectGroup;
    GameObject attachedObject;
    GameObject parentOfAttachedObject;

    //GameObject Capsules;
    //GameObject Cubes;
    //GameObject Cylinder;
    //GameObject Spheres;
    public SteamVR_LaserPointer laserPointer;
    public SteamVR_Input_Sources hand;
    public SteamVR_Action_Boolean positionAction;
    //public SteamVR_Action_Boolean forceDrop = null;
    public GameObject Controller;

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
    }


    void Start()
    {
        HelperObject = GameObject.Find("HelperObject");
        HelperObjectGroup = GameObject.Find("HelperObjectGroup");
        //Capsules = GameObject.Find("Capsules");
        //Cubes = GameObject.Find("Cubes");
        //Cylinder = GameObject.Find("Cylinder");
        //Spheres = GameObject.Find("Spheres");
        attachedObject = HelperObject;
        parentOfAttachedObject = HelperObjectGroup;
        positionAction.AddOnStateDownListener(TriggerDown, hand);
        positionAction.AddOnStateUpListener(TriggerUp, hand);
    }

    //private void Update()
    //{
    //    if (forceDrop.GetLastStateDown(SteamVR_Input_Sources.Any))
    //    {
    //        print("Grip pressed ");
    //        foreach (GameObject child in Utils.ChildrenToList(Controller))
    //        {
    //            if (child.CompareTag("manipulable"))
    //            {
    //                if (child.name == "Capsule")
    //                {
    //                    child.transform.parent = Capsules.transform;
    //                }
    //                else if (child.name == "Cube")
    //                {
    //                    child.transform.parent = Cubes.transform;
    //                }
    //                else if (child.name == "Cylinde")
    //                {
    //                    child.transform.parent = Cylinder.transform;
    //                }
    //                else if (child.name == "Sphere")
    //                {
    //                    child.transform.parent = Spheres.transform;
    //                }
    //            }
    //        }
    //    }
    //}

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger down");
        attachSingleObject();
    }


    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger up");
        detachSingleObject();
    }


    public void PointerInside(object sender, PointerEventArgs e)
    {
        string name = e.target.name;
        Debug.Log(name + " was entered");
        if (e.target.tag == "manipulable")
        {
            pointerInsideObject = e.target.gameObject;
        }
        else
        {
            Debug.Log(name + " was clicked, but has no tag `manipulable`");
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        string name = e.target.name;
        Debug.Log(name + " was exited");
        if (e.target.tag == "manipulable")
        {
            pointerInsideObject = HelperObject;
        }
        else
        {
            Debug.Log(name + " was exited, but has no tag `manipulable`");
        }
    }


    void attachSingleObject()
    {
        attachedObject = pointerInsideObject;
        parentOfAttachedObject = attachedObject.transform.parent.gameObject;
        if (attachedObject.GetComponent<Rigidbody>() != null)
        {
            attachedObject.GetComponent<Rigidbody>().useGravity = false; //https://answers.unity.com/questions/767287/chow-to-disable-gravity-from-script.html
        }
        attachedObject.transform.parent = Controller.transform;
    }


    void detachSingleObject()
    {

        attachedObject.transform.SetParent(parentOfAttachedObject.transform);
        if (attachedObject.GetComponent<Rigidbody>() != null)
        {
            attachedObject.GetComponent<Rigidbody>().useGravity = true; //https://answers.unity.com/questions/767287/chow-to-disable-gravity-from-script.html
        }
        parentOfAttachedObject = HelperObjectGroup;
        attachedObject = HelperObject;
    }
}
