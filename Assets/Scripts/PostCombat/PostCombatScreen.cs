using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Serialization;
using DG.Tweening;
using System.Collections;

namespace PostCombat
{
    public class PostCombatScreen : MonoBehaviour
    {


        [SerializeField] private List<Transform> _experienceCounters = new List<Transform>();
        [SerializeField] private Text _gainedXpText;
        private bool _isCountingDown;
        private PartyContainer _partyContainer;

        private void Awake()
        {
            _partyContainer = GameObject.FindGameObjectWithTag(Tags.PARTYCONTAINER).GetComponent<PartyContainer>();
        }

        void Start()
        {
            SaveCharacters.Instance.LoadMembers();
            for (int i = 0; i < _partyContainer.PartyMembers.Count; i++)
            {
                PostCombatXPCounter.OnXPGain();
                PostCombatXPCounter.OnLevelChange();
                
            }
            XPCountersSlideIn(Screen.width / 2); //Slides the counters to the center (X) of the screen
        }

        private void Update()
        {
            for (int i = 0; i < _partyContainer.PartyMembers.Count; i++)
            {
                _gainedXpText.text = "XP Earned" + "\n" + CombatRewards.XPEarned.ToString();
            }
            PostCombatXPCounter.OnXPGain();
        }


        void XPCountersSlideIn(float moveValue)
        {
            Sequence xpTweenSequence = DOTween.Sequence();
            xpTweenSequence.Append(_gainedXpText.rectTransform.DOMoveY(900, 1).SetEase(Ease.InExpo));
            for (int i = 0; i < _experienceCounters.Count; i++)
            {
                
                xpTweenSequence.Append(_experienceCounters[i].DOMoveX(moveValue, 0.4f).SetEase(Ease.OutBack, overshoot: 1f));
                _gainedXpText.text = CombatRewards.XPEarned.ToString();
            }
            xpTweenSequence.onComplete += GiveXPToPartyOverTime;
            xpTweenSequence.onComplete += XPCountDown;
        }

        public void XPCountersSlideOut(float moveValue)
        {
            Sequence xpTweenSequence = DOTween.Sequence();
            for (int i = 0; i < _experienceCounters.Count; i++)
            {
                xpTweenSequence.Append(_experienceCounters[i].DOMoveX(moveValue, 0.4f).SetEase(Ease.InBack, overshoot: 1f));
            }
            xpTweenSequence.onComplete += ReturnToWorldScene;
        }

        void XPCountDown()
        {
            Sequence tweenSequence = DOTween.Sequence();
            tweenSequence.Append(DOTween.To(() => CombatRewards.XPEarned, x => CombatRewards.XPEarned = x, 0, 2).SetOptions(true));
        }

        void GiveXPToPartyOverTime()
        {
            //Gives all party members the XP earned in their last battle
            for (int i = 0; i < _partyContainer.PartyMembers.Count; i++)
            {
                Leveling.OnExperienceIncreaseOverTime(_partyContainer.PartyMembers[i], CombatRewards.XPEarned);
            }
        }

        void GiveXPToPartyInstant()
        {
            for (int i = 0; i < _partyContainer.PartyMembers.Count; i++)
            {
                Leveling.OnExperienceIncreaseInstant(_partyContainer.PartyMembers[i], CombatRewards.XPEarned);
            }
            CombatRewards.XPEarned = 0;
        }

        public void ReturnToWorldScene()
        {
            GiveXPToPartyInstant();
            PostCombatXPCounter.OnXPGain();
            CombatRewards.XPEarned = 0; // Sets earned xp to 0 in case player skips post combat screen
            SaveCharacters.Instance.SavePartyMembers();
            SceneManager.LoadScene(PlayerLocation.SceneName);
        }
    }
}