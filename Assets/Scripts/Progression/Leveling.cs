using System.Collections;
using UnityEngine;
using DG.Tweening;
using PostCombat;

public class Leveling : MonoBehaviour {

    public delegate void ExperienceIncreaseEvent(PartyMember character, float experienceToAdd);
    public delegate void LevelUpEvent(PartyMember character);
    public static ExperienceIncreaseEvent OnExperienceIncreaseOverTime;
    public static ExperienceIncreaseEvent OnExperienceIncreaseInstant;
    public static LevelUpEvent OnLevelUp;

    private Sequence _xpTween;

    private void Awake()
    {
        OnExperienceIncreaseOverTime += AddExperienceOverTime;
        OnExperienceIncreaseInstant += AddExperienceInstant;
        OnLevelUp += LevelUp;
    }

    void AddExperienceOverTime(PartyMember character,float experienceToAdd)
    {
        if (character.Level < 20) //Sets the max level at 20
        {
            _xpTween.Append(DOTween.To(() => character.CurrentXP, x => character.CurrentXP = x, character.CurrentXP + experienceToAdd, 3).SetOptions(true));
            
            //if (character.CurrentXP >= character.RequiredXP)
            //{
            //    LevelUp(character);
            //}
        }
    }

    void AddExperienceInstant(PartyMember character, float experienceToAdd)
    {
        if(character.Level < 20)
        {
            character.CurrentXP += experienceToAdd;
            if (PostCombatXPCounter.OnXPGain != null)
            {
                PostCombatXPCounter.OnXPGain();
            }
            if(character.CurrentXP >= character.RequiredXP)
            {
                LevelUp(character);
            }
        }
    }

    void LevelUp(PartyMember character)
    {
        if (character.Level < 20)
        {
            if (character.CurrentXP >= character.RequiredXP)
            {
                float xpToAdd = CombatRewards.XPEarned;
                _xpTween.Kill();
                PostCombatXPCounter.OnLevelChange();
                character.Level++;
                character.StatPoints += 3;
                float xpLeftOver = character.CurrentXP - character.RequiredXP;
                character.RequiredXP = (Mathf.RoundToInt(character.RequiredXP * 1.03f + 50));
                character.CurrentXP = xpLeftOver;
                SkillProgression.OnSkillProgression(character);
                

                if (PostCombatXPCounter.OnLevelChange != null)
                {
                    PostCombatXPCounter.OnLevelChange();
                }

                if (PostCombatXPCounter.OnXPGain != null)
                {
                    PostCombatXPCounter.OnXPGain();
                }
                AddExperienceOverTime(character, CombatRewards.XPEarned);

                if (character.CurrentXP >= character.RequiredXP)
                {
                    LevelUp(character);
                }
            }
        }
    }

    private void OnDisable()
    {
        OnExperienceIncreaseOverTime -= AddExperienceOverTime;
        OnExperienceIncreaseInstant -= AddExperienceInstant;
        OnLevelUp -= LevelUp;
    }
}
