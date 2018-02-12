using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyContainer : MonoBehaviour {

    [SerializeField] private List<PartyMember> _partyMembers = new List<PartyMember>();
    private int _currentMember = 0;

    public List<PartyMember> PartyMembers
    {
        get { return _partyMembers; }
        set { _partyMembers = value; }
    }
    public int CurrentMember
    {
        get { return _currentMember; }
        set { _currentMember = value; }
    }

    public void SelectNextPartyMember()
    {
        if(_currentMember < _partyMembers.Count-1)
        {
            _currentMember++;
        }
        else
        {
            _currentMember = 0;
        }
        ProgressionPanelStats.onShowStats(_partyMembers[_currentMember]);
        StatPointAllocation.onCheckBaseStats(_partyMembers[_currentMember]);
        StatPointAllocation.onCheckManipulatedStats(_partyMembers[_currentMember]);
    }

    public void SelectPreviousPartyMember()
    {
        if(_currentMember > 0)
        {
            _currentMember--;
        }
        else
        {
            _currentMember = _partyMembers.Count-1;
        }
        ProgressionPanelStats.onShowStats(_partyMembers[_currentMember]);
        StatPointAllocation.onCheckBaseStats(_partyMembers[_currentMember]);
        StatPointAllocation.onCheckManipulatedStats(_partyMembers[_currentMember]);
    }
}
