using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tripartite.Data
{
    [System.Serializable]
    public class GameData
    {
        #region FIELDS
        public long lastUpdated;

        #region FACTS
        public SerializedDictionary<string, float> globalFacts;
        public SerializedDictionary<string, float> idFacts;
        public SerializedDictionary<string, float> egoFacts;
        public SerializedDictionary<string, float> superEgoFacts;
        #endregion
        #endregion

        // Constructor will have default values for when the game starts when there's no data to load
        public GameData()
        {
            lastUpdated = 0;

            globalFacts = new SerializedDictionary<string, float>()
            {
                {"timesGameLoaded", 0},
                {"timesSinceLastPlayed", 0}
            };

            idFacts = new SerializedDictionary<string, float>()
            {
                {"isSpeaking", 0 },
                {"timesInterrupted", 0 },
                {"timesIgnored", 0 }
            };

            egoFacts = new SerializedDictionary<string, float>()
            {
                {"isSpeaking", 0 },
                {"timesInterrupted", 0 },
                {"timesIgnored", 0 }
            };

            superEgoFacts = new SerializedDictionary<string, float>()
            {
                {"isSpeaking", 0 },
                {"timesInterrupted", 0 },
                {"timesIgnored", 0 }
            };
        }
    }
}
