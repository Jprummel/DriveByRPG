[System.Serializable]
public class Isaac : PartyMember {
    //Main character (Detective)
    //All round character, player can go all routes with this character
    public Isaac()
    {
        //Every party member starts with a stat total of 47 and 3 points to allocate at the start
        _name = "Isaac";
        _level = 1;
        _statPoints = 3;
        _strength = 10;
        _accuracy = 9;
        _vitality = 10;
        _stamina = 9;
        _speed = 9;
        _requiredXP = 200;
    }
}
