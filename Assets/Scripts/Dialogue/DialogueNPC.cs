/*
	DialogueNPC.cs
	Created 11/9/2017 2:36:23 PM
	Project DriveBy RPG by DefaultCompany
*/
using UnityEngine;

namespace Dialogue
{
	public class DialogueNPC : MonoBehaviour 
	{
        public delegate void SendDialogueAction(TextAsset dialogueFile);
        public static event SendDialogueAction OnSendDialogue;

        [SerializeField] private TextAsset _dialogueFile;

        [SerializeField] private LayerMask _playerLayer;

        [SerializeField] private GameObject _spaceBarText;
        
        [SerializeField] private float _radius = 1f;

        private bool _isDialogueRunning = false;

        [SerializeField]private int _isTalkedTo = 0;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        private void OnEnable()
        {
            DisplayDialogue.OnDialogueDone += DialogueStop;
            DisplayDialogue.OnDialogueDone += ChangeViewedStatus;
            _isTalkedTo = PlayerPrefs.GetInt(_dialogueFile.name, 0);
        }

        private void OnDisable()
        {
            DisplayDialogue.OnDialogueDone -= DialogueStop;
            DisplayDialogue.OnDialogueDone -= ChangeViewedStatus;
        }

        private void DialogueStop()
        {
            _isDialogueRunning = false;
        }

        private void ChangeViewedStatus()
        {
            _isTalkedTo = 1;
            PlayerPrefs.SetInt(_dialogueFile.name, _isTalkedTo);
        }

        private void FixedUpdate()
        {
            if(_isTalkedTo == 0)
            {
                Collider2D coll = Physics2D.OverlapCircle(transform.position, _radius, _playerLayer);
                if (coll && !_spaceBarText.activeSelf && !_isDialogueRunning)
                {
                    _spaceBarText.SetActive(true);
                    if (OnSendDialogue != null)
                        OnSendDialogue(_dialogueFile);
                    
                    _isDialogueRunning = true;
                }
                else if (!coll && _spaceBarText.activeSelf)
                {
                    DialogueStop();
                    _spaceBarText.SetActive(false);
                }
            }
        }
    }
}