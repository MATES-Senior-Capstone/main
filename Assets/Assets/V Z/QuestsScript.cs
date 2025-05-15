using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using Ink.UnityIntegration;
using Unity.VisualScripting;
using TMPro;

public class QuestsScript : MonoBehaviour
{
    public TextAsset inkJSON;
    private Story story;
    [SerializeField] private TextAsset loadGlobalsJSON;
    private DialogueVariables dialogueVariables;
    public GameObject QuestText;
    public GameObject MenuPanel;
    public GameObject Player;

    void OnEnable()
    {
        Debug.Log("QuestsScript is enabled");
        if (MenuPanel.activeSelf == true)
        {
            if (Player == null)
            {
                Player = GameObject.FindWithTag("Player");
                if (Player != null)
                {
                dialogueVariables = Player.GetComponent<DialogueVariables>();
                }
            }
            Debug.Log("Q2 is enabled");
            story = new Story(inkJSON.text);
            Debug.Log("Story is set");
            dialogueVariables.StartListening(story);
            Debug.Log("Houston we are listening");
            string text = story.Continue();
            Debug.Log("QuestText is set to " + text);
            QuestText.GetComponent<Text>().text = text;
            Debug.Log("QuestText is set");
            dialogueVariables.StopListening(story);
        }
    }
}