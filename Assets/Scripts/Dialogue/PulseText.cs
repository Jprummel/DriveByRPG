/*
	PulseText.cs
	Created 11/7/2017 3:00:55 PM
	Project DriveBy RPG by DefaultCompany
*/
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
	public class PulseText : MonoBehaviour 
	{
        private void Start()
        {
            Pulse();
        }

        private void Pulse()
        {
            Sequence sqn = DOTween.Sequence();

            sqn.Append(transform.DOScale(Vector3.one * 1.2f, .5f));
            sqn.SetLoops(-1, LoopType.Yoyo);
        }
	}
}