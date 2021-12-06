using UnityEngine;

public class StartScene : MonoBehaviour
{
    public GameObject Walls;
    private void Awake()
    {
        Walls.SetActive(true);
    }
}
