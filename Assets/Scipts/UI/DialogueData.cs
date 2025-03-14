using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue System/Dialogue")]
public class DialogueData : ScriptableObject
{
    [TextArea(4, 6)] public List<string> dialogueLines;

    private string idDialogue = "LastDialogueID";

    [SerializeField] private int dialogueID;
    public int DialogueID => dialogueID;

    bool haveId = false;

    public void Awake()
    {
        if (dialogueID > 0) { return; }
        if (haveId) { return; }
        int lastID = PlayerPrefs.GetInt(idDialogue, 0);
        lastID++;
        PlayerPrefs.SetInt(idDialogue, lastID);
        PlayerPrefs.Save();
        dialogueID = lastID;
        haveId = true;
    }
}
