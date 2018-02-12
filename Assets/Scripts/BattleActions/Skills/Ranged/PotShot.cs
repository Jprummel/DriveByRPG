[System.Serializable]
public class PotShot : Skill {

	public PotShot()
    {
        _abilityName = "Pot Shot";
        _attackType = AttackType.RANGED;
        _targetingType = TargetingType.SINGLE;
        _damageModifier = 14;
        _energyCost = 20;
    }
}
