using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InkExternalFunctions
{
    public IDictionary<string, int> Quests = new Dictionary<string, int>{};

    public void Bind(Story curStory)
    {
        curStory.BindExternalFunction("StartAQuest", (string QuestName, int Phase) => {
            Debug.Log("start a quest");
            if(Quests.ContainsKey(QuestName))
            {
                Quests[QuestName] = Phase;
            }
            else Quests.Add(QuestName, Phase);
        });
        curStory.BindExternalFunction("CheckAQuest", (string QuestName) => {
            if(Quests.ContainsKey(QuestName)) return Quests[QuestName];
            else return 0;
        });
    }
    public void Unbind(Story curStory)
    {
        curStory.UnbindExternalFunction("StartAQuest");
        curStory.UnbindExternalFunction("CheckAQuest");
    }
}
