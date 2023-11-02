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
    [SerializeField] private GameEvent onNewStart;

    private void Start()
    {
        // Begin the game
        if(factSheet.TryGetKey("Global"))
        {
            if (factSheet.facts["Global"].TryGetValue("timesGameLoaded", out float value) && value == 1)
            {
                onNewStart.Raise(this, this);
            }
        }
    }
}
