using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tripartite.Dialogue
{
    [System.Serializable]
    public class Fact
    {
        public string key;
        public float value;

        public Fact(string key, float value)
        {
            this.key = key;
            this.value = value;
        }
    }

    [System.Serializable]
    public class ResponseQuery
    {
        #region FIELDS
        public List<Fact> facts = new List<Fact>();
        #endregion
        
        /// <summary>
        /// Add a fact to the query
        /// </summary>
        /// <param name="key">The key of of the fact</param>
        /// <param name="value">The value of the fact</param>
        public void Add(string key, float value)
        {
            facts.Add(new Fact(key, value));
        }

        /// <summary>
        /// Retrieve a fact by the key
        /// </summary>
        /// <param name="key">The key of the fact to find</param>
        /// <returns>A fact if the key is found within the query, null if not</returns>
        public Fact Get(string key)
        {
            // Find a fact within the fact list
            Fact fact = null;

            // Loop through the facts list and attempt to find the key
            for(int i = 0; i < facts.Count; i++)
            {
                // If a key is found, set the fact to the fact with the associated key
                if (facts[i].key == key)
                {
                    fact = facts[i];
                }
            }

            // Return the fact if the key is found, null if not
            return fact;
        }
    }
}
