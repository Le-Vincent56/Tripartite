using System.Collections;
using System.Collections.Generic;
using Tripartite.Events;
using UnityEngine;

namespace Tripartite.Dialogue
{
    [CreateAssetMenu(menuName = "Dialogue System/Rule")]
    public class Rule : ScriptableObject
    {
        #region FIELDS
        public List<Criterion> criteria;
        public Response response;
        #endregion

        /// <summary>
        /// Check how many Criterion are 
        /// </summary>
        /// <param name="query"></param>
        /// <returns>True if all the Criterion are met, false if not</returns>
        public bool CheckCriteria(ResponseQuery query)
        {
            // Establish a counter
            int matches = 0;

            // Loop through the list of Criterion and check how many are met
            foreach(Criterion criterion in criteria)
            {
                if(criterion.Evaluate(query))
                {
                    // If a criteria is met with the current query, update matches
                    matches++;
                }
            }

            // Check if all of the criteria is met
            if(matches != criteria.Count)
            {
                // If not, fail the rule
                return false;
            }

            // Return the matches
            return true;
        }
    }
}
