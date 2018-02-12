[System.Serializable]
public class PartyMember : Character {

    protected int _level;
    protected float _currentXP;
    protected float _requiredXP;
    protected float _xpTillNextLevel;
    protected int _statPoints;
    protected int _usedStatPoints;
    protected int _totalUsedStatPoints;

    public int Level
    {
        get { return _level; }
        set { _level = value; }
    }

    public float CurrentXP
    {
        get { return _currentXP; }
        set { _currentXP = value; }
    }

    public float RequiredXP
    {
        get { return _requiredXP; }
        set { _requiredXP = value; }
    }

    public float XPTillNextLevel
    {
        get { return _xpTillNextLevel; }
        set { _xpTillNextLevel = value; }
    }

    public int StatPoints
    {
        get { return _statPoints; }
        set { _statPoints = value; }
    }

    public int UsedStatPoints
    {
        get { return _usedStatPoints; }
        set { _usedStatPoints = value; }
    }

    public int TotalUsedStatPoints
    {
        get { return _totalUsedStatPoints; }
        set { _totalUsedStatPoints = value; }
    }

    public override void Death()
    {
        if (_currentHealth <= 0)
        {
            TurnBasedStateMachine.ActiveCharacters.Remove(this);
            TurnBasedStateMachine.ActivePlayers.Remove(this);
            TurnBasedStateMachine.DeadCharacters.Add(this);
            //Death anim
        }

        if(TurnBasedStateMachine.ActivePlayers.Count <= 0)
        {
            //Game over
        }
    }

    public void CalculateNeededXP()
    {
        _xpTillNextLevel = _requiredXP - _currentXP;
    }
}