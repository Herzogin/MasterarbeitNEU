using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TidyUp : MonoBehaviour
{
    private PlaneRange BluePlane = new PlaneRange(0, 10, 0, 10);
    private PlaneRange RedPlane = new PlaneRange(-10, 0, 0, 10);
    private PlaneRange YellowPlane = new PlaneRange(-10, 0, -10, 0);
    private PlaneRange GreenPlane = new PlaneRange(0, 10, -10, 0);


    public void PlaceInBlue(List<GameObject> list)//, GameObject selectedPlane)
    {
        for (int i=0; i < list.Count; i++)
        {
            if(i<3)
            {
                list[i].transform.position = new Vector3(1, list[i].transform.localPosition.y, i + 1);
            }
            else if (i < 6)
            {
                list[i].transform.position = new Vector3(2, list[i].transform.localPosition.y, i + 1 - 3 );
            }
            else if (i < 9)
            {
                list[i].transform.position = new Vector3(3, list[i].transform.localPosition.y, i + 1 - 6);
            }
            
        }         
    }

    public void PlaceInRed(List<GameObject> list)//, GameObject selectedPlane)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (i < 3)
            {
                list[i].transform.position = new Vector3(-1, list[i].transform.localPosition.y, i + 1);
            }
            else if (i < 6)
            {
                list[i].transform.position = new Vector3(-2, list[i].transform.localPosition.y, i + 1 - 3);
            }
            else if (i < 9)
            {
                list[i].transform.position = new Vector3(-3, list[i].transform.localPosition.y, i + 1 - 6);
            }
        }
    }

    public void PlaceInYellow(List<GameObject> list)//, GameObject selectedPlane)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (i < 3)
            {
                list[i].transform.position = new Vector3(-1, list[i].transform.localPosition.y, -i + 1);
            }
            else if (i < 6)
            {
                list[i].transform.position = new Vector3(-2, list[i].transform.localPosition.y, -i + 1 - 3);
            }
            else if (i < 9)
            {
                list[i].transform.position = new Vector3(-3, list[i].transform.localPosition.y, -i + 1 - 6);
            }

        }
    }

    public void PlaceInGreen(List<GameObject> list)//, GameObject selectedPlane)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (i < 3)
            {
                list[i].transform.position = new Vector3(1, list[i].transform.localPosition.y, -i + 1);
            }
            else if (i < 6)
            {
                list[i].transform.position = new Vector3(2, list[i].transform.localPosition.y, -i + 1 - 3);
            }
            else if (i < 9)
            {
                list[i].transform.position = new Vector3(3, list[i].transform.localPosition.y, -i + 1 - 6);
            }

        }
    }
}
