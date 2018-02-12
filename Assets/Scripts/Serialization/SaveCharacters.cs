/*
	SaveCharacters.cs
	Created 11/10/2017 9:35:28 AM
	Project DriveBy RPG by BaseGames
*/
using Data;
using System.Collections.Generic;
using UnityEngine;

namespace Serialization
{
    public class SaveCharacters : MonoBehaviour
    {
        private static SaveCharacters s_Instance = null;

        public static SaveCharacters Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = FindObjectOfType(typeof(SaveCharacters)) as SaveCharacters;
                }


                if (s_Instance == null)
                {
                    GameObject obj = new GameObject("SaveCharacters");
                    s_Instance = obj.AddComponent(typeof(SaveCharacters)) as SaveCharacters;
                }

                return s_Instance;
            }

            set { }
        }

        void OnApplicationQuit()
        {
            s_Instance = null;
        }

        private void Awake()
        {
            if (s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
            }

            s_Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void SavePartyMembers()
        {
            List<PartyMember> partyMembers = new List<PartyMember>();

            foreach (PartyMember pm in GameObject.FindObjectsOfType(typeof(PartyMember)))
            {
                partyMembers.Add(pm);
            }

            List<Stats> statList = new List<Stats>();
            for (int i = 0; i < partyMembers.Count; i++)
            {
                Stats st = new Stats
                {
                    Name = partyMembers[i].Name,
                    Strength = partyMembers[i].Strength,
                    Accuracy = partyMembers[i].Accuracy,
                    Vitality = partyMembers[i].Vitality,
                    Stamina = partyMembers[i].Stamina,
                    Speed = partyMembers[i].Speed,

                    Level = partyMembers[i].Level,
                    CurrentXP = partyMembers[i].CurrentXP,
                    RequiredXP = partyMembers[i].RequiredXP,
                    StatPoints = partyMembers[i].StatPoints,
                    TotalUsedStatPoints = partyMembers[i].TotalUsedStatPoints,
                    SkillList = partyMembers[i].SkillList
                };
                statList.Add(st);
            }
            Serializer.Save("characterdata.dat", statList);
        }
        
        public void LoadMembers()
        {
            List<Stats> statList = Serializer.Load<List<Stats>>("characterdata.dat");

            List<PartyMember> partyMembers = new List<PartyMember>();

            foreach (PartyMember pm in GameObject.FindObjectsOfType(typeof(PartyMember)))
            {
                partyMembers.Add(pm);
            }

            if (statList != null)
            {
                for (int i = 0; i < statList.Count; i++)
                {
                    for (int j = 0; i < partyMembers.Count; j++)
                    {

                        if (partyMembers[j].Name == statList[i].Name)
                        {
                            partyMembers[j].Strength = statList[i].Strength;
                            partyMembers[j].Accuracy = statList[i].Accuracy;
                            partyMembers[j].Vitality = statList[i].Vitality;
                            partyMembers[j].Stamina = statList[i].Stamina;
                            partyMembers[j].Speed = statList[i].Speed;

                            partyMembers[j].Level = statList[i].Level;
                            partyMembers[j].CurrentXP = statList[i].CurrentXP;
                            partyMembers[j].RequiredXP = statList[i].RequiredXP;
                            partyMembers[j].StatPoints = statList[i].StatPoints;
                            partyMembers[j].TotalUsedStatPoints = statList[i].TotalUsedStatPoints;
                            partyMembers[j].SkillList = statList[i].SkillList;
                            break;
                        }
                    }
                }
            }
            else
                Debug.Log("No save file found.");
        }
    }
}