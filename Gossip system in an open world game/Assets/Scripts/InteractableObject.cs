using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string ItemName;
    public bool IsPlayerInRange;
    [SerializeField] private TextAsset InkJson;

    void Update()
    {
        DialogueManager dm = DialogueManager.GetInstance();
        SelectionManager sm = SelectionManager.GetInstance();
        if(IsPlayerInRange && Input.GetKeyDown(KeyCode.Mouse0) && sm.onTarget && !dm.isDialoguePlaying)
        {
            //Debug.Log("Trigger Dialogue with"+ ItemName);
            dm.EnterDialogueMode(InkJson);
        }
    }

    public string GetItemName() {
        return ItemName;
    }
    private void OnTriggerEnter(Collider other)
    {
        IsPlayerInRange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        IsPlayerInRange = false;
    }

}
