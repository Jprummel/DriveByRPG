using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	
	void Update () {
        Inputs();
	}

    void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ProgressionPanel.OnPanelToggle();
        }
    }
}
