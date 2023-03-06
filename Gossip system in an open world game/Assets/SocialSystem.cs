using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This system store each NPC's social status and traits.
//e.g. affinity value
public class SocialSystem : MonoBehaviour
{
    //[SerializeField] private int affinity;
    public IDictionary<string, float> CharaterStatus = new Dictionary<string, float>
    {
        {"Affinity", 0f}
    };
    
    // Definition of impact on some social actions
    private IDictionary<string, float> Insult= new Dictionary<string, float>
    {
        {"Affinity", -3f}
    };
    private IDictionary<string, float> Praise = new Dictionary<string, float>
    {
        {"Affinity", 3f}
    };
    private IDictionary<string, IDictionary<string, float>> FunctionMap = new Dictionary<string, IDictionary<string, float>>();
    public List<string> SocialActionHistory = new List<string>();

    private void Start()
    {
        //Register Social Actions here:
        RegisterSocialAction();
        //SocialActionHistory.Add("Praise");
    }
    private void RegisterSocialAction()
    {
        FunctionMap.Add("Insult", Insult);
        FunctionMap.Add("Praise", Praise);
    }
    public bool StatusChange(string status, float amt)
    {
        if(!CharaterStatus.ContainsKey(status)) return false;
        if((CharaterStatus[status]+amt) < 0) //In case for negative vals
        {
            CharaterStatus[status] = 0;
            return true;
        }
        CharaterStatus[status]+= amt;
        return true;
    }
    private void ExecSocialAction(string Action, bool isGossip, float GossipDiscount=1) //If accept by gossip, apply gossip a discount
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
    }
    public void AcceptGossip(string Action, float GossipDiscount=0.5f)
    {
        ExecSocialAction(Action, true, GossipDiscount);
    }
    public void DisplayCurrentStatus()
    {
        foreach(var r in CharaterStatus)
        {
            Debug.Log(gameObject.name+" Status key"+r.Key+"val:"+r.Value);      
        }
    }
    public void ClearSocialHistory()
    {
        SocialActionHistory.Clear();
    }

}
