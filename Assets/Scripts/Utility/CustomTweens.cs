using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CustomTweens : MonoBehaviour {

    /// <summary>
    /// This tweens a float between 2 values and snaps it to ints
    /// </summary>
    /// <param name="startValue"></param>
    /// <param name="endValue"></param>
    /// <param name="duration"></param>
    public static void DoFloat(float startValue,float endValue,float duration)
    {
        DOTween.To(() => startValue, x => startValue = x, endValue, duration).SetOptions(true);
        Debug.Log("tweening float");
    }
}
