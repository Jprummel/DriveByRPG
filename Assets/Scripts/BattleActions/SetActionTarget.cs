using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActionTarget : MonoBehaviour {

    public static void SetTarget(Character target)
    {
        //Character target = GetComponent<ShowCharacterStats>().Character;
        ManageTargeting.DisableTargets();

        TurnBasedStateMachine.ActiveCharacter.Target = target;
        if (CombatAction.OnTargetSelect != null)
        {
            CombatAction.OnTargetSelect();
            CombatAction.OnTargetSelect = null;
            if(OpenSkillsPanel.SkillPanelEnabled == true)
                OpenSkillsPanel.onSkillPanelToggle();
        }
    }
}