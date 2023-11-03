using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using Tripartite.Data;
using UnityEngine;

namespace Tripartite.Dialogue
{
    [CreateAssetMenu(menuName = "Fact Sheet")]
    public class FactSheet : ScriptableObject
    {
        public SerializedDictionary<string, SerializedDictionary<FactKey, int>> facts = new SerializedDictionary<string, SerializedDictionary<FactKey, int>>();

        /// <summary>
        /// Save Data from the Fact Sheet
        /// </summary>
        /// <param name="gameData">The GameData to save to</param>
        public void SaveData(GameData gameData)
        {
            if (TryGetKey("Global"))
                gameData.globalFacts = facts["Global"];

            if (TryGetKey("Ida"))
                gameData.idFacts = facts["Ida"];

            if (TryGetKey("Egor"))
                gameData.egoFacts = facts["Egor"];

            if(TryGetKey("Summer"))
                gameData.superEgoFacts = facts["Summer"];
        }

        /// <summary>
        /// Load data to the Fact Sheet
        /// </summary>
        /// <param name="gameData">The GameData to load from</param>
        public void LoadData(GameData gameData)
        {
            if(TryGetKey("Global")) 
                facts["Global"] = gameData.globalFacts;

            if(TryGetKey("Ida")) 
                facts["Ida"] = gameData.idFacts;

            if(TryGetKey("Egor")) 
                facts["Egor"] = gameData.egoFacts;

            if(TryGetKey("Summer")) 
                facts["Summer"] = gameData.superEgoFacts;
        }

        /// <summary>
        /// Attempt to get a key from the fact sheet
        /// </summary>
        /// <param name="key">The key to test</param>
        /// <returns>True if the key exists, false if not</returns>
        public bool TryGetKey(string key)
        {
            if (facts.TryGetValue(key, out SerializedDictionary<FactKey, int> result))
            {
                return true;
            }
            else
            {
                Debug.LogError($"No key {key} found in Fact Sheet");
                return false;
            }
        } 
    }
}
