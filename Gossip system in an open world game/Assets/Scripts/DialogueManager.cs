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
    [Header("Global Ink File")]
    [SerializeField] private TextAsset loadGlobalJson;
    
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
    private bool WaitForClicked = false;
    private const string SPEAKER_TAG = "speaker";
    private const string SOCIAL_ACTION_TAG = "SocialAction";
    private string MAIN_CHARACTER_TAG = MainMenuScript.characterName; 
    public string NPCName = "";
    private SocialSystem DialogueNPC;
    private DialogueVariables dialogueVariables;
    private InkExternalFunctions InkEx;
    
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("Found more than one instance");
        }
        Instance = this;
        dialogueVariables = new DialogueVariables(loadGlobalJson);
        InkEx = new InkExternalFunctions();
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
        HideChoices();
    }
    private void Update()
    {
        if(!isDialoguePlaying || WaitForChoice && !GetSubmitPressed()){
            return;
        } 
        if(WaitForClicked){
            ProceedStory();
            if(!GetSubmitPressed()) return;
        }
        ContinueStory();
    }
    public void EnterDialogueMode(string CharName, TextAsset InkJson)
    {
        NPCName = CharName;
        CurrentStory = new Story(InkJson.text);
        isDialoguePlaying = true;
        DialoguePanel.SetActive(true);
        CharacterTag.SetActive(true);

        // Load NPC status into ink
        DialogueNPC = GameObject.Find(NPCName).GetComponent<SocialSystem>();
        LoadCharacterStatus();
        float temp = (float)CurrentStory.variablesState["Affinity"];
        Debug.Log("After load:"+ temp);

        dialogueVariables.StartListening(CurrentStory);
        InkEx.Bind(CurrentStory);
        ContinueStory();
        
    }
    public void ExitDialogueMode()
    {
        isDialoguePlaying = false;
        DialoguePanel.SetActive(false);
        CharacterTag.SetActive(false);
        DialogueText.text = "";
        dialogueVariables.StopListening(CurrentStory);
        InkEx.Unbind(CurrentStory);
    }
    private void ContinueStory()
    {
        WaitForChoice = false;
        WaitForClicked = false;
        if(CurrentStory.canContinue)
        {
            string c = CurrentStory.Continue();
            if(c.Length > 0)
            {
                Debug.Log("Story can continue"+ c + c.Length);
                DialogueText.text = c;
                DisplayChoices();
                HandleTags(CurrentStory.currentTags);
            }
        }
        else{
            Debug.Log("Exit dialogue!!");
            ExitDialogueMode();
        }
    }
    private void HandleTags(List<string> currentTags)
    {
        // loop through each tag and handle it accordingly
        foreach (string tag in currentTags) 
        {
            // parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2) 
            {
                Debug.LogWarning("Tag could not be appropriately parsed: " + tag);
                continue;
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();
            
            // handle the tag
            switch (tagKey) 
            {
                case SPEAKER_TAG:
                    if(tagValue == "MainCharacter") tagValue = MAIN_CHARACTER_TAG;
                    CharacterName.text = tagValue;
                    break;
                case SOCIAL_ACTION_TAG:
                    Debug.Log("ExecSocialAction"+ tagValue);
                    DialogueNPC.ExecSocialAction(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
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
            WaitForClicked = true;
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
    public void ProceedStory()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            submitPressed = true;
            Debug.Log("player clicked to proceed");
        }
    }
    public Ink.Runtime.Object GetVariableState(string variableName) 
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null) 
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }
    public void LoadCharacterStatus()
    {
        foreach(KeyValuePair<string, float> status in  DialogueNPC.CharaterStatus)
        {
            Debug.Log("Setting up key:"+status.Key+"to value"+status.Value);
            CurrentStory.variablesState[status.Key] = status.Value;
        }
        
    }

}
