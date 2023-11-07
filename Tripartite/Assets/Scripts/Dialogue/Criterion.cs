using System.Collections;
using System.Collections.Generic;
using Tripartite.Dialogue;
using UnityEditor;
using UnityEngine;

namespace Tripartite.Dialogue
{
    [System.Serializable]
    public enum CriterionOperator
    {
        [InspectorName("[...]")] ClosedInterval = 3,
        [InspectorName(" = ")] Equal = 0,
        [InspectorName(" ≥ ")] GreaterEqual = 1,
        [InspectorName(" ≤ ")] LessEqual = 2
    }


    [System.Serializable]
    [CreateAssetMenu(menuName = "Dialogue System/Criterion")]
    public class Criterion : ScriptableObject
    {
        public string _name;
        public FactKey key;
        public CriterionOperator criterionOperator;
        public int value;

        /// <summary>
        /// Evaluate the Criterion if the query has it
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public bool Evaluate(ResponseQuery query)
        {
            // Loop through all of the facts in the query
            foreach(Fact fact in query.facts)
            {
                // If the Fact key doesn't equal the criterion key, skip over it
                if (fact.key != key) continue;

                // Check the criterion operator
                switch (criterionOperator)
                {
                    // Evluate the value according to the operator and return a bool
                    case CriterionOperator.Equal:
                        return fact.value.Equals(value);

                    case CriterionOperator.LessEqual:
                        return Comparer.Default.Compare(fact.value, value) < 0;

                    case CriterionOperator.GreaterEqual:
                        return Comparer.Default.Compare(fact.value, value) > 0;
                }
            }

            // Return false if nothing is found
            return false;
        }

        public string GetName() { return _name; }
    }
}
