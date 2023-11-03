using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using Tripartite.Events;
using UnityEngine;

namespace Tripartite.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        #region FIELDS
        [Header("Fact Sheet")]
        [SerializeField] private FactSheet factSheet;

        [Header("Rule Sheets")]
        [SerializeField] private SerializedDictionary<int, RuleSheet> ruleSheets = new SerializedDictionary<int, RuleSheet>();

        [Space(20)]
        [Header("Query")]
        [SerializeField] private ResponseQuery currentQuery;

        [Header("Events")]
        [SerializeField] private GameEvent onResponse;
        #endregion

        public void Awake()
        {
            // Sort the RuleSheet's Rule list by descending amount of Criterion
            foreach(KeyValuePair<int, RuleSheet> kvp in ruleSheets)
            {
                kvp.Value.rules.Sort((a, b) => b.criteria.Count.CompareTo(a.criteria.Count));
            }
        }

        /// <summary>
        /// Allocate ResponseQuery
        /// </summary>
        /// <param name="sender">The component raising the event</param>
        /// <param name="data">The ResponseQuery</param>
        public void OnReceiveDialogue(Component sender, object data)
        {
            if (!(data is ResponseQuery)) return;

            Debug.Log("Received A00-1");

            // Cast the data
            ResponseQuery passedData = (ResponseQuery)data;
            currentQuery = new ResponseQuery();

            // Add all the facts from passed-in data to the current query
            foreach(Fact fact in passedData.facts)
            {
                currentQuery.facts.Add(fact);
            }

            // Compile more facts onto the query based on the listener
            switch (currentQuery.Get(FactKey.Listener).value)
            {
                // Id
                case 0:
                    foreach (KeyValuePair<FactKey, int> keyValue in factSheet.facts["Ida"])
                    {
                        currentQuery.Add(keyValue.Key, keyValue.Value);
                    }
                    break;

                // Ego
                case 1:
                    foreach (KeyValuePair<FactKey, int> keyValue in factSheet.facts["Egor"])
                    {
                        currentQuery.Add(keyValue.Key, keyValue.Value);
                    }
                    break;

                // Superego
                case 2:
                    foreach (KeyValuePair<FactKey, int> keyValue in factSheet.facts["Summer"])
                    {
                        currentQuery.Add(keyValue.Key, keyValue.Value);
                    }
                    break;
            }

            // Compile global facts
            foreach (KeyValuePair<FactKey, int> keyValue in factSheet.facts["Global"])
            {
                currentQuery.Add(keyValue.Key, keyValue.Value);
            }


            // Move on to the next step
            Debug.Log("Query Built");
            OnQueryBuilt();
        }

        /// <summary>
        /// Test the current ResponseQuery against rules
        /// </summary>
        public void OnQueryBuilt()
        {
            Debug.Log($"Checking Responses for {currentQuery.Get(FactKey.Listener).value}");
            // Go to the rule partition of the intended listener
            if (ruleSheets.TryGetValue(currentQuery.Get(FactKey.Listener).value, out RuleSheet value))
            {
                Debug.Log($"Rule Sheet Selected: {value}");

                foreach (Rule rule in value.rules)
                {
                    // If a criteria succeeds, raise it's response - the highest criteria rule will
                    // automatically prevail because sorting by highest amount of criterion
                    if(rule.CheckCriteria(currentQuery))
                    {
                        Debug.Log("Response Raised");
                        onResponse.Raise(this, rule.response);
                    }
                }

                Debug.LogWarning("No Rule Fit");
            } else
            {
                Debug.LogError($"No RuleSheet for {currentQuery.Get(FactKey.Listener).value}");
            }
        }
    }
}
