[System.Serializable]
public class Feint : Skill {

	public Feint()
    {
        _abilityName = "Feint";
        _attackType = AttackType.MELEE;
        _targetingType = TargetingType.SINGLE;
        _damageModifier = 9;
        _energyCost = 10;
    }
}
