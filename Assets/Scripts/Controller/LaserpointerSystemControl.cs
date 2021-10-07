using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class LaserpointerSystemControl : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;
    SceneSwitch sceneSwitch;
    SkyboxController skyboxScript;
    GameObject[] buttons;

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
        else if (e.target.name == "tidyUpVoiceButton")
        {
            Debug.Log(e.target.name + " was clicked");
            FindObjectOfType<AudioManager>().PlayAudio("SceneSwitchSound");
            sceneSwitch.GetComponent<SceneSwitch>().switchToScene("VoiceTidyUpScene");
        }
        else if(e.target.name == "tidyUpControllerButton")
        {
            Debug.Log(e.target.name + " was clicked");
            FindObjectOfType<AudioManager>().PlayAudio("SceneSwitchSound");
            sceneSwitch.GetComponent<SceneSwitch>().switchToScene("ControllerTidyUpScene");
        }
        else if (e.target.name == "walkingVoiceButton")
        {
            Debug.Log(e.target.name + " was clicked");
            FindObjectOfType<AudioManager>().PlayAudio("SceneSwitchSound");
            sceneSwitch.GetComponent<SceneSwitch>().switchToScene("VoiceWalkingScene");
        }
        else if (e.target.name == "walkingControllerButton")
        {
            Debug.Log(e.target.name + " was clicked");
            FindObjectOfType<AudioManager>().PlayAudio("SceneSwitchSound");
            sceneSwitch.GetComponent<SceneSwitch>().switchToScene("ControllerWalkingScene");
        }
        else if (e.target.name == "ManipulationVoiceButton")
        {
            Debug.Log(e.target.name + " was clicked");
            FindObjectOfType<AudioManager>().PlayAudio("SceneSwitchSound");
            sceneSwitch.GetComponent<SceneSwitch>().switchToScene("VoiceSelectManipulationScene");
        }
        else if (e.target.name == "ManipulationControllerButton")
        {
            Debug.Log(e.target.name + " was clicked");
            FindObjectOfType<AudioManager>().PlayAudio("SceneSwitchSound");
            sceneSwitch.GetComponent<SceneSwitch>().switchToScene("ControllerSelectManipulationScene");
        }
        else if (e.target.name == "PlayButton")
        {
            Debug.Log(e.target.name + " was clicked");
            FindObjectOfType<AudioManager>().UnPauseAudio("BackgroundSound");
            skyboxScript.SkyToDay();
            //make floor big again:
            GameObject.Find("Plane").transform.localScale = new Vector3(6, 1, 6);
            GameObject.Find("PenguinSmall").GetComponent<Animation>().enabled = true;
            GameObject.Find("PenguinBig").GetComponent<Animation>().enabled = true;
            GameObject.Find("PenguinSmall").transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
            GameObject.Find("PenguinBig").transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
            //make rabbits visible and start animation again:
            GameObject Rabbits = GameObject.Find("Rabbits");
            foreach (GameObject rabbit in ChildrenToList(Rabbits))
            {
                rabbit.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
                rabbit.transform.GetChild(2).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
                rabbit.GetComponent<Animator>().enabled = true;
            }
            //make 3D buttons visible again:
            buttons = GameObject.FindGameObjectsWithTag("3Dbuttons");
            foreach (GameObject button in buttons)
            {
                button.GetComponent<MeshRenderer>().enabled = true;
            }
        }
        else if (e.target.name == "PauseButton")
        {
            Debug.Log(e.target.name + " was clicked");
            FindObjectOfType<AudioManager>().PauseAudio("BackgroundSound");
            //pause penguins animation:
            GameObject.Find("PenguinSmall").GetComponent<Animation>().enabled = false;
            GameObject.Find("PenguinBig").GetComponent<Animation>().enabled = false;
            //pause rabbits animation:
            GameObject Rabbits = GameObject.Find("Rabbits");
            foreach (GameObject rabbit in ChildrenToList(Rabbits))
            {
                rabbit.GetComponent<Animator>().enabled = false;
            }
        }
        else if (e.target.name == "StopButton")
        {
            Debug.Log(e.target.name + " was clicked");
            FindObjectOfType<AudioManager>().PauseAudio("BackgroundSound");
            skyboxScript.SkyToNight();
            //make floor small:
            GameObject.Find("Plane").transform.localScale = new Vector3(2, 1, 2);
            //make penguins invisible and pause animation:
            GameObject.Find("PenguinSmall").GetComponent<Animation>().enabled = false;
            GameObject.Find("PenguinBig").GetComponent<Animation>().enabled = false;
            GameObject.Find("PenguinSmall").transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
            GameObject.Find("PenguinBig").transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
            //make rabbits invisible and pause animation:
            GameObject Rabbits = GameObject.Find("Rabbits");
            foreach (GameObject rabbit in ChildrenToList(Rabbits))
            {
                rabbit.GetComponent<Animator>().enabled = false;
                rabbit.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
                rabbit.transform.GetChild(2).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
            }    
            //make 3D buttons invisible:
            buttons = GameObject.FindGameObjectsWithTag("3Dbuttons");
            foreach (GameObject button in buttons)
            {
                button.GetComponent<MeshRenderer>().enabled = false;
            }
        }
        else
        {
            Debug.Log(e.target.name + " was clicked, but we ignored it");
        }


        List<GameObject> ChildrenToList(GameObject game_object)
        {
            int children = game_object.transform.childCount;
            List<GameObject> ItemsInGroup = new List<GameObject>();
            for (int i = 0; i < children; ++i)
            {
                ItemsInGroup.Add(game_object.transform.GetChild(i).gameObject);
            }
            return ItemsInGroup;
        }
    }
}
