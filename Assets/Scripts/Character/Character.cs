using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Character : MonoBehaviour {

    protected string _name;
    protected float _maxHealth;
    protected float _maxEnergy;
    protected float _strength; //Melee damage modifier
    protected float _accuracy; //Ranged damage modifier
    protected float _vitality; //Health modifier
    protected float _stamina; //Energy modifier
    protected float _speed; //Determines turn order

    protected List<Skill> _skillList = new List<Skill>();
    protected float _currentHealth;
    protected float _currentEnergy;

    private Character _target;
    [SerializeField] protected Transform _indicatorPosition;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public float MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    public float CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    public float MaxEnergy
    {
        get { return _maxEnergy; }
        set { _maxEnergy = value; }
    }

    public float CurrentEnergy
    {
        get { return _currentEnergy; }
        set { _currentEnergy = value; }
    }

    public float Strength
    {
        get { return _strength; }
        set { _strength = value; }
    }

    public float Accuracy
    {
        get { return _accuracy; }
        set { _accuracy = value; }
    }

    public float Vitality
    {
        get { return _vitality; }
        set { _vitality = value; }
    }

    public float Stamina
    {
        get { return _stamina; }
        set { _stamina = value; }
    }

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public List<Skill> SkillList
    {
        get { return _skillList; }
        set { _skillList = value; }
    }

    public Character Target
    {
        get { return _target; }
        set { _target = value; }
    }

    public Transform IndicatorPosition
    {
        get { return _indicatorPosition; }
        set { _indicatorPosition = value; }
    }

    private void Start()
    { 
        CalculateVitals();

        _currentHealth = _maxHealth;
        _currentEnergy = _maxEnergy;
    }

    void CalculateVitals()
    {
        _maxHealth = _vitality * 40;
        _maxEnergy = _stamina * 5;
    }

    public virtual void Death()
    {
        
    }
}