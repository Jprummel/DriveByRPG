using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnBasedStateMachine : MonoBehaviour
{
    public delegate void NextTurnEvent();
    public static NextTurnEvent OnNextTurnEvent;
    public static NextTurnEvent OnEnemyKill;

    public delegate void BattleCompleteEvent();
    public static BattleCompleteEvent OnBattleWon;
    public static BattleCompleteEvent OnBattleLost;

    //Turn indicator
    [SerializeField] private GameObject _turnIndicatorPrefab;
    private GameObject _turnIndicator;
    private Vector3 _turnIndicatorPos;

    //Lists keeping track of alive characters (and split up between players and enemies) and dead characters (player characters only)
    public static List<Character> ActiveCharacters = new List<Character>();
    public static List<Character> ActivePlayers = new List<Character>();
    public static List<Character> ActiveEnemies = new List<Character>();
    public static List<Character> DeadCharacters = new List<Character>();

    public static Skill PlayerUsedSkill;

    private int _characterTurnIndex = 0;
    public static Character ActiveCharacter;
    [SerializeField] private TurnOrder _turnOrder;
    [SerializeField] private BasicAttack _basicAttack;

    private void Awake()
    {
        OnBattleWon += BattleWon;
        OnNextTurnEvent += EndTurn;
    }

    void Start()
    {        
        GetAllBattleCharacters();
        SortTurnList();
        SortActiveCharacters();
        Turn.SetTarget();
        _turnIndicator = Instantiate(_turnIndicatorPrefab);
        MoveTurnIndicatorPos();
    }

    void GetAllBattleCharacters()
    {
        foreach (PartyMember partyMember in GameObject.FindObjectsOfType(typeof(PartyMember)))
        {
            ActiveCharacters.Add(partyMember);
        }
        foreach (Enemy enemy in GameObject.FindObjectsOfType(typeof(Enemy)))
        {
            ActiveCharacters.Add(enemy);
        }
    }

    void SortTurnList()
    {
        //Sorts the turn list based on every characters speed stat
        ActiveCharacters.Sort((character1, character2) => character2.Speed.CompareTo(character1.Speed));
        ActiveCharacter = ActiveCharacters[_characterTurnIndex];
    }

    public static bool IsPlayerTurn()
    {
        if(ActiveCharacter.gameObject.tag == Tags.PLAYER)
        { return true; }
        else{ return false; }
    }

    public void EndTurn()
    {
        OpenSkillsPanel.OnClearSkills();
        if (_characterTurnIndex < ActiveCharacters.Count-1)
        {
            _characterTurnIndex++;
        }
        else
        {
            _characterTurnIndex = 0;
        }
        ActiveCharacter = ActiveCharacters[_characterTurnIndex];
        MoveTurnIndicatorPos();
        if (OnEnemyKill != null)
        {
            OnEnemyKill = null;
        }
        else
        {
            _turnOrder.ReorderList();
        }
        Turn.SetTarget();
        OpenSkillsPanel.OnGetSkills();
        StartNewTurn();
    }

    void StartNewTurn()
    {
        if(ActiveCharacter.tag == "Character")
        {
            HideActions.HideButtons();
            StartCoroutine(EnemyAttack());
        }
        else
        {
            StartCoroutine(PlayerTurn());
        }
    }

    IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(2f);
        _basicAttack.ExecuteAction();
    }

    IEnumerator PlayerTurn()
    {
        yield return new WaitForSeconds(1f);
        HideActions.ShowButtons();
    }

    void MoveTurnIndicatorPos()
    {
        Vector3 newPos = ActiveCharacter.IndicatorPosition.position;
        _turnIndicatorPos = new Vector3(newPos.x, newPos.y + 1, newPos.z);
        _turnIndicator.transform.position = _turnIndicatorPos;
        TurnIndicatorHover.OnResetIndicator();
    }

    void SortActiveCharacters()
    {
        for (int i = 0; i < ActiveCharacters.Count; i++)
        {
            if (ActiveCharacters[i].gameObject.tag == Tags.PLAYER)
                ActivePlayers.Add(ActiveCharacters[i]);
            else
                ActiveEnemies.Add(ActiveCharacters[i]);
        }
    }

    void BattleWon()
    {
        ActiveCharacters.Clear();
        ActivePlayers.Clear();
        ActiveEnemies.Clear();
        DeadCharacters.Clear();
        SceneManager.LoadScene("PostCombatRewardsScene");
    }

    private void OnDisable()
    {
        OnNextTurnEvent -= EndTurn;
        OnBattleWon -= BattleWon;
    }
}
