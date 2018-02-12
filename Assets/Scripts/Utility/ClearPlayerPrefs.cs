/*
	ClearPlayerPrefs.cs
	Created 11/13/2017 2:57:30 PM
	Project DriveBy RPG by Base Games
*/
using UnityEngine;

namespace Utility
{
	public class ClearPlayerPrefs : MonoBehaviour 
	{
		public void Clear()
        {
            PlayerPrefs.DeleteAll();
        }
	}
}