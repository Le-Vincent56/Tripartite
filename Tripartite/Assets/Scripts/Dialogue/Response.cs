using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tripartite.Events;
using Tripartite.UI;

namespace Tripartite.Dialogue
{
    [CreateAssetMenu(menuName = "Dialogue System/Response")]
    public class Response : ScriptableObject
    {
        #region FIELDS
        public Fact speaker;
        public bool clearResponseBefore;
        public string[] text;
        public float[] textSpeed;
        public ResponseQuery nextQuery;
        public GameEvent gameEvent;
        public GameEvent gameEventOptions;
        public List<OptionData> optionData = new List<OptionData>();
        #endregion

        /// <summary>
        /// Check if there's another ResponseQuery
        /// </summary>
        /// <returns>True if there's another ResponseQuery, false if not</returns>
        public bool CheckNextQuery()
        {
            if (nextQuery == null || nextQuery.facts.Count <= 0)
            {
                return false;
            }
            else return true;
        }

        /// <summary>
        /// Check if there's a GameEvent to trigger
        /// </summary>
        /// <returns>True if there's a GameEvent to trigger, false if not</returns>
        public bool CheckEvent()
        {
            if (gameEvent == null)
            {
                return false;
            }
            else return true;
        }

        /// <summary>
        /// Check if there's a GameEvent to trigger Dialogue Options
        /// </summary>
        /// <returns>True if there's a GameEvent to trigger for Options, false if not</returns>
        public bool CheckEventOptions()
        {
            if (gameEventOptions == null)
            {
                return false;
            }
            else return true;
        }
    }
}
