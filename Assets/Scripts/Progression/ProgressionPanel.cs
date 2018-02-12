using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionPanel : MonoBehaviour {

    public delegate void PanelToggleEvent();
    public static PanelToggleEvent OnPanelToggle;
    [SerializeField] private GameObject _progressionPanel;
	
    void Awake()
    {
        OnPanelToggle += ToggleProgressionPanel;
    }

    void ToggleProgressionPanel()
    {
        if (_progressionPanel.activeSelf)
        {
            _progressionPanel.SetActive(false);
        }else if (!_progressionPanel.activeSelf)
        {
            _progressionPanel.SetActive(true);
        }
    }

    void OnDisable()
    {
        OnPanelToggle -= ToggleProgressionPanel;
    }
}
