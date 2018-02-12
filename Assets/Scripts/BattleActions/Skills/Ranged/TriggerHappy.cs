[System.Serializable]
public class TriggerHappy : Skill {

	public TriggerHappy()
    {
        _abilityName = "Trigger Happy";
        _attackType = AttackType.RANGED;
        _targetingType = TargetingType.ALL;
        _damageModifier = 10;
        _energyCost = 15;
    }
}
