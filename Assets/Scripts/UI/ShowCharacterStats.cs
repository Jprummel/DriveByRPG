using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShowCharacterStats : MonoBehaviour {

    public delegate void UpdateStatUIEvent();
    public static UpdateStatUIEvent onUpdateHealth;
    public static UpdateStatUIEvent onUpdateEnergy;

    [SerializeField]private Character _character;
    public Character Character
    {
        get { return _character; }
        set { _character = value; }
    }

    [SerializeField] private Text _characterName;
    [SerializeField] private Image _healthBar;
    [SerializeField] private Text _healthValue;

    [SerializeField] private Text _energyValue;

    private void OnEnable()
    {
        onUpdateHealth += UpdateHealthUI;
        onUpdateEnergy += UpdateEnergyUI;
    }

    void Start () {
        ShowCharacterName();
        onUpdateEnergy();
        onUpdateHealth();
	}

    void ShowCharacterName()
    {
        _characterName.text = _character.Name;
    }

    void UpdateHealthUI()
    {
        _healthBar.DOFillAmount(_character.CurrentHealth / _character.MaxHealth,1);
        _healthValue.text = _character.CurrentHealth + " / " + _character.MaxHealth;
    }

    void UpdateEnergyUI()
    {
        if(_energyValue != null)
            _energyValue.text = _character.CurrentEnergy + " / " + _character.MaxEnergy;
    }

    private void OnDisable()
    {
        onUpdateHealth -= UpdateHealthUI;
        onUpdateEnergy -= UpdateEnergyUI;
    }
}
