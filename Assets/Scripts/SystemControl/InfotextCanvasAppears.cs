using System.Collections;
using UnityEngine;

//Script shows a note at the beginning of the scene and automatically hides it after 10 seconds. 
public class InfotextCanvasAppears : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(InfotextAppears());
        StartCoroutine(InfotextVanishes());
    }

    private IEnumerator InfotextAppears()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<Canvas>().enabled = true;
    }

    private IEnumerator InfotextVanishes()
    {
        yield return new WaitForSeconds(10f);
        GetComponent<Canvas>().enabled = false;
    }
}