using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OpenSkillsPanel : MonoBehaviour {

    public delegate void SkillPanelToggleEvent();
    public static SkillPanelToggleEvent onSkillPanelToggle;

    public delegate void SkillListEvent();
    public static SkillListEvent OnGetSkills;
    public static SkillListEvent OnClearSkills;

    public static bool SkillPanelEnabled;
    
    [SerializeField] private Transform _skillPanel;
    [SerializeField] private Button _skillButton;
    private List<int> _skillIndex = new List<int>();

    private Tween _openPanelTween;
    private Tween _closePanelTween;

    private RectTransform _rt;

    private float _startPos;
    private float _endPos;

    private List<Button> _skillButtons = new List<Button>();

    private void Awake()
    {
        onSkillPanelToggle += ToggleSkillPanel;
        OnGetSkills += GetCharacterSkills;
        OnClearSkills += ClearButtonList;
        _rt = _skillPanel.GetComponent<RectTransform>();
        _startPos = _rt.anchoredPosition.x;
        _endPos = _rt.anchoredPosition.x + 250;
        //_skillPanel.transform.localScale = Vector3.zero;
    }
    
    void Start () {

        StartCoroutine(LateStart());
	}

    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        GetCharacterSkills();
    }

    void learnTempSkill(Character chtr)
    {
        LearnSkill.onLearnSkill(chtr, new BeatDown());
    }

    void GetCharacterSkills()
    {
        Character character = TurnBasedStateMachine.ActiveCharacter;
        for (int i = 0; i < character.SkillList.Count; i++)
        {
            Button newSkillButton = Instantiate(_skillButton);
            _skillButtons.Add(newSkillButton);
            newSkillButton.name = character.SkillList[i].AbilityName;
            Text skillName = newSkillButton.GetComponentInChildren<Text>();
            skillName.text = character.SkillList[i].AbilityName;
            newSkillButton.transform.SetParent(_skillPanel.transform, false);
            UseSkill(newSkillButton, i);
        }
    }

    void ClearButtonList()
    {
        for (int i = 0; i < _skillButtons.Count; i++)
        {
            Destroy(_skillButtons[i].gameObject);
            _skillButtons.RemoveAt(i);
        }
    }

    void UseSkill(Button button, int value)
    {
        button.onClick.AddListener(() =>
        {
            foreach (PartyMember character in TurnBasedStateMachine.ActivePlayers)
            {
                PartyMember partyMember = character;

                TurnBasedStateMachine.PlayerUsedSkill = TurnBasedStateMachine.ActiveCharacter.SkillList[value];
                TurnBasedStateMachine.PlayerUsedSkill.ExecuteAction();
            }
        });
    }

    public void ToggleSkillPanel()
    {
        if (_skillPanel.gameObject.activeSelf == true)
        {
            SkillPanelEnabled = false;
            _openPanelTween.Kill();
            _closePanelTween = _rt.DOAnchorPosX(_startPos, 0.5f).SetEase(Ease.InExpo);
            _closePanelTween.onComplete += DisableSkillsPanel;
        }
        else
        {
            if (ManageTargeting.IsEnabled) 
                ManageTargeting.DisableTargets();

            SkillPanelEnabled = true;
            _closePanelTween.Kill();
            _skillPanel.gameObject.SetActive(true);
            _openPanelTween = _rt.DOAnchorPosX(_endPos, 0.5f).SetEase(Ease.OutExpo);
        }
    }

    private void OnDisable()
    {
        onSkillPanelToggle -= ToggleSkillPanel;
        OnGetSkills -= GetCharacterSkills;
        OnClearSkills -= ClearButtonList;
    }

    void DisableSkillsPanel()
    {
        _skillPanel.gameObject.SetActive(false);

        if (ManageTargeting.IsEnabled) 
            ManageTargeting.DisableTargets();
    }
}