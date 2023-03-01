using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.InputSystem;

//This is a singleton class
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject DialoguePanel;
    [SerializeField] private TextMeshProUGUI DialogueText;
    [SerializeField] private GameObject[] Choices;

    private TextMeshProUGUI[] ChoicesText;
    public static DialogueManager Instance;
    public bool isDialoguePlaying {get; private set;}
    private Story CurrentStory; 
    private bool submitPressed = false;
    
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("Found more than one instance");
        }
        Instance = this;
    }
    public static DialogueManager GetInstance()
    {
        return Instance;
    }
    private void Start()
    {
        isDialoguePlaying = false;
        DialoguePanel.SetActive(false);
        ChoicesText = new TextMeshProUGUI[Choices.Length];
        int index = 0;
        foreach(GameObject c in Choices)
        {
             ChoicesText[index] = c.GetComponentInChildren<TextMeshProUGUI>();
             index++;
        }
    }
    private void Update()
    {
        if(!isDialoguePlaying) return;
        if(GetSubmitPressed())
        {
            ContinueStory();
        }
    }
    public void EnterDialogueMode(TextAsset InkJson)
    {
        CurrentStory = new Story(InkJson.text);
        isDialoguePlaying = true;
        DialoguePanel.SetActive(true);
        ContinueStory();
        
    }
    public void ExitDialogueMode()
    {
        isDialoguePlaying = false;
        DialoguePanel.SetActive(false);
        DialogueText .text = "";
    }
    private void ContinueStory()
    {
        if(CurrentStory.canContinue)
        {
            DialogueText.text = CurrentStory.Continue();
            DisplayChoices();
        }
        else{
            ExitDialogueMode();
        }
    }
    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            submitPressed = true;
        }
        else if (context.canceled)
        {
            submitPressed = false;
        } 
    }
    public bool GetSubmitPressed() 
    {
        bool result = submitPressed;
        submitPressed = false;
        return result;
    }
    public void RegisterSubmitPressed() 
    {
        submitPressed = false;
    }
    private void DisplayChoices()
    {
        List<Choice> CurrentChoices = CurrentStory.currentChoices;
        if(CurrentChoices.Count < Choices.Length)
        {
            Debug.LogError("More choices were given than UI can support. Number of choices given:"+ CurrentChoices.Count);
        }
        int i = 0;
        foreach(Choice c in CurrentChoices)
        {
            Choices[i].gameObject.SetActive(true);
            ChoicesText[i].text = c.text;
            i++; 
        }
        //Hide those choices that UI can't support
        for(;i<Choices.Length;i++)
        {
            Choices[i].gameObject.SetActive(false); 
        }
    }
}
