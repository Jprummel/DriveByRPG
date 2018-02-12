/*
	SaveButton.cs
	Created 11/13/2017 11:05:55 AM
	Project DriveBy RPG by Base Games
*/
using UnityEngine;

namespace Serialization
{
	public class SaveButton : MonoBehaviour 
	{
		public void Save()
        {
            SaveCharacters.Instance.SavePartyMembers();
        }
	}
}