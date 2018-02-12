using UnityEngine;
using DG.Tweening;

public class BasicAttack : CombatAction
{
    public override void ExecuteAction()
    {
        ExecuteBasicAttack();
    }

    public void ExecuteBasicAttack()
    {
        _attackType = AttackType.MELEE;
        _performer = TurnBasedStateMachine.ActiveCharacter;

        if (OpenSkillsPanel.SkillPanelEnabled)
        {
            OpenSkillsPanel.onSkillPanelToggle();
        }


        if (_performer.Target != null)
        {
            if (_attackType == AttackType.MELEE)
            {
                _damage = _performer.Strength * _damageModifier;
            }
            else if (_attackType == AttackType.RANGED)
            {

                _damage = _performer.Accuracy * _damageModifier;
            }

            _performer.Target.CurrentHealth -= _damage;
            _performer.CurrentEnergy -= _energyCost;
            ShowCharacterStats.onUpdateHealth();
            ShowCharacterStats.onUpdateEnergy();
            _performer.Target.Death();

            Sequence attackSequence = DOTween.Sequence();
            Vector2 startPos = _performer.transform.position;
            Vector2 battlePos = new Vector2(0f, _performer.transform.position.y);
            attackSequence.Append(_performer.transform.DOMove(battlePos, 0.5f));
            attackSequence.Append(_performer.transform.DOShakeScale(0.5f));
            attackSequence.Append(_performer.transform.DOMove(startPos, 0.25f));
            attackSequence.SetLoops(1);

            ShowCharacterStats.onUpdateHealth();

            if (TurnBasedStateMachine.OnNextTurnEvent != null)
            {
                TurnBasedStateMachine.OnNextTurnEvent();
            }
        }
        else
        {
            SaveCombatAction();
            Debug.Log("Please select a target");
            if (ManageTargeting.IsEnabled == false)
                ManageTargeting.EnableTargets();
            else
                ManageTargeting.DisableTargets();
        }
    }
    
    public override void SaveCombatAction()
    {
        if (OnTargetSelect != null)
        {
            OnTargetSelect = null;
            OnTargetSelect += ExecuteBasicAttack;
        }
        else
        {
            OnTargetSelect += ExecuteBasicAttack;
        }
    }
}
