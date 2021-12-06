using UnityEngine;
using Valve.VR;

public class ObjectManipulation : MonoBehaviour
{
    //---------CHANGE OBJECTS COLOR----------------------------
    public void PaintObject(GameObject gameObject, Color color)
    {
        gameObject.GetComponent<Renderer>().material.color = color;
    }
    //_________________________________________________________


    //---------CHANGE OBJECTS SIZE------------------------
    //Voice only:
    public void bigger(GameObject gameObject)
    {
        print("bigger");
        Vector3 currentScale = gameObject.transform.localScale;
        if (currentScale.x < 5)
        {
            gameObject.transform.localScale = currentScale * 1.5f;
        }
    }

    //Voice only:
    public void smaller(GameObject gameObject)
    {
        print("smaller");
        Vector3 currentScale = gameObject.transform.localScale;
        if (currentScale.x > 0.2)
        {
            gameObject.transform.localScale = currentScale / 1.5f;
        }
    }

    //Controller only:
    public void ChangeSize(SteamVR_Action_Vector2 touchPadAction, SteamVR_Input_Sources hand, GameObject selectedGameObject)
    {
        Vector2 touchPadValue = touchPadAction.GetAxis(hand);
        if (touchPadValue != Vector2.zero & selectedGameObject != null)
        {
            selectedGameObject.transform.localScale = new Vector3(touchPadValue.x + 1, touchPadValue.x + 1, touchPadValue.x + 1);
        }
    }
    //_________________________________________________________


    //---------CHANGE OBJECTS POSITION---------------------
    //Voice only:
    public void moveRight(GameObject gameObject)
    {
        gameObject.transform.position += Vector3.right;
    }

    //Voice only:
    public void moveLeft(GameObject gameObject)
    {
        gameObject.transform.position += Vector3.left;
    }

    //Voice only:
    public void moveForward(GameObject gameObject)
    {
        gameObject.transform.position += Vector3.back;
    }

    //Voice only:
    public void moveBack(GameObject gameObject)
    {
        gameObject.transform.position += Vector3.forward;
    }

    //Voice only:
    public void moveUp(GameObject gameObject)
    {
        if (gameObject.transform.position.y < 10)
            gameObject.transform.position += Vector3.up;
    }

    //Voice only:
    public void moveDown(GameObject gameObject)
    {
        if (gameObject.transform.position.y >= 1)
        {
            gameObject.transform.position += Vector3.down;
        }
    }

    //Controller only:
    public void AttachObject(GameObject gameObject, GameObject Controller)
    {
        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false; //https://answers.unity.com/questions/767287/chow-to-disable-gravity-from-script.html
        }
        gameObject.transform.parent = Controller.transform;
    }

    //Controller only:
    public void DetachObject(GameObject gameObject)
    {
        gameObject.transform.SetParent(null);
        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true; //https://answers.unity.com/questions/767287/chow-to-disable-gravity-from-script.html
        }
    }
    //_________________________________________________________


    //---------CHANGE OBJECTS ROTATION---------------------
    //Voice only:
    public void rotateRight(GameObject gameObject)
    {
        gameObject.transform.Rotate(Vector3.down * 30);
    }
    //_________________________________________________________


    //---------DELETE OBJECT----------------------------
    public void DeleteObject(GameObject gameObject)
    {
        gameObject.GetComponent<MeshRenderer>().enabled = !gameObject.GetComponent<MeshRenderer>().enabled;
        if (gameObject.transform.childCount > 0)
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    //_________________________________________________________
}
