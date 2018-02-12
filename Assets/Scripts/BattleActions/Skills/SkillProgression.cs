using UnityEngine;
using Serialization;

public class SkillProgression : MonoBehaviour {

    public delegate void SkillProgressionEvent(PartyMember partyMember);
    public static SkillProgressionEvent OnSkillProgression;

    void Awake () {
        OnSkillProgression += CharacterSkillProgression;
	}

    void CharacterSkillProgression(PartyMember partyMember)
    {
        //Gives player characters skills when they level up (to a certain level)
        switch (partyMember.Name)
        {
            //Every characters second skill is a multi target skill
            case CharacterNames.ISAAC: //All round character
                switch (partyMember.Level)
                {
                    case 3:
                        LearnSkill.onLearnSkill(partyMember, new Feint());
                        break;
                    case 8:
                        LearnSkill.onLearnSkill(partyMember, new TriggerHappy());
                        break;
                    case 14:
                        LearnSkill.onLearnSkill(partyMember, new BeatDown());
                        break;
                }
                break;
            case CharacterNames.CHARLIE: //Speed based skills
                switch (partyMember.Level)
                {
                    case 4:
                        LearnSkill.onLearnSkill(partyMember, new Feint());
                        break;
                    case 9:
                        break;
                    case 15:
                        break;
                }
                break;
            case CharacterNames.JAMES: //Ranged Skills / Assassin Skills
                switch (partyMember.Level)
                {
                    case 4:
                        break;
                    case 9:
                        LearnSkill.onLearnSkill(partyMember, new TriggerHappy());
                        break;
                    case 15:
                        LearnSkill.onLearnSkill(partyMember, new PreciseShot());
                        break;
                }
                break;
            case CharacterNames.HENRY: //Melee skills
                switch (partyMember.Level)
                {
                    case 4:

                        break;
                    case 9:
                        break;
                    case 15:
                        break;
                }
                break;
        }
        SaveCharacters.Instance.SavePartyMembers();
    }

    private void OnDisable()
    {
        OnSkillProgression -= CharacterSkillProgression;
    }
}