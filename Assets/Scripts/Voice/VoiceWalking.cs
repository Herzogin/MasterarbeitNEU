using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

//Script starts listener, defines keywords and which action they should trigger for navigation scene.
//Inspired by: https://docs.microsoft.com/en-us/windows/mixed-reality/develop/unity/voice-input-in-unity
public class VoiceWalking : MonoBehaviour
{
    GameObject game_object;
    public GameObject CameraRig;
    Walk walk;
    SceneSwitch sceneSwitch;
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    
    void Start()
    {
        walk = CameraRig.GetComponent<Walk>();
        sceneSwitch = FindObjectOfType(typeof(SceneSwitch)) as SceneSwitch;

        //__________________________________________________
        //show help for voice commands:
        keywords.Add("Hilfe an", () =>
        {
            FindObjectOfType<AudioManager>().PlayAudio("HelpOnSound");
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
        //start and stop walking
        keywords.Add("los", () => {
            walk.SetMovementSpeed(4);
        });

        keywords.Add("stop", () => {
            walk.SetMovementSpeed(0);
        });
        //__________________________________________________
        //increase or decrease walking speed
        keywords.Add("schneller", () => {
            float lastSpeed = walk.GetMovementSpeed();
            if (lastSpeed < 15)
            {
                walk.SetMovementSpeed(lastSpeed * 2);
            }
        });

        keywords.Add("langsamer", () => {
            float lastSpeed = walk.GetMovementSpeed();
            if (lastSpeed > 3)
            {
                walk.SetMovementSpeed(lastSpeed / 2);
            }

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
}
