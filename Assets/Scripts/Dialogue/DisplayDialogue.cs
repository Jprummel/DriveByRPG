/*
	DisplayDialogue.cs
	Created 11/6/2017 3:06:18 PM
	Project DriveBy RPG by DefaultCompany
*/
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Dialogue
{
	public class DisplayDialogue : MonoBehaviour 
	{
        public delegate void DialogueDoneAction();
        public static event DialogueDoneAction OnDialogueDone;

        [SerializeField] private GameObject _dialogueBox;

        [SerializeField] private Image _sourceImage, _playerImage = null;

        [SerializeField] private Text _sourceText;
        [SerializeField] private Text _dialogueText;

        [SerializeField] private List<Text> _optionTexts;
        [SerializeField] private List<Button> _optionButtons;

        private Sprite[] _sprites;

        private Tween _textTween;

        private DialogueNode _dialogueNode;

        private string _fileName;
        private string _optionString = string.Empty;
        [SerializeField] private string _playerName;

        private Coroutine _callBackDelayCoroutine;

        private bool _isFirstDialogue;
        private bool _isPlayertalking;

        private void OnEnable()
        {
            SpaceToTalk.OnSpaceBar += LoadDialogue;
            _isFirstDialogue = true;
        }

        private void OnDisable()
        {
            SpaceToTalk.OnSpaceBar -= LoadDialogue;
        }

        private void Start()
        {
            _sprites = Resources.LoadAll<Sprite>("SourceImages/characters");
        }

        public void LoadDialogue(string fileName,int id)
        {
            _fileName = fileName;
            _dialogueBox.SetActive(true);

            _dialogueNode = DialogueLoader.LoadDialogueNode(fileName, id);

            for (int i = 0; i < _sprites.Length; i++)
            {
                if(_sprites[i].name == _dialogueNode.DialogueSource)
                {
                    _sourceImage.sprite = _sprites[i];
                    break;
                }
            }

            if (_textTween != null)
            {
                _textTween.Kill();
            }

            _dialogueText.text = string.Empty;
            
                TweenCallback showButtonsCallback = FirstTextCallBack;
            if (_optionString != string.Empty)
            {
                _isPlayertalking = true;
                _textTween = _dialogueText.DOText(_optionString, (float)_optionString.ToCharArray().Length / 20, scrambleMode: ScrambleMode.None);
                SwitchHighLight(_isPlayertalking);
            }  
            else
                _textTween = _dialogueText.DOText(_optionString, 0f);
            _textTween.onComplete += showButtonsCallback;

            for (int j = 0; j < _optionButtons.Count; j++)
            {
                _optionButtons[j].onClick.RemoveAllListeners();
                _optionButtons[j].gameObject.SetActive(false);
                _optionTexts[j].text = string.Empty;
            }

            if (_dialogueNode.Options != null)
            {
                for (int i = 0; i < _dialogueNode.Options.Length; i++)
                {
                    _optionTexts[i].text = _dialogueNode.Options[i].Option;
                    int x = i;
                    _optionButtons[x].onClick.AddListener(() => { SetOptionString(_dialogueNode.Options[x].OptionText); });
                    _optionButtons[x].onClick.AddListener(() => { LoadDialogue(_fileName, _dialogueNode.Options[x].OptionDestination); });
                }
            }
        }

        private void FirstTextCallBack()
        {
            _callBackDelayCoroutine = StartCoroutine(FirstCallBackDelay());
        }

        private IEnumerator FirstCallBackDelay()
        {
            _isPlayertalking = false;
            if(!_isFirstDialogue)
                yield return new WaitForSeconds(1);

            _isFirstDialogue = false;

            TweenCallback callback = SecondTextCallBack;
            _dialogueText.text = string.Empty;
            if (_dialogueNode.Text != null)
            {
                SwitchHighLight(_isPlayertalking);
                _textTween = _dialogueText.DOText(_dialogueNode.Text, (float)_dialogueNode.Text.ToCharArray().Length / 20, scrambleMode: ScrambleMode.None);
                _textTween.onComplete += callback;
            }
            else
                SecondTextCallBack();

            _callBackDelayCoroutine = null;
        }

        private void SecondTextCallBack()
        {
            if (_dialogueNode.NodeID == 999)
            {
                Invoke("DisableDialogueBox", 2f);
            }
            else if (_dialogueNode.NodeID == 1000)
            {
                DisableDialogueBox();
            }
            else
            {
                for (int i = 0; i < _dialogueNode.Options.Length; i++)
                {
                    _optionButtons[i].gameObject.SetActive(true);
                }
            }

            _optionString = string.Empty;
        }

        public void SetOptionString(string optionString)
        {
            _optionString = optionString;
        }

        private void DisableDialogueBox()
        {
            _sourceImage.color = Color.white;
            _playerImage.color = Color.white;
            _dialogueBox.SetActive(false);

            if(OnDialogueDone != null)
                OnDialogueDone();
        }

        private void SwitchHighLight(bool sourceIsPlayer)
        {
            if (sourceIsPlayer)
            {
                _sourceImage.color = Color.white;
                _playerImage.color = Color.yellow;
                _sourceText.text = _playerName;
            }
            else
            {
                _sourceImage.color = Color.yellow;
                _playerImage.color = Color.white;
                _sourceText.text = _dialogueNode.DialogueSource;
            }
        }

        public void CompleteDialogue()
        {
            _textTween.Kill();

            if (_callBackDelayCoroutine != null)
                StopCoroutine(_callBackDelayCoroutine);

            if (!_isPlayertalking)
            {
                SwitchHighLight(_isPlayertalking);
                _dialogueText.text = _dialogueNode.Text;
                SecondTextCallBack();
            }
            else
            {
                _dialogueText.text = _optionString;
                FirstTextCallBack();
            }
        }
    }
}