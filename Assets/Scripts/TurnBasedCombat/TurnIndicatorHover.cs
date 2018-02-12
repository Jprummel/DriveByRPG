using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurnIndicatorHover : MonoBehaviour
{

    private Sequence _hoverSqn;
    public delegate void ResetIndicator();
    public static ResetIndicator OnResetIndicator;

    private void OnEnable()
    {
        OnResetIndicator += ResetTween;
    }

    private void ResetTween()
    {
        if (_hoverSqn != null)
            _hoverSqn.Kill();

        _hoverSqn = DOTween.Sequence();

        _hoverSqn.Append(transform.DOMoveY(transform.position.y - 0.2f, 0.75f));
        _hoverSqn.SetLoops(-1, loopType: LoopType.Yoyo);
    }

    private void OnDisable()
    {
        OnResetIndicator -= ResetTween;
    }
}