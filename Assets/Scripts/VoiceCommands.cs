﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public class VoiceCommands : MonoBehaviour
{
    GameObject game_object;
    public ObjectManipulation objectManipulation;
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();


    void Start()
    {
        game_object = GameObject.Find("Plane");

        //select GameObject:
        keywords.Add("Kugel", () => { game_object = GameObject.Find("Sphere"); });

        keywords.Add("Würfel", () => { game_object = GameObject.Find("Cube"); });

        keywords.Add("Schneemann", () => { game_object = GameObject.Find("snowman"); });

        keywords.Add("Zylinder", () => { game_object = GameObject.Find("Cylinder"); });

        //change color of GameObject:
        keywords.Add("rot", () => { objectManipulation.colorVoice(game_object, Color.red); });

        keywords.Add("gelb", () => { objectManipulation.colorVoice(game_object, Color.yellow); });

        keywords.Add("blau", () => { objectManipulation.colorVoice(game_object, Color.blue); });

        keywords.Add("grün", () => { objectManipulation.colorVoice(game_object, Color.green); });

        //change size of GameObject:
        keywords.Add("größer", () => { objectManipulation.bigger(game_object); });

        keywords.Add("kleiner", () => { objectManipulation.smaller(game_object); });

        //rotate GameObject:
        keywords.Add("drehen", () => { objectManipulation.rotateRight(game_object, 20f); });

        //delete GameObject:
        keywords.Add("löschen", () => { objectManipulation.delete(game_object); });

        //move GameObject:
        keywords.Add("rechts", () => { objectManipulation.moveRight(game_object); });

        keywords.Add("links", () => { objectManipulation.moveLeft(game_object); });

        keywords.Add("nach vorne", () => { objectManipulation.moveForward(game_object); });

        keywords.Add("nach hinten", () => { objectManipulation.moveBack(game_object); });

        keywords.Add("hoch", () => { objectManipulation.moveUp(game_object); });

        keywords.Add("runter", () => { objectManipulation.moveDown(game_object); });

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