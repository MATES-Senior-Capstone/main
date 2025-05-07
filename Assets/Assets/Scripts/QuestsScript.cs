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

    void OnEnable()
    {
        Debug.Log("QuestsScript is enabled");
        if (MenuPanel.activeSelf == true)
        {
            Debug.Log("Q2 is enabled");
            story = new Story(inkJSON.text);
            Debug.Log("Story is set");
            //dialogueVariables.StartListening(story);
            Debug.Log("Houston we are listening");
            QuestText.GetComponent<Text>().text = story.Continue();
            Debug.Log("QuestText is set");
        }
    }
}