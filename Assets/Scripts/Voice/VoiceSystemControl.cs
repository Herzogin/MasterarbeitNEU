using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using UnityEngine.UI;

public class VoiceSystemControl : MonoBehaviour
{

    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    SceneSwitch sceneSwitch;
    SystemControlSucceded systemControlSucceded;
    SkyboxController skyboxScript;
    GameObject[] buttons;
    GameObject MainCanvas;


    void Start()
    {
        sceneSwitch = FindObjectOfType(typeof(SceneSwitch)) as SceneSwitch;
        skyboxScript = FindObjectOfType(typeof(SkyboxController)) as SkyboxController;
        systemControlSucceded = FindObjectOfType(typeof(SystemControlSucceded)) as SystemControlSucceded;
        MainCanvas = GameObject.Find("MainCanvas");


        keywords.Add("Hilfe an", () =>
        {
            systemControlSucceded.UsedHelpOn();
            FindObjectOfType<AudioManager>().PlayAudio("HelpOnSound");
            GameObject.Find("InfoCanvasVoice").GetComponent<Canvas>().enabled = false;
            MainCanvas.GetComponent<Canvas>().enabled = true;
        });

        keywords.Add("Hilfe aus", () =>
        {
            systemControlSucceded.UsedHelpOff();
            FindObjectOfType<AudioManager>().PlayAudio("HelpOffSound");
            MainCanvas.GetComponent<Canvas>().enabled = false;
        });

        keywords.Add("Musik an", () => {
            systemControlSucceded.UsedMusicOn();
            FindObjectOfType<AudioManager>().UnPauseAudio("BackgroundSound");
        });

        keywords.Add("Musik aus", () =>{
            systemControlSucceded.UsedMusicOff();
            FindObjectOfType<AudioManager>().PauseAudio("BackgroundSound");
        });

        keywords.Add("Tag", () =>{
            systemControlSucceded.UsedDay();
            skyboxScript.SkyToDay();
            UItextColor(MainCanvas, Color.black);
        });

        keywords.Add("Nacht", () =>{
            systemControlSucceded.UsedNight();
            skyboxScript.SkyToNight();
            UItextColor(MainCanvas, Color.white);
        });

        keywords.Add("Start", () =>{ GameStart(); });
        keywords.Add("Play", () => { GameStart(); });

        void GameStart()
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

        keywords.Add("Pause", () => 
        {
            systemControlSucceded.UsedPause();
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
        });


        keywords.Add("Stop", () =>{ GameStop(); });
        keywords.Add("Halt", () => { GameStop(); });

        void GameStop()
        {
            systemControlSucceded.UsedStop();
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

        keywords.Add("Aufräumen Sprachbefehl", () =>
        {
            FindObjectOfType<AudioManager>().PlayAudio("SceneSwitchSound");
            sceneSwitch.GetComponent<SceneSwitch>().switchToScene("VoiceTidyUpScene");
        });

        keywords.Add("Aufräumen Controller", () =>
        {
            FindObjectOfType<AudioManager>().PlayAudio("SceneSwitchSound");
            sceneSwitch.GetComponent<SceneSwitch>().switchToScene("ControllerTidyUpScene");
        });

        keywords.Add("Gehen Sprachbefehl", () =>
        {
            FindObjectOfType<AudioManager>().PlayAudio("SceneSwitchSound");
            sceneSwitch.GetComponent<SceneSwitch>().switchToScene("VoiceWalkingScene");
        });

        keywords.Add("Gehen Controller", () =>
        {
            FindObjectOfType<AudioManager>().PlayAudio("SceneSwitchSound");
            sceneSwitch.GetComponent<SceneSwitch>().switchToScene("ControllerWalkingScene");
        });

        keywords.Add("Manipulation Sprachbefehl", () =>
        {
            FindObjectOfType<AudioManager>().PlayAudio("SceneSwitchSound");
            sceneSwitch.GetComponent<SceneSwitch>().switchToScene("VoiceSelectManipulationScene");
        });

        keywords.Add("Manipulation Controller", () =>
        {
            FindObjectOfType<AudioManager>().PlayAudio("SceneSwitchSound");
            sceneSwitch.GetComponent<SceneSwitch>().switchToScene("ControllerSelectManipulationScene");
        });







        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;

        keywordRecognizer.Start();
    }


    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        print("you said: " + args.text);
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    public void UItextColor(GameObject Canvas, Color color)
    {
        foreach (GameObject CanvasChild in ChildrenToList(Canvas))
        {
            CanvasChild.transform.GetChild(0).gameObject.transform.GetComponent<Text>().color = color;
            CanvasChild.transform.GetChild(1).gameObject.transform.GetComponent<Text>().color = color;
        }
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
