using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TidyUpSucceded : MonoBehaviour
{
    GameObject CubesGameObject;
    List<GameObject> CubeList;
    GameObject CylinderGameObject;
    List<GameObject> CylinderList;
    GameObject CapsulesGameObject;
    List<GameObject> CapsulesList;
    GameObject SpheresGameObject;
    List<GameObject> SpheresList;
    TidyUp tidyUp;
    bool taskCompleted = false;
    bool notWonalready = true;
    bool objectsAreTouching = false;

    GameObject bluePlane;


    public void Start()
    {
        CubesGameObject = GameObject.Find("Cubes");
        CubeList = ChildrenToList(CubesGameObject);
        CylinderGameObject = GameObject.Find("Cylinder");
        CylinderList = ChildrenToList(CylinderGameObject);
        CapsulesGameObject = GameObject.Find("Capsules");
        CapsulesList = ChildrenToList(CapsulesGameObject);
        SpheresGameObject = GameObject.Find("Spheres");
        SpheresList = ChildrenToList(SpheresGameObject);
        tidyUp = FindObjectOfType(typeof(TidyUp)) as TidyUp;
    }

    
    void Update()
    {
        if( tidyUp.IsInBlue(CubeList) &
            tidyUp.IsInRed(SpheresList) &
            tidyUp.IsInYellow(CapsulesList) &
            tidyUp.IsInGreen(CylinderList)
            )
        {
            taskCompleted = true;
        }

        //Quelle: https://forum.unity.com/threads/wait-for-second-without-startcouroutine.410320/
        if (taskCompleted & notWonalready)
        {
            StartCoroutine(TaskCompleted());

            notWonalready = false;
        }
    }

    

    

    private IEnumerator TaskCompleted()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<AudioManager>().PlayAudio("WinningSound");
    }

    List<GameObject> ChildrenToList(GameObject selectedGroup)
    {
        int children = selectedGroup.transform.childCount;
        List<GameObject> ItemsInGroup = new List<GameObject>();
        for (int i = 0; i < children; ++i)
        {
            ItemsInGroup.Add(selectedGroup.transform.GetChild(i).gameObject);
        }
        return ItemsInGroup;
    }
}
