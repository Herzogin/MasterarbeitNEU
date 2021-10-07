using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    public GameObject Walls;
    private void Awake()
    {
        //Walls = GameObject.Find("Walls");
        Walls.SetActive(true);
    }
}
