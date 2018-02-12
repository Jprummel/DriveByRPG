using UnityEngine;
using DG.Tweening;
public class Enemy : Character {

    public delegate void EnemyDeathEvent();
    public static EnemyDeathEvent OnEnemyDeath;

    protected float _xpToGive;
    protected float _currencyToGive;

    public float XPToGive
    {
        get { return _xpToGive; }
        set { _xpToGive = value; }
    }

    public float CurrencyToGive
    {
        get { return _currencyToGive; }
        set { _currencyToGive = value; }
    }

    private void Awake()
    {
        OnEnemyDeath += Death;
    }

    public override void Death()
    {
        if(_currentHealth <= 0)
        {
            CombatRewards.XPEarned += _xpToGive;             //Adds this enemies xp value to the XP earned pool 
            CombatRewards.CurrencyEarned += _currencyToGive; //Adds this enemies currency value to the currency earned pool
            TurnOrder.OnDeath(this);
            TurnBasedStateMachine.ActiveCharacters.Remove(this);
            TurnBasedStateMachine.ActiveEnemies.Remove(this);
            TurnBasedStateMachine.OnEnemyKill += Death;
            Sequence deathSequence = DOTween.Sequence();
            deathSequence.Append(transform.DOScale(0, 0.75f));
            deathSequence.Join(transform.DORotate(new Vector3(0, 0, 180), 0.75f));
            deathSequence.onComplete += DestroyCharacter;
        }
        if(TurnBasedStateMachine.ActiveEnemies.Count <= 0)
        {
            //Battle Won
            TurnBasedStateMachine.OnBattleWon();
        }
    }

    void DestroyCharacter()
    {
        Destroy(this.gameObject);
    }

    public virtual void ChooseAction(PartyMember target)
    {
        
    }

    private void OnDisable()
    {
        OnEnemyDeath -= Death;
    }
}
