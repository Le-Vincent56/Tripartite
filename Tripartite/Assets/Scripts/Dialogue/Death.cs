using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tripartite.UI;
using UnityEngine.InputSystem;

namespace Tripartite.Characters
{
    public class Death : MonoBehaviour
    {
        #region FIELDS
        private Text messageText;
        private TextWriter.TextWriterSingle textWriterSingle;
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
                    string[] messageArray = new string[]
                    {
                    "Where am I?",
                    "What am I doing?",
                    "What is this place?"
                    };

                    string message = messageArray[Random.Range(0, messageArray.Length)];
                    textWriterSingle = TextWriter.AddWriter_Static(messageText, message, 0.05f, true, true);
                }
            }
        }
    }
}

