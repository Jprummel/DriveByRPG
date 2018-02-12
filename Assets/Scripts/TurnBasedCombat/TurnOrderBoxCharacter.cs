using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOrderBoxCharacter : MonoBehaviour {

    [SerializeField]private Character _linkedCharacter;
    public Character LinkedCharacter
    {
        get { return _linkedCharacter; }
        set { _linkedCharacter = value; }
    }
}
