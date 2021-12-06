using System.Collections;
using UnityEngine;

//Script checks whether the player has solved all tasks in system control scene
public class SystemControlSucceded : MonoBehaviour
{
    bool start, stop, pause, day, night, musicOn, musicOff, helpOn, helpOff = false;
    bool taskCompleted = false;
    bool notWonAlready = true;

    public void Update()
    {
        if (start & stop & pause & day & night & musicOn & musicOff & helpOn & helpOff)
        {
            taskCompleted = true;
            print("taskCompleted: " + taskCompleted);
        }

        //Quelle: https://forum.unity.com/threads/wait-for-second-without-startcouroutine.410320/
        if (taskCompleted & notWonAlready)
        {
            StartCoroutine(TaskCompleted());
            notWonAlready = false;
        }
    }

    private IEnumerator TaskCompleted()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<AudioManager>().PlayAudio("WinningSound");
    }

    public void UsedStart() => start = true;
    public void UsedStop() => stop = true;
    public void UsedPause() => pause = true;
    public void UsedDay() => day = true;
    public void UsedNight() => night = true;
    public void UsedMusicOn() => musicOn = true;
    public void UsedMusicOff() => musicOff = true;
    public void UsedHelpOn() => helpOn = true;
    public void UsedHelpOff() => helpOff = true;
}
