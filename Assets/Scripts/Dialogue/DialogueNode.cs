/*
	DialogueNode.cs
	Created 11/6/2017 1:43:02 PM
	Project DriveBy RPG by DefaultCompany
*/
using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

namespace Dialogue
{
    [System.Serializable]
    public class DialogueOption
    {
        [XmlElement("Option")]
        public string Option;
        [XmlElement("OptionText")]
        public string OptionText;
        [XmlElement("DestinationNodeID")]
        public int OptionDestination;
    }

    public class DialogueNode
    {
        [XmlElement("NodeID")]
        public int NodeID { get; set; }
        
        [XmlElement("DialogueSource")]
        public string DialogueSource { get; set; }
        
        [XmlElement("Text")]
        public string Text { get; set; }
        
        [XmlArray("Options")]
        [XmlArrayItem("DialogueOption")]
        public DialogueOption[] Options { get; set; }
    }
}