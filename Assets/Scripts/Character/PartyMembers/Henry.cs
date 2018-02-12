[System.Serializable]
public class Henry : PartyMember {
    //Hothead
    //Packs a punch and tanky , fists first kind of character

    public Henry()
    {
        //Every party member starts with a stat total of 47 and 3 points to allocate at the start
        _name = "Henry";
        _level = 1;
        _statPoints = 3;
        _strength = 14;
        _accuracy = 6;
        _vitality = 11;
        _stamina = 10;
        _speed = 6;
        _requiredXP = 200;
    }
}
