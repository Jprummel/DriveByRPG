/*
	SpaceToTalk.cs
	Created 11/7/2017 3:34:05 PM
	Project DriveBy RPG by DefaultCompany
*/
using UnityEngine;

namespace Dialogue
{
	public class SpaceToTalk : MonoBehaviour 
	{
        private TextAsset _dialogueFile;

        public delegate void SpaceBarAction(string fileName, int id = 1);
        public static event SpaceBarAction OnSpaceBar;

        private void OnEnable()
        {
            DialogueNPC.OnSendDialogue += ReceiveDialogueFile;
        }

        private void OnDisable()
        {
            DialogueNPC.OnSendDialogue -= ReceiveDialogueFile;
        }

        private void ReceiveDialogueFile(TextAsset dialogueFile)
        {
            _dialogueFile = dialogueFile;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (OnSpaceBar != null)
                    OnSpaceBar(_dialogueFile.name);
                gameObject.SetActive(false);
            }
        }
    }
}