using UnityEngine;
using Valve.VR;

//Script enables player come back to the System Control Scene by pushing controllers menu-button.
public class ControllerSceneSwitch : MonoBehaviour
{
    public SteamVR_Action_Boolean MenuButton = null;
    SceneSwitch sceneSwitch;

    void Start()
    {
        sceneSwitch = FindObjectOfType(typeof(SceneSwitch)) as SceneSwitch;
    }

    private void Update()
    {
        if (MenuButton.GetLastStateDown(SteamVR_Input_Sources.Any))
        {
            print("MenuButton pressed ");
        }
        else if (MenuButton.GetLastStateUp(SteamVR_Input_Sources.Any))
        {
            print("MenuButton released");
            sceneSwitch.GetComponent<SceneSwitch>().switchToScene("SystemControlScene");
        }
    }

}
