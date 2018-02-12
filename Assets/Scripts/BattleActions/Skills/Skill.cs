using UnityEngine;
[System.Serializable]
public class Skill
{
    protected Character _performer;

    protected float _damageModifier = 5;
    protected float _energyCost;
    protected float _damage;

    public enum AttackType
    {
        MELEE,
        RANGED
    }

    protected AttackType _attackType;

    public enum TargetingType
    {
        SINGLE,
        ALL
    }

    protected TargetingType _targetingType;

    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    protected string _abilityName;
    public string AbilityName
    {
        get { return _abilityName; }
        set { _abilityName = value; }
    }

    public void ExecuteAction()
    {
        ExecuteSkill();
    }

    public void ExecuteSkill()  
    {

        _performer = TurnBasedStateMachine.ActiveCharacter;

        if (_performer.CurrentEnergy >= _energyCost)
        {
            if (_targetingType == TargetingType.SINGLE)
            {
                if (_performer.Target != null)
                {
                    //if (_attackType == AttackType.MELEE)
                    //{
                    //    _damage = _performer.Strength * _damageModifier;
                    //}
                    //else if (_attackType == AttackType.RANGED)
                    //{
                    //    _damage = _performer.Accuracy * _damageModifier;
                    //}
                    CalculateDamage();
                    _performer.Target.CurrentHealth -= _damage;
                    _performer.CurrentEnergy -= _energyCost;
                    ShowCharacterStats.onUpdateHealth();
                    if (TurnBasedStateMachine.ActivePlayers.Contains(_performer))
                    {
                        OpenSkillsPanel.onSkillPanelToggle();
                        ShowCharacterStats.onUpdateEnergy();
                    }
                    _performer.Target.Death(); //Checks if the target died (Check is done within the Death() function)

                    if (TurnBasedStateMachine.OnNextTurnEvent != null)
                    {
                        TurnBasedStateMachine.OnNextTurnEvent();
                    }
                }
                else
                {
                    SaveCombatAction();
                    ManageTargeting.EnableTargets();
                }
            }
            else if (_targetingType == TargetingType.ALL)
            {
                CalculateDamage();
                if (TurnBasedStateMachine.ActivePlayers.Contains(_performer)) //If performer is a player character
                {
                    //Target all enemies
                    for (int i = 0; i < TurnBasedStateMachine.ActiveEnemies.Count; i++)
                    {
                        TurnBasedStateMachine.ActiveEnemies[i].CurrentHealth -= _damage;
                        ShowCharacterStats.onUpdateHealth();
                        ShowCharacterStats.onUpdateEnergy();
                        TurnBasedStateMachine.ActiveEnemies[i].Death();
                        OpenSkillsPanel.onSkillPanelToggle();
                    }
                }
                if (TurnBasedStateMachine.ActiveEnemies.Contains(_performer)) //If performer is a enemy
                {
                    //Target all players
                    for (int i = 0; i < TurnBasedStateMachine.ActivePlayers.Count; i++)
                    {
                        TurnBasedStateMachine.ActivePlayers[i].CurrentHealth -= _damage;
                        ShowCharacterStats.onUpdateHealth();
                        TurnBasedStateMachine.ActivePlayers[i].Death();
                    }
                }
                SaveCombatAction();

                if (TurnBasedStateMachine.OnNextTurnEvent != null)
                {
                    TurnBasedStateMachine.OnNextTurnEvent();
                }
            }
        }
    }

    void CalculateDamage()
    {
        if (_attackType == AttackType.MELEE)
        {
            _damage = _performer.Strength * _damageModifier;
        }
        else if (_attackType == AttackType.RANGED)
        {
            _damage = _performer.Accuracy * _damageModifier;
        }
    }

    public void SaveCombatAction()
    {
        if(CombatAction.OnTargetSelect != null)
        {
            CombatAction.OnTargetSelect = null;
            CombatAction.OnTargetSelect += ExecuteSkill;
        }
        else
        {
            CombatAction.OnTargetSelect += ExecuteSkill;
        }
    }
}
