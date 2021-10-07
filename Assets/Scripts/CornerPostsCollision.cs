using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerPostsCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "[CameraRig]")
        {
            print("collision with corner post!");
            if (transform.tag == "manipulable")
            {
                FindObjectOfType<AudioManager>().PlayAudio("SuccessSound");
                GetComponent<Renderer>().material.color = Color.black;
                transform.tag = "altered";
            }
            
        }
    }
}
