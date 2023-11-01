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
        [SerializeField] private GameEvent onTalk;
        private Text messageText;
        private TextWriter.TextWriterSingle textWriterSingle;

        public string message;
        #endregion

        private void Awake()
        {
            messageText = GetComponent<Text>();
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                if (textWriterSingle != null && textWriterSingle.IsActive())
                {
                    textWriterSingle.WriteAll();
                }
                else
                {
                    ResponseQuery query = new ResponseQuery();
                    query.Add("listener", 15067f);
                    query.Add("timesGameLoaded", 0f);
                    
                    onTalk.Raise(this, query);
                }
            }
        }

        public void OnResponse(Component sender, object data)
        {
            if (!(data is string)) return;

            textWriterSingle = TextWriter.AddWriter_Static(messageText, (string)data, 0.05f, true, true);
        }
    }
}

