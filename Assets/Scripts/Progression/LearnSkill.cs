using UnityEngine;

public class LearnSkill : MonoBehaviour {

    public delegate void LearnSkillEvent(Character character, Skill skillToAdd);
    public static LearnSkillEvent onLearnSkill;

    private void Awake()
    {
        onLearnSkill += AddSkill;
    }

    void AddSkill(Character character, Skill skillToAdd)
    {
        character.SkillList.Add(skillToAdd);
    }

    private void OnDisable()
    {
        onLearnSkill -= AddSkill;
    }
}
