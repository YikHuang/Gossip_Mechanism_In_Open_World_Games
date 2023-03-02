using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

//This is a singleton class
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject DialoguePanel;
    [SerializeField] private GameObject CharacterTag;
    [SerializeField] private TextMeshProUGUI DialogueText;
    [SerializeField] private TextMeshProUGUI CharacterName;
    [SerializeField] private GameObject[] Choices;

    private TextMeshProUGUI[] ChoicesText;
    public static DialogueManager Instance;
    public bool isDialoguePlaying {get; private set;}
    private Story CurrentStory; 
    private bool submitPressed = false;
    private bool WaitForChoice = false;
    
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
        CharacterTag.SetActive(false);

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
        if(!isDialoguePlaying || WaitForChoice && !GetSubmitPressed()) return;
        ContinueStory();
    }
    public void EnterDialogueMode(string CharName, TextAsset InkJson)
    {
        CurrentStory = new Story(InkJson.text);
        isDialoguePlaying = true;
        DialoguePanel.SetActive(true);
        CharacterTag.SetActive(true);
        CharacterName.text = CharName;
        ContinueStory();
        
    }
    public void ExitDialogueMode()
    {
        isDialoguePlaying = false;
        DialoguePanel.SetActive(false);
        CharacterTag.SetActive(false);
        DialogueText.text = "";
        CharacterName.text = "";
    }
    private void ContinueStory()
    {
        WaitForChoice = false;
        if(CurrentStory.canContinue)
        {
            string c = CurrentStory.Continue();
            Debug.Log("Story can continue"+ c);
            DialogueText.text = c;
            DisplayChoices();
        }
        else{
            Debug.Log("Exit dialogue!!");
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
        
        // No choices available
        if(CurrentChoices == null || CurrentChoices.Count == 0)
        {
            Debug.Log("No choices anymore!!");
            return;
        }
        Debug.Log("choice:"+CurrentChoices[0].text);
        WaitForChoice = true;
        if(CurrentChoices.Count > Choices.Length)
        {
            Debug.LogWarning("More choices were given than UI can support. Number of choices given:"+ CurrentChoices.Count);
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
        StartCoroutine(SelectFirstChoice());
    }
    private void HideChoices() 
    {
        int i = 0;
        foreach(GameObject c in Choices)
        {
            c.SetActive(false);
            ChoicesText[i].text = "";
            i++; 
        }
    }
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(Choices[0].gameObject);
    }
    public void MakeChoice(int ChoiceIndex)
    {
        CurrentStory.ChooseChoiceIndex(ChoiceIndex);
        submitPressed = true;
        HideChoices();
    }
}
