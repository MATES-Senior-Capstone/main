using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    public GameObject DialogueCue;
    public GameObject Canvas;
    public bool playerIsClose;
    public TextAsset inkJSON1;
    public TextAsset inkJSON2;
    private Story story;
    public Button buttonPrefab;
    public Button ContinueButtonFab;
    public Font ChosenFont;
    void DialogueStart()
    {
        story = new Story(inkJSON1.text);

        refresh();

    }

    // Refresh the UI elements
    //  – Clear any current elements
    //  – Show any text chunks
    //  – Iterate through any choices and create listeners on them
    void refresh()
    {
        clearUI();

        CreateTextChunk();

        CreateContinue();
        
        CreateChoices();
    }

    void CreateTextChunk()
    {
        // Create a new GameObject to hold text
        GameObject newGameObject = new GameObject("TextChunk");
        newGameObject.transform.SetParent(Canvas.transform, false);

        Text newTextObject = newGameObject.AddComponent<Text>();
        
        newTextObject.fontSize = 64;
        newTextObject.GetComponent<Text> ().font = ChosenFont;

        // Set the text from new story block
        string storyText = getNextStoryBlock();
        newTextObject.text = storyText;
        Debug.Log("Story text: " + storyText);
    }

    void CreateContinue()
    {
        if(story.canContinue){
            //Create a continue button from prefab
            Button ContinueButton = Instantiate(ContinueButtonFab) as Button;
            ContinueButton.transform.SetParent(Canvas.transform, false);
            ContinueButton.onClick.AddListener(delegate {OnClickContinueButton();});
        }
        if(!story.canContinue && story.currentChoices.Count == 0){
            //Prepares the exit button
            Button ExitButton = Instantiate(ContinueButtonFab) as Button;
            ExitButton.transform.SetParent(Canvas.transform, false);
            ExitButton.onClick.AddListener(delegate {OnClickExitButton();});
        }
    }

    void CreateChoices()
    {
        foreach (Choice choice in story.currentChoices)
        {
            Debug.Log("Creating button for choice: " + choice.text);

            //Creates a button from prefab
            Button choiceButton = Instantiate(buttonPrefab) as Button;
            choiceButton.transform.SetParent(Canvas.transform, false);
        
            //Destroys the TMP Text child that exists in all buttons
            foreach (Transform child in choiceButton.transform)
             {
            Destroy(child.gameObject);
            }

            // Gets the text from the button prefab
            Text choiceText = choiceButton.GetComponentInChildren<Text>();
            choiceText.text = choice.text;
            choiceText.fontSize = 64;
            choiceText.font = ChosenFont;

            // Sets listener for choices
            choiceButton.onClick.AddListener(delegate {OnClickChoiceButton(choice);});
            
        }
        Debug.Log("Total choices: " + story.currentChoices.Count);
    }
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        story.Continue();
        refresh();
    }

    void OnClickContinueButton()
    {
        if (story.canContinue)
        {
            refresh();
        }
    }

    void OnClickExitButton()
    {
        clearUI();
    }
    void clearUI()
    {
        int childCount = Canvas.transform.childCount;
        for (int i = childCount-1; i >= 0; i--)
        {
            GameObject.Destroy(Canvas.transform.GetChild(i).gameObject);
        }
    }


    // Load and potentially return the next story block
    string getNextStoryBlock()
    {
        string text = "";

        if (story.canContinue)
        {
            text = story.Continue();
        }

        return text;
    }

    void Update()
    {
        if (playerIsClose)
        {
            DialogueCue.SetActive(true);
        }
        else 
        {
            DialogueCue.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            DialogueStart();
        }
        if (Input.GetKeyDown(KeyCode.Q) | !playerIsClose)
        {
            clearUI();
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
            
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
    
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }
}