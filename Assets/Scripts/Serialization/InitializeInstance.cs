/*
	InitializeInstance.cs
	Created 11/13/2017 10:56:43 AM
	Project DriveBy RPG by Base Games
*/
using UnityEngine;

namespace Serialization
{
	public class InitializeInstance : MonoBehaviour 
	{
        private void Awake()
        {
            SaveCharacters.Instance.LoadMembers();
        }
    }
}