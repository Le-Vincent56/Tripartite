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
        public SerializedDictionary<string, SerializedDictionary<string, float>> facts = new SerializedDictionary<string, SerializedDictionary<string, float>>();

        /// <summary>
        /// Save Data from the Fact Sheet
        /// </summary>
        /// <param name="gameData">The GameData to save to</param>
        public void SaveData(GameData gameData)
        {
            if (TryGetKey("Global"))
                gameData.globalFacts = facts["Global"];

            if (TryGetKey("Id"))
                gameData.idFacts = facts["Id"];

            if (TryGetKey("Ego"))
                gameData.egoFacts = facts["Ego"];

            if(TryGetKey("Superego"))
                gameData.superEgoFacts = facts["Superego"];
        }

        /// <summary>
        /// Load data to the Fact Sheet
        /// </summary>
        /// <param name="gameData">The GameData to load from</param>
        public void LoadData(GameData gameData)
        {
            if(TryGetKey("Global")) 
                facts["Global"] = gameData.globalFacts;

            if(TryGetKey("Id")) 
                facts["Id"] = gameData.idFacts;

            if(TryGetKey("Ego")) 
                facts["Ego"] = gameData.egoFacts;

            if(TryGetKey("Superego")) 
                facts["Superego"] = gameData.superEgoFacts;
        }

        /// <summary>
        /// Attempt to get a key from the fact sheet
        /// </summary>
        /// <param name="key">The key to test</param>
        /// <returns>True if the key exists, false if not</returns>
        public bool TryGetKey(string key)
        {
            if (facts.TryGetValue(key, out SerializedDictionary<string, float> result))
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
