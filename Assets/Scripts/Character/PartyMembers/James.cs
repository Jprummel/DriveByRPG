[System.Serializable]
public class James : PartyMember {
    //Serious guy
    //Assassin like stats (focus on ranged damage and speed)

    public James()
    {
        //Every party member starts with a stat total of 47 and 3 points to allocate at the start
        _name = "James";
        _level = 1;
        _statPoints = 3;
        _strength = 7;
        _accuracy = 13;
        _vitality = 8;
        _stamina = 10;
        _speed = 9;
        _requiredXP = 200;
    }
}
