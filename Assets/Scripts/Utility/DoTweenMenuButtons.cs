/*
	DoTweenMenuButtons.cs
	Created 11/20/2017 1:34:07 PM
	Project DriveBy RPG by Base Games
*/
using DG.Tweening;
using UnityEngine;

namespace Utility
{
	public class DoTweenMenuButtons : MonoBehaviour 
	{
        private void OnEnable()
        {

            transform.localScale = Vector3.one * 10f;

            Sequence slamSequence = DOTween.Sequence();
            slamSequence.Append(transform.DOScale(Vector3.one/10f, 1));
            slamSequence.Append(transform.DORotate(new Vector3(0, 0, -360), .5f, RotateMode.FastBeyond360));
            slamSequence.Join(transform.DOScale(Vector3.one * 1.2f, .5f));
            slamSequence.Append(transform.DOScale(Vector3.one, .25f));


            slamSequence.SetLoops(1);
        }
    }
}