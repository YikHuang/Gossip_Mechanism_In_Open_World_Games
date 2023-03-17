using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This system contains all characters' personality
// Five-Factor Model (Scale from 0 to 1)
public class PersonalityManager : MonoBehaviour
{
    public static PersonalityManager Instance;
    public IDictionary<string, IDictionary<string, float>> PersonalityDictionary;

    public IDictionary<string, float> Mousey = new Dictionary<string, float>
    {
        {"Openness", 0.7f},
        {"Conscientiousness", 0.6f},
        {"Extraversion", 0.5f},
        {"Agreeableness", 0.8f},
        {"Neuroticism", 0.2f}
    };

    public IDictionary<string, float> Doozy = new Dictionary<string, float>
    {
        {"Openness", 0.2f},
        {"Conscientiousness", 0.1f},
        {"Extraversion", 0.3f},
        {"Agreeableness", 0.2f},
        {"Neuroticism", 0.9f}
    };

    public IDictionary<string, float> Timmy = new Dictionary<string, float>
    {
        {"Openness", 0.5f},
        {"Conscientiousness", 0.6f},
        {"Extraversion", 0.6f},
        {"Agreeableness", 0.7f},
        {"Neuroticism", 0.4f}
    };

    public IDictionary<string, float> Claire = new Dictionary<string, float>
    {
        {"Openness", 0.6f},
        {"Conscientiousness", 0.7f},
        {"Extraversion", 0.8f},
        {"Agreeableness", 0.4f},
        {"Neuroticism", 0.3f}
    };

    public IDictionary<string, float> Elviss = new Dictionary<string, float>
    {
        {"Openness", 0.1f},
        {"Conscientiousness", 0.9f},
        {"Extraversion", 0.1f},
        {"Agreeableness", 0.8f},
        {"Neuroticism", 0.2f}
    };

    public IDictionary<string, float> Eric = new Dictionary<string, float>
    {
        {"Openness", 0.5f},
        {"Conscientiousness", 0.7f},
        {"Extraversion", 0.3f},
        {"Agreeableness", 0.9f},
        {"Neuroticism", 0.5f}
    };

    public IDictionary<string, float> Gramma = new Dictionary<string, float>
    {
        {"Openness", 0.1f},
        {"Conscientiousness", 0.9f},
        {"Extraversion", 0.6f},
        {"Agreeableness", 0.7f},
        {"Neuroticism", 0.1f}
    };


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Found more than one instance");
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StorePersonality();
    }

    private void StorePersonality() {
        PersonalityDictionary = new Dictionary<string, IDictionary<string, float>>
        {
            {"Mousey", Mousey},
            {"Doozy", Doozy},
            {"Timmy", Timmy},
            {"Claire", Claire},
            {"Elviss", Elviss},
            {"Eric", Eric},
            {"Gramma", Gramma},
        };
    }

    public static PersonalityManager GetInstance()
    {
        return Instance;
    }

    public IDictionary<string, float> GetNpcPersonality(string npcName) {

        Debug.Log(npcName + " Personality");
        foreach (var i in PersonalityDictionary[npcName])
        {
            Debug.Log(i.Key + ": " + i.Value);
        }

        return PersonalityDictionary[npcName];
    }

    // All status is having the same rule now
    public float GenerateNpcStatusFromDirectAction(string npcName, string status, float amt) {
        IDictionary<string, float> NpcPersonality = PersonalityDictionary[npcName];

        // Openness
        float opennessEffect = amt;

        if (NpcPersonality["Openness"] >= 0.7f) {
            opennessEffect = 1.15f * amt;
        }
        else if (NpcPersonality["Openness"] < 0.3f) {
            opennessEffect = 0.85f * amt;
        }


        // Agreeableness
        float agreeablenessEffect = amt;

        if (NpcPersonality["Agreeableness"] >= 0.7f)
        {
            if (amt > 0f)
            {
                agreeablenessEffect = 1.25f * amt;
            }
            else
            {
                agreeablenessEffect = 0.75f * amt;
            }
        }
        else if (NpcPersonality["Agreeableness"] < 0.3f)
        {
            if (amt > 0f)
            {
                agreeablenessEffect = 0.75f * amt;
            }
        }


        // Extraversion
        float extraversionEffect = amt;

        if (NpcPersonality["Extraversion"] >= 0.75f)
        {
            if (amt > 0f)
            {
                extraversionEffect = 1.4f * amt;
            }
            else
            {
                extraversionEffect = 0.6f * amt;
            }
        }
        else if (NpcPersonality["Extraversion"] < 0.75f && NpcPersonality["Extraversion"] >= 0.4f)
        {
            if (amt > 0f)
            {
                extraversionEffect = 1.25f * amt;
            }
            else
            {
                extraversionEffect = 0.75f * amt;
            }
        }


        // Neuroticism
        float neuroticismEffect = amt;

        if (NpcPersonality["Neuroticism"] >= 0.75f) {
            if (amt > 0f)
            {
                neuroticismEffect = 0.5f * amt;
            }
            else
            {
                neuroticismEffect = 1.5f * amt;
            }
        }
        else if (NpcPersonality["Neuroticism"] < 0.75f && NpcPersonality["Neuroticism"] >= 0.4f)
        {
            if (amt > 0f)
            {
                neuroticismEffect = 0.65f * amt;
            }
            else
            {
                neuroticismEffect = 1.4f * amt;
            }
        }

        return (opennessEffect + agreeablenessEffect + extraversionEffect + neuroticismEffect) / 4;
    }


    // All status is having the same rule now
    public float GenerateNpcStatusFromGossip(string npcName, string status, float amt)
    {
        IDictionary<string, float> NpcPersonality = PersonalityDictionary[npcName];

        // Extraversion
        float extraversionEffect = amt;

        if (NpcPersonality["Extraversion"] >= 0.75f)
        {
            if (amt > 0f)
            {
                extraversionEffect = 1.3f * amt;
            }
            else
            {
                extraversionEffect = 0.7f * amt;
            }
        }
        else if (NpcPersonality["Extraversion"] < 0.75f && NpcPersonality["Extraversion"] >= 0.4f)
        {
            if (amt > 0f)
            {
                extraversionEffect = 1.2f * amt;
            }
            else
            {
                extraversionEffect = 0.8f * amt;
            }
        }


        // Neuroticism
        float neuroticismEffect = amt;

        if (NpcPersonality["Neuroticism"] >= 0.75f)
        {
            if (amt > 0f)
            {
                neuroticismEffect = 0.6f * amt;
            }
            else
            {
                neuroticismEffect = 1.4f * amt;
            }
        }
        else if (NpcPersonality["Neuroticism"] < 0.75f && NpcPersonality["Neuroticism"] >= 0.4f)
        {
            if (amt > 0f)
            {
                neuroticismEffect = 0.75f * amt;
            }
            else
            {
                neuroticismEffect = 1.35f * amt;
            }
        }


        return (extraversionEffect + neuroticismEffect) / 2;
    }


    // Gossip Decision
    public bool DecideGossip(string npcName)
    {
        IDictionary<string, float> NpcPersonality = PersonalityDictionary[npcName];
        SocialSystem SpreaderSys = GossipManager.GetInstance().GetSpreaderSocialSystem(npcName);

        if (SpreaderSys.SocialActionHistory.Count == 0)
        {
            Debug.Log(npcName + " has nothing to gossip");
            return false;
        }

        // Conscientiousness
        float conscientiousnessProb = 0.5f;
        bool isExtremeEmotion = false;

        if (SpreaderSys.CharaterStatus["Affinity"] <= -75f || SpreaderSys.CharaterStatus["Affinity"] >= 75f
            || SpreaderSys.CharaterStatus["Trust"] <= -75f || SpreaderSys.CharaterStatus["Trust"] >= 75f
            || SpreaderSys.CharaterStatus["Admiration"] <= -75f || SpreaderSys.CharaterStatus["Admiration"] >= 75f)
        {
            isExtremeEmotion = true;
        }

        if (NpcPersonality["Conscientiousness"] >= 0.7f) {
            if (isExtremeEmotion)
            {
                conscientiousnessProb = 0.85f;
            }
            else
            {
                conscientiousnessProb = 0.2f;
            }
        }
        else if (NpcPersonality["Conscientiousness"] < 0.25f) 
        {
            conscientiousnessProb = 0.7f;
        }


        // Extraversion
        float extraversionProb = 0.5f;

        if (NpcPersonality["Extraversion"] >= 0.7)
        {
            extraversionProb = 0.7f;
        }
        else if (NpcPersonality["Extraversion"] < 0.25) 
        {
            extraversionProb = 0.3f;
        }


        // Neuroticism
        float neuroticismProb = 0.5f;

        if (NpcPersonality["Neuroticism"] >= 0.7f)
        {
            neuroticismProb = 0.7f;
        }
        else if (NpcPersonality["Neuroticism"] < 0.25f) 
        {
            neuroticismProb = 0.3f;
        }


        // Gossip Decision
        float gossipProb = (conscientiousnessProb + extraversionProb + neuroticismProb) / 3;
        float rand = Random.Range(0f, 1f);
        Debug.Log(npcName+" gossipProb is" + gossipProb);
        if (rand <= gossipProb)
        {
            return true;
        }
        else 
        {
            return false;
        }

    }

}
