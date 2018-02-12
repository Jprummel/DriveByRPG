/*
	CombatEnterEffect.cs
	Created 11/20/2017 4:39:56 PM
	Project DriveBy RPG by Base Games
*/
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Utility
{
	public class CombatEnterEffect : MonoBehaviour 
	{
        [SerializeField] private Material _transitionMaterial;

        [SerializeField] private Image _imageToFlash;
        private void OnEnable()
        {
            //TweenCallback callback = DarkenScreen;

            //Sequence flashSequence = DOTween.Sequence();
            //flashSequence.Append(_imageToFlash.DOFade(0.5f, .1f));
            //flashSequence.Append(_imageToFlash.DOFade(0.75f, .1f)).SetEase(Ease.Flash);
            //flashSequence.onComplete += DarkenScreen;
            //flashSequence.SetLoops(Random.Range(5, 8), LoopType.Yoyo);
            StartCoroutine(BattleTransition());
        }

        private void DarkenScreen()
        {
            _imageToFlash.DOFade(1f, .25f);
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dst)
        {
            Graphics.Blit(src, dst, _transitionMaterial);
        }

        private IEnumerator BattleTransition()
        {
            float cutoff = 0f;
            while(cutoff < 1f)
            {
                cutoff += 0.15f;
                _transitionMaterial.SetFloat("_Cutoff", cutoff);
                yield return new WaitForSeconds(.1f);
            }
        }

        private void OnDisable()
        {
            _transitionMaterial.SetFloat("_Cutoff", 0f);
        }
    }
}