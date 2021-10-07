using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
using System.Linq;

public class TidyUpVoice : MonoBehaviour
{
    GameObject game_object;
    TidyUp tidyUp;
    SceneSwitch sceneSwitch;
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    // Start is called before the first frame update
    void Start()
    {
        game_object = GameObject.Find("Plane");
        tidyUp = GameObject.FindObjectOfType(typeof(TidyUp)) as TidyUp;
        sceneSwitch = GameObject.FindObjectOfType(typeof(SceneSwitch)) as SceneSwitch;

        // go back to first scene:
        keywords.Add("zurück", () => { sceneSwitch.GetComponent<SceneSwitch>().switchToScene("SystemControlScene"); });

        keywords.Add("Anfang", () => { sceneSwitch.GetComponent<SceneSwitch>().switchToScene("SystemControlScene"); });

        //select Parent-GameObject:
        keywords.Add("Kugeln", () => {
            game_object = GameObject.Find("Spheres");
            LiftUp(ChildrenToList(game_object));
        });

        keywords.Add("Würfel", () => {
            game_object = GameObject.Find("Cubes");
            LiftUp(ChildrenToList(game_object));
        });

        keywords.Add("Kapseln", () => {
            game_object = GameObject.Find("Capsules");
            LiftUp(ChildrenToList(game_object));
        });

        keywords.Add("Zylinder", () => {
            game_object = GameObject.Find("Cylinder");
            LiftUp(ChildrenToList(game_object));
        });

        //define location
        keywords.Add("auf blau", () => {
            tidyUp.PlaceInBlue(ChildrenToList(game_object));
        });

        keywords.Add("auf rot", () => {
            
            tidyUp.PlaceInRed(ChildrenToList(game_object));
        });

        keywords.Add("auf gelb", () => {
            tidyUp.PlaceInYellow(ChildrenToList(game_object));
        });

        keywords.Add("auf grün", () => {
            tidyUp.PlaceInGreen(ChildrenToList(game_object));
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

    List<GameObject> ChildrenToList(GameObject selectedGroup)
    {
        int children = game_object.transform.childCount;
        List<GameObject> ItemsInGroup = new List<GameObject>();
        for (int i = 0; i < children; ++i)
        {
            ItemsInGroup.Add(game_object.transform.GetChild(i).gameObject);
        }
        return ItemsInGroup;
    }

    void LiftUp(List<GameObject> itemList)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            itemList[i].transform.position += Vector3.up;
        }
    }
}
