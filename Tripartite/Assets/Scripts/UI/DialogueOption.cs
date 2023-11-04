using System.Collections;
using System.Collections.Generic;
using Tripartite.Dialogue;
using Tripartite.Events;
using UnityEngine;

namespace Tripartite.UI
{
    public class DialogueOption : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private GameEvent onHideOptions;
        [SerializeField] private GameEvent gameEvent;
        [SerializeField] private ResponseQuery response;
        #endregion

        public void OnClick()
        {
            Debug.Log("OnClick()");

            // Raise this response
            gameEvent.Raise(this, response);

            // Hide the options
            onHideOptions.Raise(this, this);
        }

        /// <summary>
        /// Set this Option's ResponseQuery
        /// </summary>
        /// <param name="query">The ResponseQuery to set</param>
        public void SetResponse(ResponseQuery query)
        {
            response = new ResponseQuery();

            // Copy over the Facts to this object's response
            foreach(Fact fact in query.facts)
            {
                response.facts.Add(fact);
            }
        }
    }
}
