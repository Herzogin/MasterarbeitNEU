using Valve.VR;
using UnityEngine;

// inspired from: https://wirewhiz.com/how-to-implement-walking-and-jumping-in-unity-steamvr/


public class ControllerWalking : MonoBehaviour
{
    private Vector2 trackpad;
    private Vector3 moveDirection;
    private int GroundCount;
    private CapsuleCollider CapCollider;
    private Rigidbody RBody;
    bool speedButtonPressed = false;

    public SteamVR_Input_Sources MovementHand;//Set Hand To Get Input From
    public SteamVR_Action_Vector2 TrackpadAction;
    public SteamVR_Action_Boolean touch = null;
    public SteamVR_Action_Boolean press = null;
    public float MovementSpeed;
   
    public GameObject Head;
    public GameObject AxisHand;//Hand Controller GameObject

    
    private void Start()
    {
        RBody = GetComponent<Rigidbody>();
        CapCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        updateInput();
        updateCollider();
        moveDirection = Quaternion.AngleAxis(Angle(trackpad) + AxisHand.transform.localRotation.eulerAngles.y, Vector3.up) * Vector3.forward;//get the angle of the touch and correct it for the rotation of the controller
        Rigidbody RBody = GetComponent<Rigidbody>();
        Vector3 velocity = new Vector3(0, 0, 0);
        velocity = moveDirection;
        RBody.AddForce(velocity.x * MovementSpeed - RBody.velocity.x, 0, velocity.z * MovementSpeed - RBody.velocity.z, ForceMode.VelocityChange);

        if (touch.lastState & !speedButtonPressed)
        {
            MovementSpeed = 3;
        }
        else if (touch.lastState & speedButtonPressed)
        {
            MovementSpeed = 9;
        }
        else if (!touch.lastState)
        {
            MovementSpeed = 0;
        }
      
        if (press.GetLastStateDown(SteamVR_Input_Sources.Any))
        {
            speedButtonPressed = true;
            print("speed button pressed ");
        }
        else if (press.GetLastStateUp(SteamVR_Input_Sources.Any))
        {
            speedButtonPressed = false;
            print("speed button released");
        }
    }

   
    public static float Angle(Vector2 p_vector2)
    {
        if (p_vector2.x < 0)
        {
            return 360 - (Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg * -1);
        }
        else
        {
            return Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg;
        }
    }
 
    private void updateCollider()
    {
        CapCollider.height = Head.transform.localPosition.y;
        CapCollider.center = new Vector3(Head.transform.localPosition.x, Head.transform.localPosition.y / 2, Head.transform.localPosition.z);
    }

    public void updateInput() => trackpad = TrackpadAction.GetAxis(MovementHand);

    //private void updateInput()
    //{
    //    trackpad = TrackpadAction.GetAxis(MovementHand);
    //}

}
