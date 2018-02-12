using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace PostCombat
{
    public class PostCombatXPCounter : MonoBehaviour
    {

        public delegate void LevelUIEvent();
        public delegate void XpBarEvent();
        public static LevelUIEvent OnLevelChange;
        public static XpBarEvent OnXPGain;

        [SerializeField] private PartyMember _partyMember;
        [SerializeField] private Image _levelBar;
        [SerializeField] private Text _level;

        void Awake()
        {
            OnLevelChange += SetLevelText;
            OnXPGain += FillXpBar;

            OnXPGain();
        }

        void SetLevelText()
        {
            _level.text = "Lv. " + _partyMember.Level.ToString();
        }


        void FillXpBar()
        {
            _levelBar.fillAmount = _partyMember.CurrentXP / _partyMember.RequiredXP;
            if(_partyMember.CurrentXP >= _partyMember.RequiredXP)
            {
                Leveling.OnLevelUp(_partyMember);
                _levelBar.fillAmount = 0;
            }
        }

        private void OnDisable()
        {
            OnLevelChange -= SetLevelText;
            OnXPGain -= FillXpBar;
        }
    }
}