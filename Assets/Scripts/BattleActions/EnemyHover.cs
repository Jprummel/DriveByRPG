using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class EnemyHover : MonoBehaviour
{
    private Character _character;
    private SpriteRenderer _sr;
    private Button _button;
    public Button CorrespondingButton
    {
        get { return _button; }
        set { _button = value; }
    }

    private void Start()
    {
        _character = GetComponent<Character>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (ManageTargeting.IsEnabled)
        {
            SetActionTarget.SetTarget(_character);
        }
    }

    private void OnMouseEnter()
    {
        if (ManageTargeting.IsEnabled)
        {
            _sr.color = Color.red;
            _button.image.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        _sr.color = Color.white;
        _button.image.color = Color.white;
    }

    
}
