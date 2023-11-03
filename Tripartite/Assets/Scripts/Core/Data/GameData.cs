using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tripartite.Dialogue;

namespace Tripartite.Data
{
    [System.Serializable]
    public class GameData
    {
        #region FIELDS
        public long lastUpdated;

        #region FACTS
        public SerializedDictionary<FactKey, int> globalFacts;
        public SerializedDictionary<FactKey, int> idFacts;
        public SerializedDictionary<FactKey, int> egoFacts;
        public SerializedDictionary<FactKey, int> superEgoFacts;
        #endregion
        #endregion

        // Constructor will have default values for when the game starts when there's no data to load
        public GameData()
        {
            lastUpdated = 0;

            globalFacts = new SerializedDictionary<FactKey, int>()
            {
                {FactKey.TimesGameLoaded, 0},
                {FactKey.TimeSinceLastPlayed, 0}
            };

            idFacts = new SerializedDictionary<FactKey, int>()
            {
                {FactKey.IsSpeaking, 0 },
                {FactKey.TimesInterrupted, 0 },
                {FactKey.TimesIgnored, 0 }
            };

            egoFacts = new SerializedDictionary<FactKey, int>()
            {
                {FactKey.IsSpeaking, 0 },
                {FactKey.TimesInterrupted, 0 },
                {FactKey.TimesIgnored, 0 }
            };

            superEgoFacts = new SerializedDictionary<FactKey, int>()
            {
                {FactKey.IsSpeaking, 0 },
                {FactKey.TimesInterrupted, 0 },
                {FactKey.TimesIgnored, 0 }
            };
        }
    }
}
