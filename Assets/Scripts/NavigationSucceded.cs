using System.Collections;
using UnityEngine;

public class NavigationSucceded : MonoBehaviour
{
    GameObject CornerPosts;
    bool taskCompleted = false;
    bool notWonalready = true;
   

    public void Start()
    {
        CornerPosts = GameObject.Find("CornerPosts");
    }

    public void Update()
    {
        if (AllCornerPostsAltered())
        {
            taskCompleted = true;
            print("taskCompleted: " + taskCompleted);
        }
        
        if (taskCompleted & notWonalready)
        {
            StartCoroutine(TaskCompleted());
            
            notWonalready = false;
        }
    }

    private IEnumerator TaskCompleted()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<AudioManager>().PlayAudio("WinningSound");
        
    }

    bool AllCornerPostsAltered()
    {
        if (
            CornerPosts.transform.GetChild(0).tag == "altered" &
            CornerPosts.transform.GetChild(1).tag == "altered" &
            CornerPosts.transform.GetChild(2).tag == "altered" &
            CornerPosts.transform.GetChild(3).tag == "altered"
            )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

