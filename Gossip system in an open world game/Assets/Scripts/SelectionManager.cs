using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance {set;get;}
    public GameObject interaction_Info_UI;
    public bool onTarget;
    Text interaction_text;


    void Awake()
    {
        if(Instance != null & Instance != this)
        {
            Destroy(gameObject);
        }
        else{
            Instance = this;
        }
    }
    private void Start()
    {
        interaction_text = interaction_Info_UI.GetComponent<Text>();
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
                interaction_text.text = selectedObj.GetItemName();
                interaction_Info_UI.SetActive(true);
                onTarget = true;
            }
            else
            {
                interaction_Info_UI.SetActive(false);
                onTarget = false;
            }

        }
        else
        {
            interaction_Info_UI.SetActive(false);
            onTarget = false;
        }
    }
    public static SelectionManager GetInstance()
    {
        return Instance;
    }
}
