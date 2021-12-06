using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TidyUpBarrier : MonoBehaviour
{
    //GameObject Ork;
    List<GameObject> CapsulesList;
    List<GameObject> CubesList;
    List<GameObject> CylinderList;
    List<GameObject> SpheresList;


    // Start is called before the first frame update
    void Start()
    {
        //Ork = GameObject.Find("Cube (1)");
        CapsulesList = Utils.ChildrenToList(GameObject.Find("Capsules"));
        CubesList = Utils.ChildrenToList(GameObject.Find("Cubes"));
        CylinderList = Utils.ChildrenToList(GameObject.Find("Cylinder"));
        SpheresList = Utils.ChildrenToList(GameObject.Find("Spheres"));
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach (GameObject child in CapsulesList)
        {
            if (child.transform.localPosition.y < -2 |
                child.transform.localPosition.y > 10)
            {
                child.transform.position = new Vector3(-2, 5, -3);
            }
        }

        foreach (GameObject child in CubesList)
        {
            if (child.transform.localPosition.y < -2 |
                child.transform.localPosition.y > 10)
            {
                child.transform.position = new Vector3(2, 5, 3);
            }
        }

        foreach (GameObject child in CylinderList)
        {
            if (child.transform.localPosition.y < -2 |
                child.transform.localPosition.y > 10)
            {
                child.transform.position = new Vector3(2, 5, -3);
            }
        }

        foreach (GameObject child in SpheresList)
        {
            if (child.transform.localPosition.y < -2 |
                child.transform.localPosition.y > 10)
            {
                child.transform.position = new Vector3(-2, 5, 3);
            }
        }
    }
}
