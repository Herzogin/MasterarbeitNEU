using System.Collections;
using UnityEngine;

//Script checks whether the player has solved all tasks in manipulation scene
public class ManipulationSucceeded : MonoBehaviour
{
    int changedSizeCount = 0;
    int changedColorCount = 0;
    int changedPositionCount = 0;
    int rotatedCount = 0;
    int deletedCount = 0;
    bool taskCompleted = false;
    bool notWonAlready = true;


    public void Update()
    {
        if (changedSizeCount > 1 &
            changedColorCount > 1 &
            changedPositionCount > 1 &
            rotatedCount > 1 &
            deletedCount > 0
            )
        {
            taskCompleted = true;
            print("taskCompleted: " + taskCompleted);
        }

        //Source: https://forum.unity.com/threads/wait-for-second-without-startcouroutine.410320/
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

    public void IncreaseChangedSizeCount()
    {
        changedSizeCount += 1;
        print("changedSizeCount: " + changedSizeCount);
    }

    public void IncreaseChangedColorCount()
    {
        changedColorCount += 1;
        print("changedColorCount: " + changedColorCount);
    }

    public void IncreaseChangedPositionCount()
    {
        changedPositionCount += 1;
        print("changedPositionCount: " + changedPositionCount);
    }

    public void IncreaseRotatedCount()
    {
        rotatedCount += 1;
        print("rotatedCount: " + rotatedCount);
    }

    public void IncreaseDeletedCount()
    {
        deletedCount += 1;
        print("deletedCount: " + deletedCount);
    }
}
