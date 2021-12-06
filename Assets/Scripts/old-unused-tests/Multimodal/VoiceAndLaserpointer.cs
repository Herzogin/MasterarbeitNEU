using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;
using UnityEngine.Windows.Speech;
using System.Linq;
using UnityEngine.UI;
using Valve.VR;
using System;

public class VoiceAndLaserpointer : MonoBehaviour
{
    ObjectManipulation objectManipulation;
    //public Text voiceInput;
    public SteamVR_LaserPointer rightLaserPointer;
    public SteamVR_LaserPointer leftLaserPointer;
    public SteamVR_Action_Boolean select_object = null;
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    GameObject helperObject;
    GameObject pointerInsideObject;
    GameObject targetGameObject;
    bool gripPressed = false;

    void Awake()
    {

        leftLaserPointer.PointerIn += LeftPointerInside;
        leftLaserPointer.PointerOut += LeftPointerOutside;
        leftLaserPointer.PointerClick += LeftPointerClick;

        rightLaserPointer.PointerIn += RightPointerInside;
        rightLaserPointer.PointerOut += RightPointerOutside;
        rightLaserPointer.PointerClick += RightPointerClick;
    }

    

    public void Update()
    {
        if (select_object.GetLastStateDown(SteamVR_Input_Sources.Any))
        {
            gripPressed = true;
            print("Grip pressed " + gripPressed);
        }
        else if (select_object.GetLastStateUp(SteamVR_Input_Sources.Any))
        {
            gripPressed = false;
            print("Grip pressed " + gripPressed);
        }




        if (gripPressed)
        {
            targetGameObject = pointerInsideObject;
        }
        else
        {
            targetGameObject = helperObject;
        }
    }

        // Start is called before the first frame update
        void Start()
    {
        helperObject = GameObject.Find("HelperObject");
        objectManipulation = FindObjectOfType(typeof(ObjectManipulation)) as ObjectManipulation;
        keywords.Add("blau", (() =>
        {
            Debug.Log("blue");
            objectManipulation.PaintObject(targetGameObject, Color.blue);
        }));

        keywords.Add("grün", (() =>
        {
            Debug.Log("green");
            objectManipulation.PaintObject(targetGameObject, Color.green);
        }));

        keywords.Add("gelb", (() =>
        {
            Debug.Log("yellow");
            objectManipulation.PaintObject(targetGameObject, Color.yellow);
        }));

        keywords.Add("rot", (() =>
        {
            Debug.Log("red");
            objectManipulation.PaintObject(targetGameObject, Color.red);
        }));

        keywords.Add("pink", (() =>
        {
            Debug.Log("magenta");
            objectManipulation.PaintObject(targetGameObject, Color.magenta);
        }));

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;

        keywordRecognizer.Start();
    }

    private void LeftPointerClick(object sender, PointerEventArgs e)
    {
        //targetGameObject = e.target.gameObject;
    }



    private void LeftPointerInside(object sender, PointerEventArgs e)
    {
        string name = e.target.name;
        Debug.Log(name + " was entered");
        pointerInsideObject = e.target.gameObject;
        
    }

    private void LeftPointerOutside(object sender, PointerEventArgs e)
    {
        pointerInsideObject = helperObject;
    }

    private void RightPointerClick(object sender, PointerEventArgs e)
        {
            throw new NotImplementedException();
        }

    private void RightPointerOutside(object sender, PointerEventArgs e)
     {
        pointerInsideObject = helperObject;
    }

        private void RightPointerInside(object sender, PointerEventArgs e)
        {
        string name = e.target.name;
        Debug.Log(name + " was entered");
        pointerInsideObject = e.target.gameObject;
    }
    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        //voiceInput.text = args.text;
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    
}
