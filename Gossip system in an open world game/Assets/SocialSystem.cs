using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//This system store each NPC's social status and traits.
//e.g. affinity value
public class SocialSystem : MonoBehaviour
{
    //[SerializeField] private int affinity;
    public IDictionary<string, float> CharaterStatus = new Dictionary<string, float>
    {
        {"Affinity", 0f},
        {"Trust", 0f},
        {"Admiration", 0f}
    };
    
    // Definition of impact on some social actions
    private IDictionary<string, float> Insult= new Dictionary<string, float>
    {
        {"Affinity", -5f},
        {"Trust", -5f},
        {"Admiration", -5f}
    };
    private IDictionary<string, float> Praise = new Dictionary<string, float>
    {
        {"Affinity", 3f},
        {"Trust", 3f}
    };
    private IDictionary<string, float> Greeting = new Dictionary<string, float>
    {
        {"Affinity", 1f}
    };
    private IDictionary<string, float> StraightForward = new Dictionary<string, float>
    {
        {"Affinity", -2f}
    };
    private IDictionary<string, float> Bonding = new Dictionary<string, float>
    {
        {"Affinity", 5f},
        {"Trust", 3f}
    };
    private IDictionary<string, float> Rejection = new Dictionary<string, float>
    {
        {"Affinity", -2f},
        {"Trust", -2f}
    };
    private IDictionary<string, float> Help = new Dictionary<string, float>
    {
        {"Affinity", 5f},
        {"Trust", 5f},
        {"Admiration", 5f}
    };
    private IDictionary<string, IDictionary<string, float>> FunctionMap = new Dictionary<string, IDictionary<string, float>>();
    public List<string> SocialActionHistory = new List<string>();
    [SerializeField] GameObject SocialStatusBubble;
    [SerializeField] TextMeshPro SocialStatusText;
    static float STATUS_DISPLAY_TIME = 5f;
    private float CountDownTime = 0;

    private void Start()
    {
        //Register Social Actions here:
        RegisterSocialAction();
        SocialStatusBubble.SetActive(false);
    }
    private void Update()
    {
        if(IsTimeUp()) SocialStatusBubble.SetActive(false);
    }
    private void RegisterSocialAction()
    {
        FunctionMap.Add("Insult", Insult);
        FunctionMap.Add("Praise", Praise);
        FunctionMap.Add("Greeting", Greeting);
        FunctionMap.Add("StraightForward", StraightForward);
        FunctionMap.Add("Bonding", Bonding);
        FunctionMap.Add("Rejection", Rejection);
        FunctionMap.Add("Help", Help);
    }
    public bool StatusChange(string status, float amt)
    {
        if(!CharaterStatus.ContainsKey(status)) return false;
        // if((CharaterStatus[status]+amt) < 0) //In case for negative vals
        // {
        //     CharaterStatus[status] = 0;
        //     return true;
        // }

        //Negative val is acceptable!
        CharaterStatus[status]+= amt;
        return true;
    }
    public void ExecSocialAction(string Action, bool isGossip=false, float GossipDiscount=1) //If accept by gossip, apply gossip a discount
    {
        var Func = FunctionMap[Action];
        foreach(var r in Func)
        {
            if(!isGossip) Debug.Log(gameObject.name+" Accept action:"+Action+" Inludes key"+r.Key+"val:"+r.Value*GossipDiscount);   
            else Debug.Log(gameObject.name+"Accept Gossip Action:"+Action+" Inludes key"+r.Key+"val:"+r.Value*GossipDiscount);   
            if (!StatusChange(r.Key, r.Value*GossipDiscount))
            {
                Debug.LogWarning("Key doesn't exists in status table! key:"+r.Key);  
            }       
        }
        if(!isGossip) SocialActionHistory.Add(Action);
        
        //Display Social Status After changed
        DisplaySocialStatus();
    }
    public void AcceptGossip(string Action, float GossipDiscount=0.5f)
    {
        ExecSocialAction(Action, true, GossipDiscount);
    }
    public void ClearSocialHistory()
    {
        SocialActionHistory.Clear();
    }
    private void DisplaySocialStatus()
    {
        SocialStatusBubble.SetActive(true);
        string Temp = "";
        foreach(var r in CharaterStatus)
        {
            Temp += r.Key+": "+r.Value+"<br>";
            //Debug.Log(gameObject.name+" Status key"+r.Key+"val:"+r.Value);      
        }
        SocialStatusText.text = Temp;
        SetUpTimer(STATUS_DISPLAY_TIME);
    }
    private void SetUpTimer(float c)
    {
        CountDownTime = c;
    }
    private bool IsTimeUp()
    {
        CountDownTime -= Time.deltaTime;
        if (CountDownTime <= 0.0f) return true;
        return false;
    }

}
