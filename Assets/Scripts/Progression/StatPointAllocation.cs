using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Serialization;
using DG.Tweening;

public class StatPointAllocation : MonoBehaviour {

    public delegate void CheckStatsEvent(PartyMember character);
    public static CheckStatsEvent onCheckBaseStats;
    public static CheckStatsEvent onCheckManipulatedStats;

    private PartyContainer _party;
    [SerializeField] private List<GameObject> _addStatButtons = new List<GameObject>();
    [SerializeField] private List<GameObject> _removeStatButtons = new List<GameObject>();
    [SerializeField] private GameObject _confirmButton;
    [SerializeField] private GameObject _resetButton;
    [SerializeField] private Text _availablePointsText;
    private float[] _pointsToAllocate = new float[5]; //Points to put in stats chosen by the player
    private float[] _baseStatPoints = new float[5]; //Starting stat values before applying allocated points

    private void OnEnable()
    { 
        //Assign functions to delegates
        onCheckBaseStats += RetrieveStatBaseStatPoints;
        onCheckManipulatedStats += RetrievePointsToAllocate;
        //Use delegates to check base stats and see if anything has been manipulated on startup
        if(_party != null)
        {
            onCheckBaseStats(_party.PartyMembers[_party.CurrentMember]);
            onCheckManipulatedStats(_party.PartyMembers[_party.CurrentMember]);
        }
        
    }

    void Start()
    {             
        //Get the partycontainer script
        _party = GameObject.FindGameObjectWithTag(Tags.PARTYCONTAINER).GetComponent<PartyContainer>();
        onCheckBaseStats(_party.PartyMembers[_party.CurrentMember]);
        onCheckManipulatedStats(_party.PartyMembers[_party.CurrentMember]);
    }

    void Update()
    {
        ShowAvailablePoints();
        ShowButtons();
    }

    void ShowAvailablePoints()
    {
        //Shows the available stat points for the current character
        if (_party.PartyMembers[_party.CurrentMember].StatPoints > 0)
        {
            _availablePointsText.text = "Available points : " + _party.PartyMembers[_party.CurrentMember].StatPoints;
        }
        else
        {
            _availablePointsText.text = "";
        }
    }

    void ShowButtons()
    {
        //Shows the Add Point buttons if there are points left to allocate and disables them when there arent any points left
        for (int i = 0; i < _pointsToAllocate.Length; i++)
        {
            if (_pointsToAllocate[i] >= _baseStatPoints[i] && _party.PartyMembers[_party.CurrentMember].StatPoints > 0)
            {
                foreach (GameObject button in _addStatButtons)
                {
                    button.SetActive(true);
                }
            }
            else
            {
                foreach (GameObject button in _addStatButtons)
                {
                    button.SetActive(false);
                }
            }

            //Shows the remove point button if there are stat points put in a certain stat
            if (_pointsToAllocate[i] > _baseStatPoints[i])
            {
                _removeStatButtons[i].SetActive(true);
            }
            else
            {
                _removeStatButtons[i].SetActive(false);
            } 
        }

        if(_party.PartyMembers[_party.CurrentMember].UsedStatPoints > 0)
        {
            //If the player has used any of his available stat points, activate confirm and reset button
            _confirmButton.SetActive(true);
            _resetButton.SetActive(true);
        }
        else
        {
            //If he hasnt (or has confirmed his stats), deactivate confirm and reset button
            _confirmButton.SetActive(false);
            _resetButton.SetActive(false);
        }
    }

    public void AddStatPoint(int statIndex)
    {
        //Set this function to a buttons onclick to add a point in a stat
        switch (statIndex)
        {
            case 0:
                _party.PartyMembers[_party.CurrentMember].Strength++;
                break;
            case 1:
                _party.PartyMembers[_party.CurrentMember].Accuracy++;
                break;
            case 2:
                _party.PartyMembers[_party.CurrentMember].Vitality++;
                break;
            case 3:
                _party.PartyMembers[_party.CurrentMember].Stamina++;
                break;
            case 4:
                _party.PartyMembers[_party.CurrentMember].Speed++;
                break;
        }
        _party.PartyMembers[_party.CurrentMember].StatPoints--; //Removes a stat point to use
        _party.PartyMembers[_party.CurrentMember].UsedStatPoints++; //Increases the used stat points by 1 (Used if player resets his current decision)
        _party.PartyMembers[_party.CurrentMember].TotalUsedStatPoints++; //Increases total used stat points by 1 (used if player wants to re-spec from the start)
        onCheckManipulatedStats(_party.PartyMembers[_party.CurrentMember]);
        ProgressionPanelStats.onStatChange(statIndex, true);
        ProgressionPanelStats.onShowStats(_party.PartyMembers[_party.CurrentMember]); //Updates stats on screen
    }

    public void RemoveStatPoint(int statIndex)
    {
        //Set this function to a buttons onclick to remove a point in a stat
        switch (statIndex)
        {
            case 0:
                _party.PartyMembers[_party.CurrentMember].Strength--;
                break;
            case 1:
                _party.PartyMembers[_party.CurrentMember].Accuracy--;
                break;
            case 2:
                _party.PartyMembers[_party.CurrentMember].Vitality--;
                break;
            case 3:
                _party.PartyMembers[_party.CurrentMember].Stamina--;
                break;
            case 4:
                _party.PartyMembers[_party.CurrentMember].Speed--;
                break;
        }
        _party.PartyMembers[_party.CurrentMember].StatPoints++; //Returns a stat point to use
        _party.PartyMembers[_party.CurrentMember].UsedStatPoints--; //Reduces the used stat points used by 1 (used if player resets his decision)
        _party.PartyMembers[_party.CurrentMember].TotalUsedStatPoints--; //Reduces total used stat points used by 1 (used if player wants to re-spec from the start)
        onCheckManipulatedStats(_party.PartyMembers[_party.CurrentMember]);
        ProgressionPanelStats.onStatChange(statIndex, false);
        ProgressionPanelStats.onShowStats(_party.PartyMembers[_party.CurrentMember]); //Updates stats on screen
    }

    public void ConfirmStats()
    {
        _party.PartyMembers[_party.CurrentMember].UsedStatPoints = 0; //Sets used points to 0 if player confirms
        onCheckBaseStats(_party.PartyMembers[_party.CurrentMember]); //Sets the new base stats so only the proper buttons show
        SaveCharacters.Instance.SavePartyMembers();
        
    }

    public void ResetToBaseStats()
    {
        //Resets the stats to what they were before allocating points
        _party.PartyMembers[_party.CurrentMember].Strength = _baseStatPoints[0];
        _party.PartyMembers[_party.CurrentMember].Accuracy = _baseStatPoints[1];
        _party.PartyMembers[_party.CurrentMember].Vitality = _baseStatPoints[2];
        _party.PartyMembers[_party.CurrentMember].Stamina = _baseStatPoints[3];
        _party.PartyMembers[_party.CurrentMember].Speed = _baseStatPoints[4];
        _party.PartyMembers[_party.CurrentMember].StatPoints += _party.PartyMembers[_party.CurrentMember].UsedStatPoints; //Returns the players used points
        _party.PartyMembers[_party.CurrentMember].UsedStatPoints = 0; // Resets used points to 0
        onCheckManipulatedStats(_party.PartyMembers[_party.CurrentMember]); 
        ProgressionPanelStats.onShowStats(_party.PartyMembers[_party.CurrentMember]);
    }

    void RetrieveStatBaseStatPoints(PartyMember character)
    {
        //This is used at the start of point allocation to check if there are any changes to any stat
        _baseStatPoints[0] = character.Strength;
        _baseStatPoints[1] = character.Accuracy;
        _baseStatPoints[2] = character.Vitality;
        _baseStatPoints[3] = character.Stamina;
        _baseStatPoints[4] = character.Speed;
        _party.PartyMembers[_party.CurrentMember].UsedStatPoints = 0; //Makes sure the the player cant return points after switching characters
    }

    void RetrievePointsToAllocate(PartyMember character)
    {
        //This is used to check if there is any change to the stat , compares to the base stats
        _pointsToAllocate[0] = character.Strength;
        _pointsToAllocate[1] = character.Accuracy;
        _pointsToAllocate[2] = character.Vitality;
        _pointsToAllocate[3] = character.Stamina;
        _pointsToAllocate[4] = character.Speed;        
    }

    private void OnDisable()
    {
        onCheckBaseStats -= RetrieveStatBaseStatPoints;
        onCheckManipulatedStats -= RetrievePointsToAllocate;
    }
}