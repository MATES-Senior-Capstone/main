using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject TextPanelFab;
    public GameObject TextHolderFab;
    public TextAsset inkJSON1;
    public TextAsset inkJSON2;
    private Story story;
    public Button ContinueButtonFab;
    public Button buttonPrefab;
    public Font ChosenFont;
    public GameObject DialogueCue;
    public bool playerIsClose;
    private Coroutine displayLineCoroutine;
    private Text TextChunk;
    private Button ContinueButton;
    private Button ExitButton;
    private Button choiceButton;
    private bool canContinueCoroutine = false;
    private float TypingSpeed = 0.0025f;
    private bool continueButtonExists = false;
    private bool exitButtonExists = true;
    private bool choiceButtonExists = false;
    
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

    public void CreateTextChunk()
    {
        // Create a new GameObject to hold text
        GameObject TextPanel = Instantiate(TextPanelFab) as GameObject;
        TextPanel.transform.SetParent(Canvas.transform, false);

        GameObject TextHolder = Instantiate(TextHolderFab) as GameObject;
        TextHolder.transform.SetParent(TextPanel.transform, false);

        TextChunk = TextHolder.AddComponent<Text>();
        
        TextChunk.fontSize = 64;
        TextChunk.GetComponent<Text> ().font = ChosenFont;

        // Set the text from new story block
        if (displayLineCoroutine != null)
        {
            StopCoroutine(displayLineCoroutine);
        }
        displayLineCoroutine = StartCoroutine(DisplayLine(getNextStoryBlock()));
        Debug.Log("Story text: " + TextChunk.text);
    }

    private IEnumerator DisplayLine(string line)
    {
        TextChunk.text = "";

        canContinueCoroutine = false;

        foreach (char letter in line.ToCharArray())
        {
            TextChunk.text += letter;
            yield return new WaitForSeconds(TypingSpeed);
        }

        canContinueCoroutine = true;
    }

    void CreateContinue()
    {
            if(story.canContinue){
            //Create a continue button from prefab
            Button ContinueButtonPreFab = Instantiate(ContinueButtonFab) as Button;
            ContinueButton = ContinueButtonPreFab;
            ContinueButton.transform.SetParent(Canvas.transform, false);
            ContinueButton.onClick.AddListener(delegate {OnClickContinueButton();});
            }

            continueButtonExists = true;

            if(!story.canContinue && story.currentChoices.Count == 0 ){
            //Prepares the exit button
            Button ExitButtonPreFab = Instantiate(ContinueButtonFab) as Button;
            ExitButton = ExitButtonPreFab;
            ExitButton.transform.SetParent(Canvas.transform, false);
            ExitButton.onClick.AddListener(delegate {OnClickExitButton();});
            }
            exitButtonExists = true;
    }

    void CreateChoices()
    {
        foreach (Choice choice in story.currentChoices)
        {
            Debug.Log("Creating button for choice: " + choice.text);

            //Creates a button from prefab
            Button choiceButtonPreFab = Instantiate(buttonPrefab) as Button;
            choiceButton = choiceButtonPreFab;
            choiceButton.transform.SetParent(Canvas.transform, false);
        
            choiceButtonExists = true;

            //Destroys the TMP Text child that exists in all buttons
            foreach (Transform child in choiceButton.transform)
             {
            Destroy(child.gameObject);
            }

            //Sets a tag so the waiting system can recongize it
            choiceButton.tag = "ChoiceButton";

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
        if (canContinueCoroutine)
        {
            story.ChooseChoiceIndex(choice.index);
            story.Continue();
            refresh();
        }
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
        continueButtonExists = false;
        exitButtonExists = false;
        choiceButtonExists = false;
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
        if (choiceButtonExists && choiceButton != null){
            if(!canContinueCoroutine)
            {   
                foreach (Transform child in Canvas.transform)
                {
                    Button button = child.GetComponent<Button>();
                    if (button != null && button.CompareTag("ChoiceButton"))
                    {
                     button.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                foreach (Transform child in Canvas.transform)
                {
                    Button button = child.GetComponent<Button>();
                    if (button != null && button.CompareTag("ChoiceButton"))
                    {
                     button.gameObject.SetActive(true);
                    }
                }
            }
        }
        if (continueButtonExists && ContinueButton != null){
            if(!canContinueCoroutine)
            {
                ContinueButton.gameObject.SetActive(false);
            }
            else
            {
                ContinueButton.gameObject.SetActive(true);
            }
        }
        if (exitButtonExists && ExitButton != null){
            if(!canContinueCoroutine)
            {
                ExitButton.gameObject.SetActive(false);
            }
            else
            {
                ExitButton.gameObject.SetActive(true);
            }
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