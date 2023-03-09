using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string ItemName;
    public bool IsPlayerInRange;
    public bool IsNpcInRange;
    public GameObject Npc;
    public Vector3 NpcPos;
    [SerializeField] private TextAsset InkJson;
    [SerializeField] private LayerMask NpcLayer;
    [SerializeField] private LayerMask PlayerLayer;

    void Update()
    {
        DialogueManager dm = DialogueManager.GetInstance();
        SelectionManager sm = SelectionManager.GetInstance();
        if(IsPlayerInRange && Input.GetKeyDown(KeyCode.Mouse0) && sm.onTarget && !dm.isDialoguePlaying)
        {
            //Debug.Log("Trigger Dialogue with"+ ItemName);
            dm.EnterDialogueMode(ItemName, InkJson);
        }
    }

    public string GetItemName() {
        return ItemName;
    }
    private void OnTriggerEnter(Collider other)
    {
        var obj = other.gameObject;
        if(1 << obj.layer == PlayerLayer) IsPlayerInRange = true;
        else if(1 << obj.layer == NpcLayer && obj.transform.position != transform.position)
        {
            IsNpcInRange = true;
            Npc = obj;
            //NpcPos = Npc.transform.position;
            //Debug.Log("Detect a NPC"+obj.transform.position+"My pos:"+transform.position);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var obj = other.gameObject;
        if(1 << obj.layer == PlayerLayer) IsPlayerInRange = false;
        else if(1 << obj.layer == NpcLayer && obj.transform.position != transform.position) IsNpcInRange = false;
            
    }

}
