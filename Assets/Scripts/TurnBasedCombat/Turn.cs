using UnityEngine;

public class Turn : MonoBehaviour {

    private static Character _currentCharactersTurn;

    public static void SetTarget()
    {
        _currentCharactersTurn = TurnBasedStateMachine.ActiveCharacter;

        if (TurnBasedStateMachine.IsPlayerTurn())
        {
            _currentCharactersTurn.Target = null;
        }
        else
        {
            _currentCharactersTurn.Target = TurnBasedStateMachine.ActivePlayers[Random.Range(0, TurnBasedStateMachine.ActivePlayers.Count)];
        }
    }
}
