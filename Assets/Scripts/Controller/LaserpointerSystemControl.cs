using UnityEngine;
using Valve.VR.Extras;

//Script enables player to choose a 3d-button out of a menu and defines, which action is triggered, if a 3d-button is clicked.
public class LaserpointerSystemControl : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;
    SceneSwitch sceneSwitch;
    SkyboxController skyboxScript;
    SystemControlSucceded systemControlSucceded;
    GameObject[] buttons;

    void Awake()
    {
        laserPointer.PointerClick += PointerClick;
    }

    private void Start()
    {
        sceneSwitch = FindObjectOfType(typeof(SceneSwitch)) as SceneSwitch;
        skyboxScript = FindObjectOfType(typeof(SkyboxController)) as SkyboxController;
        systemControlSucceded = FindObjectOfType(typeof(SystemControlSucceded)) as SystemControlSucceded;
    } 

    //checks with 3d-button is clicked by controllers laserpointer:
    public void PointerClick(object sender, PointerEventArgs e)
    {
        //3d buttons to change status music:
        if (e.target.name == "MusicOn")
        {
            systemControlSucceded.UsedMusicOn();
            FindObjectOfType<AudioManager>().UnPauseAudio("BackgroundSound");
        }
        else if (e.target.name == "MusicOff")
        {
            systemControlSucceded.UsedMusicOff();
            FindObjectOfType<AudioManager>().PauseAudio("BackgroundSound");
        }
        //_______________________________________________
        //3d buttons to change time of day:
        else if (e.target.name == "sunSwitch")
        {
            systemControlSucceded.UsedDay();
            skyboxScript.SkyToDay();
        }
        else if (e.target.name == "moonSwitch")
        {
            systemControlSucceded.UsedNight();
            skyboxScript.SkyToNight();
        }
        //_______________________________________________
        //3d buttons to change game status:
        else if (e.target.name == "PlayButton")
        {
            systemControlSucceded.UsedStart();
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
            foreach (GameObject rabbit in Utils.ChildrenToList(Rabbits))
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
            systemControlSucceded.UsedPause();
            FindObjectOfType<AudioManager>().PauseAudio("BackgroundSound");
            //pause penguins animation:
            GameObject.Find("PenguinSmall").GetComponent<Animation>().enabled = false;
            GameObject.Find("PenguinBig").GetComponent<Animation>().enabled = false;
            //pause rabbits animation:
            GameObject Rabbits = GameObject.Find("Rabbits");
            foreach (GameObject rabbit in Utils.ChildrenToList(Rabbits))
            {
                rabbit.GetComponent<Animator>().enabled = false;
            }
        }
        else if (e.target.name == "StopButton")
        {
            systemControlSucceded.UsedStop();
            print(e.target.name + " was clicked");
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
            foreach (GameObject rabbit in Utils.ChildrenToList(Rabbits))
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
        //_______________________________________________
        //3d buttons to change scenes:
        else if (e.target.name == "tidyUpVoiceButton")
        {
            FindObjectOfType<AudioManager>().PlayAudio("SceneSwitchSound");
            sceneSwitch.GetComponent<SceneSwitch>().switchToScene("VoiceTidyUpScene");
        }
        else if (e.target.name == "tidyUpControllerButton")
        {
            FindObjectOfType<AudioManager>().PlayAudio("SceneSwitchSound");
            sceneSwitch.GetComponent<SceneSwitch>().switchToScene("ControllerTidyUpScene");
        }
        else if (e.target.name == "walkingVoiceButton")
        {
            FindObjectOfType<AudioManager>().PlayAudio("SceneSwitchSound");
            sceneSwitch.GetComponent<SceneSwitch>().switchToScene("VoiceWalkingScene");
        }
        else if (e.target.name == "walkingControllerButton")
        {
            FindObjectOfType<AudioManager>().PlayAudio("SceneSwitchSound");
            sceneSwitch.GetComponent<SceneSwitch>().switchToScene("ControllerWalkingScene");
        }
        else if (e.target.name == "ManipulationVoiceButton")
        {
            FindObjectOfType<AudioManager>().PlayAudio("SceneSwitchSound");
            sceneSwitch.GetComponent<SceneSwitch>().switchToScene("VoiceSelectManipulationScene");
        }
        else if (e.target.name == "ManipulationControllerButton")
        {
            FindObjectOfType<AudioManager>().PlayAudio("SceneSwitchSound");
            sceneSwitch.GetComponent<SceneSwitch>().switchToScene("ControllerSelectManipulationScene");
        }
        //_______________________________________________
        //checks if clicked object is an 3d-button at all:
        else
        {
            print(e.target.name + " was clicked, but we ignored it");
        }
    }
}
