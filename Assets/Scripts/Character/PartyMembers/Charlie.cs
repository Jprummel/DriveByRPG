[System.Serializable]
public class Charlie : PartyMember {
    //Joker
    //Quickness is the main key , trickster/playful abilities and combat style

    public Charlie()
    {
        //Every party member starts with a stat total of 47 and 3 points to allocate at the start
        _name = "Charlie";
        _level = 1;
        _statPoints = 3;
        _strength = 8;
        _accuracy = 9;
        _vitality = 9;
        _stamina = 12;
        _speed = 11;
        _requiredXP = 200;
    }
}
