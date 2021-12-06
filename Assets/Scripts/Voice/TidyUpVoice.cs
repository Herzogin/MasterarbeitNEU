using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
using System.Linq;

//Script starts listener, defines keywords and which action they should trigger for the tidy up scene.
//Inspired by: https://docs.microsoft.com/en-us/windows/mixed-reality/develop/unity/voice-input-in-unity
public class TidyUpVoice : MonoBehaviour
{
    GameObject game_object;
    TidyUp tidyUp;
    SceneSwitch sceneSwitch;
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    void Start()
    {
        game_object = GameObject.Find("HelperObject");
        tidyUp = FindObjectOfType(typeof(TidyUp)) as TidyUp;
        sceneSwitch = FindObjectOfType(typeof(SceneSwitch)) as SceneSwitch;
        //__________________________________________________
        //show help for voice commands:
        keywords.Add("Hilfe an", () =>
        {
            FindObjectOfType<AudioManager>().PlayAudio("HelpOnSound");
            GameObject.Find("InfoCanvasVoice").GetComponent<Canvas>().enabled = false;
            GameObject.Find("VoicecommandCanvas").GetComponent<Canvas>().enabled = true;
        });
        keywords.Add("Hilfe aus", () =>
        {
            FindObjectOfType<AudioManager>().PlayAudio("HelpOffSound");
            GameObject.Find("VoicecommandCanvas").GetComponent<Canvas>().enabled = false;
        });
        //__________________________________________________
        // go back to first scene:
        keywords.Add("zurück", () => { sceneSwitch.GetComponent<SceneSwitch>().switchToScene("VoiceSystemControlScene"); });

        keywords.Add("Anfang", () => { sceneSwitch.GetComponent<SceneSwitch>().switchToScene("VoiceSystemControlScene"); });
        //__________________________________________________
        //select Parent-GameObject:
        keywords.Add("Kugeln", () => {
            game_object = GameObject.Find("Spheres");
            LiftUp(Utils.ChildrenToList(game_object));
        });

        keywords.Add("Würfel", () => {
            game_object = GameObject.Find("Cubes");
            LiftUp(Utils.ChildrenToList(game_object));
        });

        keywords.Add("Kapseln", () => {
            game_object = GameObject.Find("Capsules");
            LiftUp(Utils.ChildrenToList(game_object));
        });

        keywords.Add("Zylinder", () => {
            game_object = GameObject.Find("Cylinder");
            LiftUp(Utils.ChildrenToList(game_object));
        });

        keywords.Add("keins", () => {
            game_object = GameObject.Find("HelperObject");
        });
        //__________________________________________________
        //define location:
        keywords.Add("auf blau", () => {
            tidyUp.PlaceInBlue(Utils.ChildrenToList(game_object));
        });

        keywords.Add("auf rot", () => {
            
            tidyUp.PlaceInRed(Utils.ChildrenToList(game_object));
        });

        keywords.Add("auf gelb", () => {
            tidyUp.PlaceInYellow(Utils.ChildrenToList(game_object));
        });

        keywords.Add("auf grün", () => {
            tidyUp.PlaceInGreen(Utils.ChildrenToList(game_object));
        });
        //__________________________________________________

        //start listener:
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

    //lifts all objects in a group when the group is selected:
    void LiftUp(List<GameObject> itemList)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            itemList[i].transform.position += Vector3.up;
        }
    }
}
