using Valve.VR.Extras;
using System.Collections;
using UnityEngine;
using Valve.VR;

public class ObjectManipulation : MonoBehaviour
{

    public void PaintObject(GameObject gameObject, Color color)
    {
        gameObject.GetComponent<Renderer>().material.color = color;
    }

    //public void colorVoice(GameObject gameObject, Color color)
    //{
    //    if (gameObject.name == "snowman")
    //    {
    //        gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = color;
    //        gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = color;
    //        gameObject.transform.GetChild(2).gameObject.GetComponent<Renderer>().material.color = color;
    //    }
    //    else
    //    {
    //        gameObject.GetComponent<Renderer>().material.color = color;
    //    }
    //}

    //public void bigger(PointerEventArgs e)
    //{
    //    Debug.Log("größer");
    //    Vector3 currentScale = e.target.transform.localScale;
    //    if (currentScale.x < 5)
    //    {
    //        e.target.transform.localScale = currentScale * 1.5f;
    //    }
    //}

    public void bigger(GameObject gameObject)
    {
        Debug.Log("bigger");
        Vector3 currentScale = gameObject.transform.localScale;
        if (currentScale.x < 5)
        {
            gameObject.transform.localScale = currentScale * 1.5f;
        }
    }

    public void smaller(GameObject gameObject)
    {
        Debug.Log("smaller");
        Vector3 currentScale = gameObject.transform.localScale;
        if (currentScale.x > 0.2)
        {
            gameObject.transform.localScale = currentScale / 1.5f;
        }
    }

    public void ChangeSize(SteamVR_Action_Vector2 touchPadAction, SteamVR_Input_Sources hand, GameObject selectedGameObject)
    {
        Vector2 touchPadValue = touchPadAction.GetAxis(hand);
        if (touchPadValue != Vector2.zero & selectedGameObject != null)
        {
            selectedGameObject.transform.localScale = new Vector3(touchPadValue.x + 1, touchPadValue.x + 1, touchPadValue.x + 1);
        }
    }

    public void moveRight(GameObject gameObject)
    {
        gameObject.transform.position += Vector3.right;
    }

    public void moveLeft(GameObject gameObject)
    {
        gameObject.transform.position += Vector3.left;
    }

    public void moveForward(GameObject gameObject)
    {
        gameObject.transform.position += Vector3.back;
    }

    public void moveBack(GameObject gameObject)
    {
        gameObject.transform.position += Vector3.forward;
    }

    public void moveUp(GameObject gameObject)
    {
        if (gameObject.transform.position.y < 10)
            gameObject.transform.position += Vector3.up;
    }

    public void moveDown(GameObject gameObject)
    {
        if (gameObject.transform.position.y >= 1)
        {
            gameObject.transform.position += Vector3.down;
        }
        //else
        //{
        //    gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
        //}

    }



  

    public void rotateRight(GameObject gameObject, float speed)
    {
        //gameObject.transform.Rotate(Time.deltaTime * speed * Vector3.down);
        gameObject.transform.Rotate(Vector3.down * 30);
    }

    public void DeleteObject(GameObject gameObject)
    {
        gameObject.GetComponent<MeshRenderer>().enabled = !gameObject.GetComponent<MeshRenderer>().enabled;
        //gameObject.SetActive(false);
    }

    public void AttachObject(GameObject gameObject, GameObject Controller)
    {
        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false; //https://answers.unity.com/questions/767287/chow-to-disable-gravity-from-script.html
        }
        gameObject.transform.parent = Controller.transform;
    }

    public void DetachObject(GameObject gameObject)
    {
        gameObject.transform.SetParent(null);
        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true; //https://answers.unity.com/questions/767287/chow-to-disable-gravity-from-script.html
        }
    }


    //public IEnumerator vanish(PointerEventArgs e)
    //{
    //    Vector3 newVector = new Vector3(0.01f, 0.01f, 0.01f);
    //    float xsize = e.target.localScale.x;
    //    while (xsize > 0.02f)
    //    {
    //        xsize = e.target.localScale.x;
    //        e.target.localScale = e.target.localScale - newVector;
    //        yield return new WaitForSeconds(0.0001f);
    //    }
    //    Destroy(e.target.gameObject);
    //}

    //public IEnumerator vanish(GameObject gameObject)
    //{
    //    Vector3 newVector = new Vector3(0.01f, 0.01f, 0.01f);
    //    float xsize = gameObject.transform.localScale.x;
    //    while (xsize > 0.02f)
    //    {
    //        xsize = gameObject.transform.localScale.x;
    //        gameObject.transform.localScale = gameObject.transform.localScale - newVector;
    //        yield return new WaitForSeconds(0.0001f);
    //    }
    //    Destroy(gameObject.transform.gameObject);
    //}
}
