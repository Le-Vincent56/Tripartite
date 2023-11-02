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
        [SerializeField] private GameEvent onConversationA01;
        [SerializeField] private GameEvent onTalk;
        private Text messageText;
        private TextWriter.TextWriterSingle textWriterSingle;

        public string message;
        #endregion

        private void Awake()
        {
            messageText = GetComponent<Text>();
        }

        #region DIALOGUE EVENTS
        public void OnConversationA00(Component sender, object data)
        {
            string message = "Well, this is it. I think I’ve finally gone insane.";
            textWriterSingle = TextWriter.AddWriter_Static(messageText, message, 0.05f, true);

            message = "It’s been a good run, I think. I kept to myself, I treated my parents well, my dog’s still alive.";
            textWriterSingle = TextWriter.AddWriter_Static(messageText, message, 0.05f, true);

            ResponseQuery query = new ResponseQuery();

            // Add Ego as the listener
            query.Add("listener", 15068f);
            query.Add("concept", 6500f);
        }
        #endregion

        public void OnResponse(Component sender, object data)
        {
            if (!(data is string)) return;

            textWriterSingle = TextWriter.AddWriter_Static(messageText, (string)data, 0.05f, true);
        }
    }
}

