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
    public TextAsset inkJSONAsset;
    private Story story;
    public Button buttonPrefab;

    // Start is called before the first frame update
    void DialogueStart()
    {
        // Load the next story block
        story = new Story(inkJSONAsset.text);

        // Start the refresh cycle
        refresh();

    }

    // Refresh the UI elements
    //  – Clear any current elements
    //  – Show any text chunks
    //  – Iterate through any choices and create listeners on them
    void refresh()
    {
        // Clear the UI
        clearUI();

        // Create a new GameObject
        GameObject newGameObject = new GameObject("TextChunk");
        // Set its transform to the Canvas (this)
        newGameObject.transform.SetParent(Canvas.transform, false);

        // Add a new Text component to the new GameObject
        Text newTextObject = newGameObject.AddComponent<Text>();
        // Set the fontSize larger
        newTextObject.fontSize = 64;

        newTextObject.GetComponent<Text> ().font = Resources.GetBuiltinResource(typeof(Font), "LegacyRuntime.ttf") as Font;

        // Set the text from new story block
        newTextObject.text = getNextStoryBlock();
        Debug.Log("Story text: " + getNextStoryBlock());
        // Load Arial from the built-in resources
        

        foreach (Choice choice in story.currentChoices)
        {
            Debug.Log("Creating button for choice: " + choice.text);
            Button choiceButton = Instantiate(buttonPrefab) as Button;
            choiceButton.transform.SetParent(Canvas.transform, false);

            // Gets the text from the button prefab
            Text choiceText = choiceButton.GetComponentInChildren<Text>();
            choiceText.text = choice.text;
            choiceText.fontSize = 64;

            // Set listener
            choiceButton.onClick.AddListener(delegate {
                OnClickChoiceButton(choice);
            });

        }
        Debug.Log("Total choices: " + story.currentChoices.Count);
    }

    // When we click the choice button, tell the story to choose that choice!
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        story.Continue();
        refresh();

    }

    // Clear out all of the UI, calling Destory() in reverse
    void clearUI()
    {
        int childCount = Canvas.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
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

    // Update is called once per frame
    void Update()
    {
        if (playerIsClose){
            DialogueCue.SetActive(true);
            Debug.Log("Player is close");
        }
        else {
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