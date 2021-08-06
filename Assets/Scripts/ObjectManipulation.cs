using Valve.VR.Extras;
using System.Collections;
using UnityEngine;

public class ObjectManipulation : MonoBehaviour
{
    public void colorMe(PointerEventArgs e, Color color)
    {
        e.target.gameObject.GetComponent<Renderer>().material.color = color;
    }

    public void colorVoice(GameObject gameObject, Color color)
    {
        if (gameObject.name == "snowman")
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = color;
            gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = color;
            gameObject.transform.GetChild(2).gameObject.GetComponent<Renderer>().material.color = color;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = color;
        }


    }

    public void bigger(PointerEventArgs e)
    {
        Debug.Log("größer");
        Vector3 newScale = e.target.transform.localScale;
        newScale *= 1.5f;
        e.target.transform.localScale = newScale;
    }

    public void bigger(GameObject gameObject)
    {
        Debug.Log("bigger");
        Vector3 newScale = gameObject.transform.localScale;
        newScale *= 1.5f;
        gameObject.transform.localScale = newScale;
    }

    public void smaller(GameObject gameObject)
    {
        Debug.Log("smaller");
        Vector3 newScale = gameObject.transform.localScale;
        newScale /= 1.5f;
        gameObject.transform.localScale = newScale;
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



    //public void moveRight(GameObject gameObject, float speed)
    //{
    //    gameObject.transform.position += Time.deltaTime * speed * Vector3.right;
    //}

    public void rotateRight(GameObject gameObject, float speed)
    {
        //gameObject.transform.Rotate(Time.deltaTime * speed * Vector3.down);
        gameObject.transform.Rotate(Vector3.down * 30);
    }

    public void delete(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public IEnumerator vanish(PointerEventArgs e)
    {
        Vector3 newVector = new Vector3(0.01f, 0.01f, 0.01f);
        float xsize = e.target.localScale.x;
        while (xsize > 0.02f)
        {
            xsize = e.target.localScale.x;
            e.target.localScale = e.target.localScale - newVector;
            yield return new WaitForSeconds(0.0001f);
        }
        Destroy(e.target.gameObject);
    }

    public IEnumerator vanish(GameObject gameObject)
    {
        Vector3 newVector = new Vector3(0.01f, 0.01f, 0.01f);
        float xsize = gameObject.transform.localScale.x;
        while (xsize > 0.02f)
        {
            xsize = gameObject.transform.localScale.x;
            gameObject.transform.localScale = gameObject.transform.localScale - newVector;
            yield return new WaitForSeconds(0.0001f);
        }
        Destroy(gameObject.transform.gameObject);
    }
}
