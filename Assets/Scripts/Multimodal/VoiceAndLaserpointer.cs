﻿using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;
using UnityEngine.Windows.Speech;
using System.Linq;
using UnityEngine.UI;

public class VoiceAndLaserpointer : MonoBehaviour
{
    public ObjectManipulation objectManipulation;
    public Text voiceInput;
    public SteamVR_LaserPointer laserPointer;
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    GameObject targetGameObject;

    void Awake()
    {

        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }


    // Start is called before the first frame update
    void Start()
    {
        targetGameObject = null;
        keywords.Add("blue", (System.Action)(() =>
        {
            Debug.Log("blue");
            objectManipulation.PaintObject((GameObject)targetGameObject, (Color)Color.blue);
        }));

        keywords.Add("green", (System.Action)(() =>
        {
            Debug.Log("green");
            objectManipulation.PaintObject((GameObject)targetGameObject, (Color)Color.green);
        }));

        keywords.Add("yellow", (System.Action)(() =>
        {
            Debug.Log("yellow");
            objectManipulation.PaintObject((GameObject)targetGameObject, (Color)Color.yellow);
        }));

        keywords.Add("red", (System.Action)(() =>
        {
            Debug.Log("red");
            objectManipulation.PaintObject((GameObject)targetGameObject, (Color)Color.red);
        }));

        keywords.Add("magenta", (System.Action)(() =>
        {
            Debug.Log("magenta");
            objectManipulation.PaintObject((GameObject)targetGameObject, (Color)Color.magenta);
        }));

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;

        keywordRecognizer.Start();
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        targetGameObject = e.target.gameObject;
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        string name = e.target.name;
        Debug.Log(name + " was entered");
        targetGameObject = e.target.gameObject;
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        targetGameObject = null;
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        voiceInput.text = args.text;
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}
