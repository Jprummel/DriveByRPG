using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SpawnStatPanel : MonoBehaviour
{

    [SerializeField] private Transform _partyPanelContainer;
    [SerializeField] private Transform _enemyPanelContainer;
    [SerializeField] private GameObject _statPanel;
    private List<Character> _enemies = new List<Character>();
    private List<Character> _partyMembers = new List<Character>();

    [SerializeField] private List<GameObject> _partyMembersUI = new List<GameObject>();
    [SerializeField] private List<GameObject> _enemiesUI = new List<GameObject>();

    void Start()
    {
        foreach (Character partyMember in GameObject.FindObjectsOfType(typeof(PartyMember)))
        {
            _partyMembers.Add(partyMember);
        }

        foreach (Character enemy in GameObject.FindObjectsOfType(typeof(Enemy)))
        {
            _enemies.Add(enemy);
        }

        SpawnPartyPanels();
        SpawnEnemyPanels();
        StartCoroutine(TweenPanelsAlternatively());
    }

    void SpawnEnemyPanels()
    {
        for (int j = 0; j < _enemies.Count; j++)
        {
            ShowCharacterStats characterPanelScript = _enemiesUI[j].GetComponent<ShowCharacterStats>();
            Button button = characterPanelScript.gameObject.GetComponentInChildren<Button>();
            characterPanelScript.Character = _enemies[j];
            characterPanelScript.Character.gameObject.GetComponent<EnemyHover>().CorrespondingButton = button;

        }
    }

    void SpawnPartyPanels()
    {
        for (int j = 0; j < _partyMembers.Count; j++)
        {
            ShowCharacterStats characterPanelScript = _partyMembersUI[j].GetComponent<ShowCharacterStats>();
            characterPanelScript.Character = _partyMembers[j];
        }        
    }

    IEnumerator TweenPanelsAlternatively()
    {
        int x = 0;

        for (int i = 0; i < TurnBasedStateMachine.ActivePlayers.Count; i++)
        {
            yield return new WaitForSeconds(0.2f);
            _partyMembersUI[i].SetActive(true);
            _partyMembersUI[i].GetComponent<RectTransform>().DOAnchorPosX(0, 0.4f).SetEase(Ease.InExpo);
                                  
            for (int j = x; j < TurnBasedStateMachine.ActiveEnemies.Count;)
            {
                yield return new WaitForSeconds(0.2f);
                _enemiesUI[j].SetActive(true);
                _enemiesUI[j].GetComponent<RectTransform>().DOAnchorPosX(0, 0.4f).SetEase(Ease.InExpo);
                x++;
                break;
            }
        }
    }
}
