using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

//Script starts listener, defines keywords and which action they should trigger for selection and manipulation scene.
//Inspired by: https://docs.microsoft.com/en-us/windows/mixed-reality/develop/unity/voice-input-in-unity
public class VoiceCommands : MonoBehaviour
{
    GameObject game_object;
    ObjectManipulation objectManipulation;
    SceneSwitch sceneSwitch;
    ManipulationSucceeded manipulationSucceded;
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

  
    void Start()
    {
        game_object = GameObject.Find("HelperObject");
        objectManipulation = FindObjectOfType(typeof(ObjectManipulation)) as ObjectManipulation;
        sceneSwitch = FindObjectOfType(typeof(SceneSwitch)) as SceneSwitch;
        manipulationSucceded = FindObjectOfType(typeof(ManipulationSucceeded)) as ManipulationSucceeded;
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
        keywords.Add("zurück", () =>{sceneSwitch.GetComponent<SceneSwitch>().switchToScene("VoiceSystemControlScene");});

        keywords.Add("Anfang", () =>{sceneSwitch.GetComponent<SceneSwitch>().switchToScene("VoiceSystemControlScene");});
        //__________________________________________________
        //select GameObject:
        keywords.Add("Kugel", () => {
            DeHighlightSelectedObject(game_object);
            game_object = GameObject.Find("Sphere");
            HighlightSelectedObject(game_object);
        });

        keywords.Add("Würfel", () => {
            DeHighlightSelectedObject(game_object);
            game_object = GameObject.Find("Cube");
            HighlightSelectedObject(game_object);
        });

        keywords.Add("Kapsel", () => {
            DeHighlightSelectedObject(game_object);
            game_object = GameObject.Find("Capsule");
            HighlightSelectedObject(game_object);
        });

        keywords.Add("Zylinder", () => {
            DeHighlightSelectedObject(game_object);
            game_object = GameObject.Find("Cylinder");
            HighlightSelectedObject(game_object);
        });

        keywords.Add("keins", () => {
            DeHighlightSelectedObject(game_object);
            game_object = GameObject.Find("HelperObject");
            HighlightSelectedObject(game_object);
        });
        //__________________________________________________
        //change color of GameObject:
        keywords.Add("rot", (System.Action)(() => {
            objectManipulation.PaintObject((GameObject)game_object, (Color)Color.red);
            manipulationSucceded.IncreaseChangedColorCount();
        }));

        keywords.Add("gelb", (System.Action)(() => {
            objectManipulation.PaintObject((GameObject)game_object, (Color)Color.yellow);
            manipulationSucceded.IncreaseChangedColorCount();
        }));

        keywords.Add("blau", (System.Action)(() => {
            objectManipulation.PaintObject((GameObject)game_object, (Color)Color.blue);
            manipulationSucceded.IncreaseChangedColorCount();
        }));

        keywords.Add("grün", (System.Action)(() => {
            objectManipulation.PaintObject((GameObject)game_object, (Color)Color.green);
            manipulationSucceded.IncreaseChangedColorCount();
        }));
        //__________________________________________________
        //change size of GameObject:
        keywords.Add("größer", () => {
            objectManipulation.bigger(game_object);
            manipulationSucceded.IncreaseChangedSizeCount();
        });

        keywords.Add("kleiner", () => {
            objectManipulation.smaller(game_object);
            manipulationSucceded.IncreaseChangedSizeCount();
        });
        //__________________________________________________
        //rotate GameObject:
        keywords.Add("drehen", () => {
            objectManipulation.rotateRight(game_object);
            manipulationSucceded.IncreaseRotatedCount();
        });
        //__________________________________________________
        //delete GameObject:
        keywords.Add("löschen", () => {
            objectManipulation.DeleteObject(game_object);
            manipulationSucceded.IncreaseDeletedCount();
        });
        //__________________________________________________
        //move GameObject:
        keywords.Add("rechts", () => {
            objectManipulation.moveRight(game_object);
            manipulationSucceded.IncreaseChangedPositionCount();
        });

        keywords.Add("links", () => {
            objectManipulation.moveLeft(game_object);
            manipulationSucceded.IncreaseChangedPositionCount();
        });

        keywords.Add("nach vorne", () => {
            objectManipulation.moveForward(game_object);
            manipulationSucceded.IncreaseChangedPositionCount();
        });

        keywords.Add("nach hinten", () => {
            objectManipulation.moveBack(game_object);
            manipulationSucceded.IncreaseChangedPositionCount();
        });

        keywords.Add("hoch", () => {
            objectManipulation.moveUp(game_object);
            manipulationSucceded.IncreaseChangedPositionCount();
        });

        keywords.Add("runter", () => {
            objectManipulation.moveDown(game_object);
            manipulationSucceded.IncreaseChangedPositionCount();
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

    void HighlightSelectedObject(GameObject selectedObject)
    {
        print("inside highlight");
        selectedObject.gameObject.transform.GetChild(0).gameObject.transform.GetComponent<TextMesh>().color = Color.red;
    }

    void DeHighlightSelectedObject(GameObject deselectedObject)
    {
        print("inside de-highlight");
        deselectedObject.gameObject.transform.GetChild(0).gameObject.transform.GetComponent<TextMesh>().color = Color.black;
    }
}
