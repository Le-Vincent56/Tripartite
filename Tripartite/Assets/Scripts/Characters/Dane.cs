using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tripartite.UI;
using UnityEngine.InputSystem;
using Tripartite.Events;
using Tripartite.Dialogue;

namespace Tripartite.Characters
{
    public class Dane : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private GameEvent onConversationA00Cont;
        [SerializeField] private GameEvent onTalk;
        [SerializeField] private string textColor;
        private TextWithScrollbar messageText;
        private TextWriter.TextWriterSingle textWriterSingle;

        public string message;
        #endregion

        private void Awake()
        {
            messageText = GetComponent<TextWithScrollbar>();
        }

        /// <summary>
        /// Start the first conversation
        /// </summary>
        /// <param name="sender">The component raising the event</param>
        /// <param name="data">The data being sent</param>
        public void OnConversationA00(Component sender, object data)
        {
            string message = "Well, this is it. I think I’ve finally gone insane.";
            textWriterSingle = TextWriter.AddWriter_Static(messageText, textColor, message, 0.025f);

            message = "It’s been a good run, I think. I kept to myself, I treated my parents well, my dog’s still alive.";
            textWriterSingle = TextWriter.AddWriter_Static(messageText, textColor, message, 0.025f);

            StartCoroutine(WaitForInactiveA00());
        }

        /// <summary>
        /// Check for a Response
        /// </summary>
        /// <param name="sender">The component raising the event</param>
        /// <param name="data">The Response data</param>
        public void OnResponse(Component sender, object data)
        {
            if (!(data is Response)) return;

            // Cast data
            Response response = (Response)data;
            Debug.Log($"Response Received for Speaker {response.speaker.value}");

            // Return if not this speaker
            if (response.speaker.value != 3) return;

            Debug.Log("Response Written!");

            // Add text
            for (int i = 0; i < response.text.Length; i++)
            {
                textWriterSingle = TextWriter.AddWriter_Static(messageText, textColor, response.text[i], response.textSpeed[i]);
            }

            StartCoroutine(WaitForText(response));
        }

        /// <summary>
        /// Wait for text to finish writing before continuing dialogue
        /// </summary>
        /// <param name="response">The current Response</param>
        /// <returns></returns>
        public IEnumerator WaitForText(Response response)
        {
            while(textWriterSingle.IsActive())
            {
                yield return null;
            }

            // If there's another response to be had, raise it
            if (response.CheckNextQuery())
            {
                onTalk.Raise(this, response.nextQuery);
                Debug.Log("Next Response Raised!");
            }
            else
            {
                Debug.LogWarning("No Other Responses");
            }

            // Trigger an event if there is one
            if (response.CheckEvent())
            {
                response.gameEvent.Raise(this, this);
                Debug.Log("Triggered Response Event");
            }

            // Trigger an event for options if there is one
            if (response.CheckEventOptions())
            {
                response.gameEventOptions.Raise(this, response.optionData);
                Debug.Log("Triggered Options Response Event");
            }
        }

        /// <summary>
        /// Wait for the first conversation to end
        /// </summary>
        /// <returns></returns>
        public IEnumerator WaitForInactiveA00()
        {
            while(textWriterSingle.IsActive())
            {
                yield return null;
            }

            ResponseQuery query = new ResponseQuery();

            // Add Ego as the listener
            query.Add(FactKey.Listener, 1);
            query.Add(FactKey.Concept, 6500);
            query.Add(FactKey.ConversationPart, 0);

            // Raise the event
            onConversationA00Cont.Raise(this, query);

            Debug.Log("Raised A00-1");
        }
    }
}

