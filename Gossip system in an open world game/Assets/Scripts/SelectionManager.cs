using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance {set;get;}
    [SerializeField] private TextMeshProUGUI InteractionInfo;
    public bool onTarget;


    void Awake()
    {
        if(Instance != null & Instance != this) Destroy(gameObject);
        else Instance = this;
    }
    private void Start()
    {
        InteractionInfo.text = "";
        onTarget = false;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;
            InteractableObject selectedObj = selectionTransform.GetComponent<InteractableObject>();

            if (selectedObj && selectedObj.IsPlayerInRange)
            {
                InteractionInfo.transform.position = Input.mousePosition;
                InteractionInfo.text = selectedObj.GetItemName();            
                onTarget = true;
            }
            else
            {
                InteractionInfo.text = "";
                onTarget = false;
            }

        }
        else
        {
            InteractionInfo.text = "";
            onTarget = false;
        }
    }
    public static SelectionManager GetInstance()
    {
        return Instance;
    }
}
