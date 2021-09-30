﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
using System.Linq;

public class TidyUpVoice : MonoBehaviour
{
    GameObject game_object;
    public TidyUp tidyUp;
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    // Start is called before the first frame update
    void Start()
    {
        game_object = GameObject.Find("Plane");

        //select Parent-GameObject:
        keywords.Add("Kugeln", () => { game_object = GameObject.Find("Spheres"); });

        keywords.Add("Würfel", () => { game_object = GameObject.Find("Cubes"); });

        keywords.Add("Kapseln", () => { game_object = GameObject.Find("Capsules"); });

        keywords.Add("Zylinder", () => { game_object = GameObject.Find("Cylinder"); });

        //define location
        keywords.Add("auf blau", () => {
            int children = game_object.transform.childCount;
            List<GameObject> ItemsInGroup = new List<GameObject>();
            for (int i = 0; i < children; ++i)
                ItemsInGroup.Add(game_object.transform.GetChild(i).gameObject);
            tidyUp.PlaceInBlue(ItemsInGroup);
        });

        keywords.Add("auf rot", () => {
            int children = game_object.transform.childCount;
            List<GameObject> ItemsInGroup = new List<GameObject>();
            for (int i = 0; i < children; ++i)
                ItemsInGroup.Add(game_object.transform.GetChild(i).gameObject);
            tidyUp.PlaceInRed(ItemsInGroup);
        });

        keywords.Add("auf gelb", () => {
            int children = game_object.transform.childCount;
            List<GameObject> ItemsInGroup = new List<GameObject>();
            for (int i = 0; i < children; ++i)
                ItemsInGroup.Add(game_object.transform.GetChild(i).gameObject);
            tidyUp.PlaceInYellow(ItemsInGroup);
        });

        keywords.Add("auf grün", () => {
            int children = game_object.transform.childCount;
            List<GameObject> ItemsInGroup = new List<GameObject>();
            for (int i = 0; i < children; ++i)
                ItemsInGroup.Add(game_object.transform.GetChild(i).gameObject);
            tidyUp.PlaceInGreen(ItemsInGroup);
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