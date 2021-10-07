using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

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
