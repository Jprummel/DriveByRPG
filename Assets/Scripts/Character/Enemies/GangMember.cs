using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangMember : Enemy {

	public GangMember()
    {
        _name = CharacterNames.GANGMEMBER;
        _strength = 5;
        _accuracy = 5;
        _vitality = 7;
        _stamina = 3;
        _speed = 4;

        _xpToGive = 50;
    }

    public override void ChooseAction(PartyMember target)
    {
        
    }
}
