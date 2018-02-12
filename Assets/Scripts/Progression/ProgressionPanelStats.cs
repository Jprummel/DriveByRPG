using Serialization;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ProgressionPanelStats : MonoBehaviour {

    public delegate void ShowCharacterStatsEvent(PartyMember character);
    public delegate void ShowStatChangeEvent(int statIndex,bool isIncrease);
    public static ShowCharacterStatsEvent onShowStats;
    public static ShowStatChangeEvent onStatChange;
    //Text fade colors
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _increaseColor;
    [SerializeField] private Color _decreaseColor;

    private PartyContainer _party;
    //All text fields
    [SerializeField] private Text _name;
    [SerializeField] private Text _level;
    [SerializeField] private Text _strength;
    [SerializeField] private Text _accuracy;
    [SerializeField] private Text _vitality;
    [SerializeField] private Text _stamina;
    [SerializeField] private Text _speed;
    [SerializeField] private Text _xpValues;

    private void OnEnable()
    {
        onShowStats += ShowCharacterInfo;
        onStatChange += HighlightText;
    }

    void Start () {
        _party = GameObject.FindGameObjectWithTag(Tags.PARTYCONTAINER).GetComponent<PartyContainer>();
        SaveCharacters.Instance.LoadMembers();
        onShowStats(_party.PartyMembers[_party.CurrentMember]);
	}

    void ShowCharacterInfo(PartyMember character)
    {
        _name.text = character.Name;
        _level.text = "Lv. " + character.Level;
        _strength.text = character.Strength.ToString();
        _accuracy.text = character.Accuracy.ToString();
        _vitality.text = character.Vitality.ToString();
        _stamina.text = character.Stamina.ToString();
        _speed.text = character.Speed.ToString();
        _xpValues.text = "XP : " + character.CurrentXP + " / " + character.RequiredXP; 
    }

    private void OnDisable()
    {
        onShowStats -= ShowCharacterInfo;
        onStatChange -= HighlightText;
    }

    void HighlightText(int statIndex,bool isIncrease)
    {
        switch (statIndex)
        {
            case 0:
                StartCoroutine(HighlightTextRoutine(_strength,isIncrease));
                break;
            case 1:
                StartCoroutine(HighlightTextRoutine(_accuracy,isIncrease));
                break;
            case 2:
                StartCoroutine(HighlightTextRoutine(_vitality,isIncrease));
                break;
            case 3:
                StartCoroutine(HighlightTextRoutine(_stamina,isIncrease));
                break;
            case 4:
                StartCoroutine(HighlightTextRoutine(_speed,isIncrease));
                break;                
        }
    }

    IEnumerator HighlightTextRoutine(Text textToHighlight,bool isIncrease)
    {
        Color colorToFade;
        if (isIncrease) //if isIncrease, text color is green, else its red
        {
            colorToFade = _increaseColor;
        }
        else
        {
            colorToFade = _decreaseColor;
        }

        Tween colorTween = textToHighlight.DOColor(colorToFade, 0.5f).SetEase(Ease.Flash);
        yield return colorTween.WaitForCompletion();
        textToHighlight.DOColor(_defaultColor, 0.5f).SetEase(Ease.Flash);
    }
}