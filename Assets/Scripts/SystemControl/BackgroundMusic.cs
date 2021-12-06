using UnityEngine;

//Script starts a music-loop, when scene is loaded.
public class BackgroundMusic : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<AudioManager>().LoopAudio("BackgroundSound");
    } 
}
