using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Tripartite.Dialogue;
using Tripartite.Events;
using Tripartite.UI;

namespace Tripartite.Characters
{
    public class Egor : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private GameEvent onTalk;
        private Text messageText;
        private TextWriter.TextWriterSingle textWriterSingle;

        public string message;
        #endregion

        private void Awake()
        {
            messageText = GetComponent<Text>();
        }

        public void OnResponse(Component sender, object data)
        {
            if (!(data is string)) return;

            textWriterSingle = TextWriter.AddWriter_Static(messageText, (string)data, 0.05f, true);
        }
    }
}
