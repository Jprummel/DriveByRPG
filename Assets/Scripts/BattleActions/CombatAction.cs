using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CombatAction : MonoBehaviour{

    public delegate void ResumeAction();
    public static ResumeAction OnTargetSelect;

    protected Character _performer;

    protected float _damageModifier = 5;
    protected float _energyCost;
    protected float _damage;

    protected AttackType _attackType;

    public enum AttackType
    {
        MELEE,
        RANGED
    }

    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public virtual void ExecuteAction()
    {
        
    }

    public virtual void SaveCombatAction()
    {

    }
}
