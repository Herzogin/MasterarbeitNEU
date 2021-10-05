using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class LaserpointerSystemControl : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;
    //GameObject sceneSwitch;
    SceneSwitch sceneSwitch;
    SkyboxController skyboxScript;

    void Awake()
    {
        laserPointer.PointerClick += PointerClick;
    }

    private void Start()
    {
        sceneSwitch = GameObject.FindObjectOfType(typeof(SceneSwitch)) as SceneSwitch;
        skyboxScript = GameObject.FindObjectOfType(typeof(SkyboxController)) as SkyboxController;
    } 

    public void PointerClick(object sender, PointerEventArgs e)
    {
        Debug.Log("clicked");

        if (e.target.name == "MusicOn")
        {
            Debug.Log(e.target.name + " was clicked");
            FindObjectOfType<AudioManager>().UnPauseAudio("BackgroundSound");
        }
        else if (e.target.name == "MusicOff")
        {
            Debug.Log(e.target.name + " was clicked");
            FindObjectOfType<AudioManager>().PauseAudio("BackgroundSound");
        }
        else if (e.target.name == "sunSwitch")
        {
            Debug.Log(e.target.name + " was clicked");
            skyboxScript.SkyToDay();
        }

        else if (e.target.name == "moonSwitch")
        {
            Debug.Log(e.target.name + " was clicked");
            skyboxScript.SkyToNight();
        }
        else if (e.target.name == "tidyUpVoiceScene")
        {
            Debug.Log(e.target.name + " was clicked");
            FindObjectOfType<AudioManager>().PlayAudio("SceneSwitchSound");
            sceneSwitch.GetComponent<SceneSwitch>().switchToScene("VoiceTidyUpScene");
        }
        else if(e.target.name == "tidyUpControllerScene")
        {
            Debug.Log(e.target.name + " was clicked");
            FindObjectOfType<AudioManager>().PlayAudio("SceneSwitchSound");
            sceneSwitch.GetComponent<SceneSwitch>().switchToScene("ControllerTidyUpScene");
        }
        else
        {
            Debug.Log(e.target.name + " was clicked, but we ignored it");
        }










        //if (e.target.name == "getHelp")
        //{
        //    Debug.Log(e.target.name + " was clicked");
        //    GameObject.Find("MainCanvas").GetComponent<Canvas>().enabled = true;
        //}
        //if (e.target.name == "getHelpOff")
        //{
        //    Debug.Log(e.target.name + " was clicked");
        //    GameObject.Find("MainCanvas").GetComponent<Canvas>().enabled = false;
        //}
        //if (e.target.name == "Stop")
        //{
        //    Debug.Log(e.target.name + " was clicked");
        //    rabbits = GameObject.FindGameObjectsWithTag("animal");
        //    foreach (GameObject rabbit in rabbits)
        //    {
        //        rabbit.GetComponent<Animator>().enabled = false;
        //    }
        //    GameObject.Find("Butterfly").GetComponent<Animation>().enabled = false;
        //    FindObjectOfType<AudioManager>().UnPauseAudio("BackgroundSound");
        //}
        //if (e.target.name == "Play")
        //{
        //    Debug.Log(e.target.name + " was clicked");
        //    rabbits = GameObject.FindGameObjectsWithTag("animal");
        //    foreach (GameObject rabbit in rabbits)
        //    {
        //        rabbit.GetComponent<Animator>().enabled = true;
        //    }
        //    GameObject.Find("Butterfly").GetComponent<Animation>().enabled = true;
        //    FindObjectOfType<AudioManager>().UnPauseAudio("BackgroundSound");
        //}


    }
}
