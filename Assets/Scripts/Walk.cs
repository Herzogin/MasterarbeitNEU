using UnityEngine;


// The script constantly moves the player in the direction in which he turns his head.
// inspired from: https://wirewhiz.com/how-to-implement-walking-and-jumping-in-unity-steamvr/
public class Walk : MonoBehaviour
{
    private Vector2 trackpad;
    private Vector3 moveDirection;
    private CapsuleCollider CapCollider;
    private Rigidbody RBody;

    public GameObject target;
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
        moveDirection = Head.transform.localRotation * Vector3.forward;
        Vector3 velocity = new Vector3(0, 0, 0);
        velocity = moveDirection;
        RBody.AddForce(velocity.x * MovementSpeed - RBody.velocity.x, 0, velocity.z * MovementSpeed - RBody.velocity.z, ForceMode.VelocityChange);
    }

    public float GetMovementSpeed()
    {
        return MovementSpeed;
    }

    public void SetMovementSpeed(float speed)
    {
        MovementSpeed = speed;
    }

    private void updateCollider()
    {
        CapCollider.height = Head.transform.localPosition.y;
        CapCollider.center = new Vector3(Head.transform.localPosition.x, Head.transform.localPosition.y / 2, Head.transform.localPosition.z);
    }
}