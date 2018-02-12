using UnityEngine;
using UnityEngine.SceneManagement;
using Player;

public class RandomBattleEncounter : MonoBehaviour {

    public delegate void EncounterEvent();
    public static EncounterEvent OnEncounter;

    [SerializeField] private bool _isCombatArea;
    [SerializeField] private float _encounterChanceThreshold;

    private bool _canCheckForEncounter;
    private float _encounterCheckTimer;

	void Awake () {
        OnEncounter += CheckForEncounter;
        _canCheckForEncounter = true;
	}

    private void Update()
    {
        if(_canCheckForEncounter == false && _encounterCheckTimer > 0)
        {
            _encounterCheckTimer -= Time.deltaTime;
        }
        if(_encounterCheckTimer <= 0)
        {
            _canCheckForEncounter = true;
        }
    }

    void CheckForEncounter()
    {
        if (_isCombatArea && _canCheckForEncounter)
        {
            int encounterChance = Random.Range(0, 100);
            if(encounterChance < _encounterChanceThreshold)
            {
                string currentSceneName = SceneManager.GetActiveScene().name;
                PlayerLocation.SceneName = currentSceneName;
                PlayerLocation.PlayerPosition = PlayerMovement.PlayerPosition;
                SceneManager.LoadScene(currentSceneName + "BattleScene");
            }
            _canCheckForEncounter = false;
            _encounterCheckTimer = 0.5f;
        }
    }
}