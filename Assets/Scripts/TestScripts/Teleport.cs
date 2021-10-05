using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// inspired from: https://wirewhiz.com/how-to-implement-walking-and-jumping-in-unity-steamvr/

public class Teleport : MonoBehaviour
{
    private Vector2 trackpad;
    private Vector3 moveDirection;
    private CapsuleCollider CapCollider;
    private Rigidbody RBody;

 
    public float MovementSpeed;
    public GameObject Head;

    private void Start()
    {
        CapCollider = GetComponent<CapsuleCollider>();
        RBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        updateCollider();

        //moveDirection = Head.transform.localRotation * Vector3.forward;
        //Vector3 velocity = new Vector3(0, 0, 0);
        //velocity = moveDirection;
        //RBody.AddForce(velocity.x * MovementSpeed - RBody.velocity.x, 0, velocity.z * MovementSpeed - RBody.velocity.z, ForceMode.VelocityChange);
    }

    public void TeleportTo(string targetName)
    {
        print("Head.transform.localRotation: " + Head.transform.localRotation);
        Head.transform.localRotation = new Quaternion(0, 1, 0, 0);
        //print(Head.transform.position + " = old position CameraRig");
        //Head.transform.position = new Vector3(4,6, 4);
        //print(Head.transform.position + " = new position CameraRig");
    }
    


    private void updateCollider()
    {
        CapCollider.height = Head.transform.localPosition.y;
        CapCollider.center = new Vector3(Head.transform.localPosition.x, Head.transform.localPosition.y / 2, Head.transform.localPosition.z);
    }
}
