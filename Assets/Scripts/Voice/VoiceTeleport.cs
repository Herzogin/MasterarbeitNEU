using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VoiceTeleport : MonoBehaviour
{
    public Rigidbody hmd;
    GameObject player;
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    bool stopped;
    float pause = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        print("voice navigation script started");
        player = GameObject.Find("Player");

        keywords.Add("geh", () =>
        {
            Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(0, 0, 2));
            //player.transform.position += 0.5f * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
            player.transform.position += Vector3.ProjectOnPlane(direction, Vector3.up) * 2;
        });

        keywords.Add("rechts", () => { player.transform.position += new Vector3(3, 0, 0); });

        keywords.Add("Schneemann", () =>
        {
            goTo("Schneemann");
        });

        keywords.Add("Kugel", () =>
        {
            goTo("Kugel");
        });

        keywords.Add("Würfel", () =>
        {
            goTo("Würfel");
        });

        keywords.Add("Zylinder", () =>
        {
            goTo("Zylinder");
        });

        //start listener:
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;

        keywordRecognizer.Start();
    }

    private void goTo(string targetName)
    {
        Vector3 target = GameObject.Find(targetName).transform.position;
        target -= new Vector3(1, 1, 1);
        player.transform.position += target;
    }

    private IEnumerator GoToTarget(Vector3 target)
    {
        float speed = 2;
        float timer = 0.0f;
        Vector3 origin = player.transform.position;

        while (timer < speed)
        {
            timer += Time.deltaTime;
            float t = timer / speed;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            transform.position = Vector3.Lerp(origin, target, t);

            yield return null;
        }

        yield return new WaitForSeconds(pause);
        stopped = false;
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
