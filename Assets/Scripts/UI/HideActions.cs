using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HideActions : MonoBehaviour {

    [SerializeField] private List<Transform> _actionButtons = new List<Transform>();
    private static List<Transform> actionButtons = new List<Transform>();

    private void Awake()
    {
        for (int i = 0; i < _actionButtons.Count; i++)
        {
            actionButtons.Add(_actionButtons[i]);
        }
    }

    public static void HideButtons()
    {
            for (int i = 0; i < actionButtons.Count; i++)
            {
                actionButtons[i].DOScaleX(0, 0.25f);
            }
    }

    public static void ShowButtons()
    {
        for (int i = 0; i < actionButtons.Count; i++)
        {
            actionButtons[i].DOScaleX(1, 0.25f);
        }
    }
}
