/*
	GameStateManager.cs
	Created 11/10/2017 1:33:58 PM
	Project DriveBy RPG by Base Games
*/

namespace Utility
{
    public enum GamesStates { Playing, Paused }

	public class GameStateManager 
	{
        public static GamesStates GameState = GamesStates.Playing;
	}
}