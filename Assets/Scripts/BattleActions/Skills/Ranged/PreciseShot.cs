[System.Serializable]
public class PreciseShot : Skill {

	public PreciseShot()
    {
        _abilityName = "Precise Shot";
        _attackType = AttackType.RANGED;
        _targetingType = TargetingType.SINGLE;
        _damageModifier = 25;
        _energyCost = 30;
    }
}
