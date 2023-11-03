using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using Tripartite.Data;
using Tripartite.Dialogue;
using Tripartite.Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FactSheet factSheet;
    [SerializeField] private GameEvent onConversationA00;

    private void Start()
    {
        // Begin the game
        if (factSheet.TryGetKey("Global"))
        {
            if (factSheet.facts["Global"].TryGetValue(FactKey.TimesGameLoaded, out int value) && value == 1)
            {
                onConversationA00.Raise(this, this);
            }
        }
    }
}
