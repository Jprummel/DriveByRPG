/*
	DialogueLoader.cs
	Created 11/6/2017 1:55:56 PM
	Project DriveBy RPG by DefaultCompany
*/
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class DialogueLoader : MonoBehaviour
    {
        [SerializeField] private List<TextAsset> _xmlFiles;
        private static Dictionary<string, NodeDeserializer> _ndDictionary = new Dictionary<string, NodeDeserializer>();

        void OnEnable()
        {
            DeserializeAllFiles();
        }

        private void DeserializeAllFiles()
        {
            for (int i = 0; i < _xmlFiles.Count; i++)
            {
                if (!_ndDictionary.ContainsKey(_xmlFiles[i].name))
                {
                    _ndDictionary.Add(_xmlFiles[i].name, NodeDeserializer.Load(_xmlFiles[i]));
                }
            }
        }

        public static DialogueNode LoadDialogueNode(string fileName, int id)
        {
            foreach(DialogueNode dn in _ndDictionary[fileName].DialogueNodes)
            {
                if (dn.NodeID == id)
                    return dn;
            }
            Debug.LogError("Node not found.");
            return null;
        }
    }

}