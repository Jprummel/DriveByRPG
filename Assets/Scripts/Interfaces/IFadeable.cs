/*
	IFadeable.cs
	Created 11/10/2017 9:08:03 AM
	Project DriveBy RPG by DefaultCompany
*/
using UnityEngine.EventSystems;

namespace Interfaces
{
	public interface IFadeable : IEventSystemHandler 
	{
        void FadeAlpha();

        void ResetAlpha();
	}
}