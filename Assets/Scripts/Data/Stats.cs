/*
	Stats.cs
	Created 11/10/2017 10:21:46 AM
	Project DriveBy RPG by Base Games
*/
using System.Collections.Generic;
namespace Data
{
    [System.Serializable]
	public class Stats 
	{
        public string Name;
        public float Strength; //Melee damage modifier
        public float Accuracy; //Ranged damage modifier
        public float Vitality; //Health modifier
        public float Stamina; //Energy modifier
        public float Speed;

        public int Level;
        public float CurrentXP;
        public float RequiredXP;
        public int StatPoints;
        public int TotalUsedStatPoints;

        public List<Skill> SkillList = new List<Skill>();
    }
}