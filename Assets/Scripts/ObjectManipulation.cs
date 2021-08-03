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
        gameObject.GetComponent<Renderer>().material.color = color;
    }

    public void bigger(PointerEventArgs e)
    {
        Debug.Log("größer");
        Vector3 newScale = e.target.transform.localScale;
        newScale *= 1.5f;
        e.target.transform.localScale = newScale;
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
}
