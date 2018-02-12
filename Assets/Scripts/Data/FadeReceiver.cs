/*
	FadeReceiver.cs
	Created 11/8/2017 4:04:22 PM
	Project DriveBy RPG by DefaultCompany
*/
using DG.Tweening;
using UnityEngine;

namespace Environment
{
	public class FadeReceiver : MonoBehaviour 
	{
        private SpriteRenderer _sr;

        private void Start()
        {
            _sr = GetComponent<SpriteRenderer>();
        }

        public void FadeBuilding()
        {
            _sr.DOFade(.15f, .1f);
        }

        public void ResetBuilding()
        {
            _sr.DOFade(1f, .1f);
        }
    }
}