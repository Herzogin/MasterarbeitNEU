using System.Collections.Generic;
using UnityEngine;

public class Utils 
{
    //Returns a list of all Child-GameObjects from a given Gameobject.
    public static List<GameObject> ChildrenToList(GameObject game_object)
    {
        int children = game_object.transform.childCount;
        List<GameObject> ItemsInGroup = new List<GameObject>();
        for (int i = 0; i < children; ++i)
        {
            ItemsInGroup.Add(game_object.transform.GetChild(i).gameObject);
        }
        return ItemsInGroup;
    }
}
