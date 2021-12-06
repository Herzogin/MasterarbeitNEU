using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public class VoiceTeleport2 : MonoBehaviour
{
    public GameObject CameraRig;
    KeywordRecognizer keywordRecognizer;
    Teleport teleport;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();


    private void Awake()
    {
        teleport = CameraRig.GetComponent<Teleport>();
    }


    //Start is called before the first frame update
    void Start()
    {
        print(CameraRig.transform.position + " = start position CameraRig");
        keywords.Add("Schneemann", () =>
        {
            teleport.TeleportTo("Schneemann");
        });

        keywords.Add("Kugel", () =>
        {
             teleport.TeleportTo("Kugel");
            //print(CameraRig.transform.position + " = old position CameraRig");
            //CameraRig.transform.position = new Vector3(4, 2, 4);
            //print(CameraRig.transform.position + " = new position CameraRig");
        });

        keywords.Add("Würfel", () =>
        {
            teleport.TeleportTo("Würfel");
            //print(CameraRig.transform.position + " = old position CameraRig");
            //CameraRig.transform.position = new Vector3(-4, 2, -4);
            //print(CameraRig.transform.position + " = new position CameraRig");
        });

        keywords.Add("Zylinder", () =>
        {
            teleport.TeleportTo("Zylinder");
        });

        //start listener:
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;

        keywordRecognizer.Start();
    }

    //private void TeleportTo(string targetName)
    //{
    //    Vector3 targetPosition = GameObject.Find(targetName).transform.position;
    //    //target -= new Vector3(2, 0, 2);
    //    print("you teleported to: " + targetName);
    //    print(targetPosition + " = position of " + targetName);
    //    print(CameraRig.transform.position + " = position CameraRig");
    //    CameraRig.transform.position += targetPosition;
    //}

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
