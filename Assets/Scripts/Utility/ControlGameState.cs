/*
	ControlGameState.cs
	Created 11/10/2017 1:36:53 PM
	Project DriveBy RPG by Base Games
*/
using UnityEngine;

namespace Utility
{
	public class ControlGameState : MonoBehaviour 
	{
        private void OnEnable()
        {
            GameStateManager.GameState = GamesStates.Paused;
        }

        private void OnDisable()
        {
            GameStateManager.GameState = GamesStates.Playing;
        }
    }
}