using UnityEngine;

//Script to change time of day of a scene.
public class SkyboxController : MonoBehaviour
{
    public Material daySky;
    public Material nightSky;

    public void Awake()
    {
        RenderSettings.skybox = daySky;
    }

    public void SkyToDay()
    {
        RenderSettings.skybox = daySky;
    }

    public void SkyToNight()
    {
        RenderSettings.skybox = nightSky;
    }
}
