using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GossipManager : MonoBehaviour
{

    public static GossipManager Instance;
    private IDictionary<string, GameObject> NPCs = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("Found more than one instance");
        }
        Instance = this;
    }
    public static GossipManager GetInstance()
    {
        return Instance;
    }
    private void Start()
    {
        var Objs = GameObject.FindGameObjectsWithTag("NPC");
        foreach(var o in Objs)
        {
            NPCs.Add(o.name, o);
        }
        Debug.Log("Number of NPCs"+NPCs.Count);
    }
    public void StartGossip(string Spreader, string Receiver)
    {
        // Only gossip to one person now
        SocialSystem SpreaderSys = NPCs[Spreader].GetComponent<SocialSystem>();
        SocialSystem ReceiverSys = NPCs[Receiver].GetComponent<SocialSystem>();

        foreach (string action in SpreaderSys.SocialActionHistory)
        {
            ReceiverSys.AcceptGossip(action);
        }
        SpreaderSys.ClearSocialHistory();
    }

    public SocialSystem GetSpreaderSocialSystem(string Spreader) 
    { 
        return NPCs[Spreader].GetComponent<SocialSystem>();
    }

}
