using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tripartite.Dialogue;
using Tripartite.UI;
using Tripartite.Events;

namespace Tripartite.Characters
{
    public class Ida : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private GameEvent onTalk;
        private Text messageText;
        private TextWriter.TextWriterSingle textWriterSingle;
        #endregion

        private void Awake()
        {
            messageText = GetComponent<Text>();
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
            if (response.speaker.value != 0) return;

            // Add text
            for (int i = 0; i < response.text.Length; i++)
            {
                textWriterSingle = TextWriter.AddWriter_Static(messageText, response.text[i], response.textSpeed[i], true);
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
            while (textWriterSingle.IsActive())
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
        }
    }
}
