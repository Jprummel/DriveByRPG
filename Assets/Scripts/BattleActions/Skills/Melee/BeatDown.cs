[System.Serializable]
public class BeatDown : Skill {

	public BeatDown()
    {
        _abilityName = "Beat Down";
        _attackType = AttackType.MELEE;
        _targetingType = TargetingType.SINGLE;
        _damageModifier = 23;
        _energyCost = 15;
    }
}
