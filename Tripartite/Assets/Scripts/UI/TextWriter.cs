using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static Tripartite.UI.TextWriter;

namespace Tripartite.UI
{
    public class TextWriter : MonoBehaviour
    {
        #region FIELDS
        private static TextWriter instance;
        [SerializeField] private Queue<TextWriterSingle> writerQueue;
        #endregion

        private void Awake()
        {
            instance = this;
            writerQueue = new Queue<TextWriterSingle>();
        }

        // Update is called once per frame
        void Update()
        {
            // If there are items in the writerQueue, update them
            if(writerQueue.Count > 0)
            {
                writerQueue.Peek().Update();
            }
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            // Only trigger once on click, and check if there are any items in the Queue
            if(context.started && writerQueue.Count > 0)
            {
                // If a current writer is active, write all of it
                if(writerQueue.Peek().IsActive())
                {
                    writerQueue.Peek().WriteAll();
                } else
                {
                    // Otherwise, dequeue it
                    writerQueue.Dequeue();
                }
            }
        }

        /// <summary>
        /// Add a TextWriterSingle to the list
        /// </summary>
        /// <param name="textUI">The Text object to write on</param>
        /// <param name="text">The text to write</param>
        /// <param name="timePerCharacter">How fast to write the message</param>
        /// <param name="invisibleCharacters">Whether to keep invisible character placement</param>
        /// <param name="removeWriterBeforeAdd">Whether to destroy the TextWriterSingle before it</param>
        /// <returns></returns>
        public static TextWriterSingle AddWriter_Static(Text textUI, string text, float timePerCharacter, bool invisibleCharacters)
        {
            // Create single text writer with data
            return instance.AddWriter(textUI, text, timePerCharacter, invisibleCharacters);
        }

        /// <summary>
        /// Add a TextWriterSingle to the list
        /// </summary>
        /// <param name="textUI">The Text UI object</param>
        /// <param name="text">The text to show</param>
        /// <param name="timePerCharacter">How fast to show a character</param>
        /// <param name="invisibleCharacters">Keep invisible characters</param>
        private TextWriterSingle AddWriter(Text textUI, string text, float timePerCharacter, bool invisibleCharacters)
        {
            TextWriterSingle textWriterSingle = new TextWriterSingle(textUI, text, timePerCharacter, invisibleCharacters);

            // Create single text writer with data
            writerQueue.Enqueue(textWriterSingle);

            return textWriterSingle;
        }

        public class TextWriterSingle
        {
            #region FIELDS
            private Text textUI;
            private string textToWrite;
            private string textToShow;
            private int characterIndex;
            private float timePerCharacter;
            private float timer = 0;
            private bool invisibleCharacters;
            #endregion

            public TextWriterSingle(Text textUI, string textToWrite, float timePerCharacter, bool invisibleCharacters)
            {
                this.textUI = textUI;
                this.textToWrite = textToWrite;
                this.timePerCharacter = timePerCharacter;
                this.invisibleCharacters = invisibleCharacters;
                characterIndex = 0;
                timer = 0;
            }

            /// <summary>
            /// Update the TextWriterSingle
            /// </summary>
            /// <returns>True if the text has completed, false if not</returns>
            public void Update()
            {
                // If textUI is null, return
                if (textUI == null) return;

                // Check if the text has ended
                if (characterIndex >= textToWrite.Length)
                {
                    // If so, return true
                    return;
                }

                // Subtract deltaTime from the timer
                timer -= Time.deltaTime;

                // Check if the timer is under 0
                while (timer <= 0f)
                {
                    // If so, then add the time per character and increment the index
                    timer += timePerCharacter;
                    characterIndex++;

                    // Write the text up to the character index
                    textToShow = textToWrite.Substring(0, characterIndex);

                    // If invisible characters are enabled, add the rest of the substring in an invisible color to prevent movement
                    if (invisibleCharacters)
                    {
                        textToShow += $"<color=#00000000>{textToWrite.Substring(characterIndex)}</color>";
                    }

                    // Show the text
                    textUI.text = textToShow;
                }
            }

            /// <summary>
            /// Get the Text object associated with the TextWriterSingle
            /// </summary>
            /// <returns></returns>
            public Text GetTextUI()
            {
                return textUI;
            }

            /// <summary>
            /// Check if the TextWriterSingle is active
            /// </summary>
            /// <returns>True is active, false if not</returns>
            public bool IsActive()
            {
                return characterIndex < textToWrite.Length;
            }

            /// <summary>
            /// Write all the character
            /// </summary>
            public void WriteAll()
            {
                // Show all the text and prevent any more writing
                textUI.text = textToWrite;
                characterIndex = textToWrite.Length - 1;
            }
        }
    }
}