using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public class VoiceWalking : MonoBehaviour
{
    GameObject game_object;
    public GameObject CameraRig;
    Walk walk;
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    private void Awake()
    {
        walk = CameraRig.GetComponent<Walk>();
    }


    // Start is called before the first frame update
    void Start()
    {

        keywords.Add("los", () => {
            walk.SetMovementSpeed(2);
        });

        keywords.Add("stop", () => {
            walk.SetMovementSpeed(0);
        });

        keywords.Add("schneller", () => {
            float lastSpeed = walk.GetMovementSpeed();
            if (lastSpeed < 15)
            {
                walk.SetMovementSpeed(lastSpeed + 3);
            }

        });

        keywords.Add("langsamer", () => {
            float lastSpeed = walk.GetMovementSpeed();
            if (lastSpeed > 3)
            {
                walk.SetMovementSpeed(lastSpeed - 3);
            }

        });


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
