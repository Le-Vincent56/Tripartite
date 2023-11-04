using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
                if (writerQueue.Peek().IsActive())
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
        public static TextWriterSingle AddWriter_Static(TextWithScrollbar textUI, string color, string text, float timePerCharacter)
        {
            // Create single text writer with data
            return instance.AddWriter(textUI, color, text, timePerCharacter);
        }

        /// <summary>
        /// Add a TextWriterSingle to the list
        /// </summary>
        /// <param name="textUI">The Text UI object</param>
        /// <param name="text">The text to show</param>
        /// <param name="timePerCharacter">How fast to show a character</param>
        /// <param name="invisibleCharacters">Keep invisible characters</param>
        private TextWriterSingle AddWriter(TextWithScrollbar textUI, string color, string text, float timePerCharacter)
        {
            TextWriterSingle textWriterSingle = new TextWriterSingle(textUI, color, text, timePerCharacter);

            // Create single text writer with data
            writerQueue.Enqueue(textWriterSingle);

            return textWriterSingle;
        }

        public class TextWriterSingle
        {
            #region FIELDS
            private TextWithScrollbar textUI;
            string color;
            private string textToWrite;
            private string textToAdd;
            private int characterIndex;
            private float timePerCharacter;
            private float timer;
            private bool addedNewLine;
            #endregion

            public TextWriterSingle(TextWithScrollbar textUI, string color, string textToWrite, float timePerCharacter)
            {
                this.textUI = textUI;
                this.color = color;
                this.textToWrite = textToWrite;
                this.timePerCharacter = timePerCharacter;
                characterIndex = 0;
                timer = 0;
                addedNewLine = false;
            }

            /// <summary>
            /// Update the TextWriterSingle
            /// </summary>
            /// <returns>True if the text has completed, false if not</returns>
            public void Update()
            {
                // If textUI is null, return
                if (textUI == null) return;

                if(characterIndex == 0)
                {
                    textUI.AppendTextNewLine($"<color={color}></color>");
                }

                // Check if the text has ended
                if (characterIndex >= textToWrite.Length)
                {
                    if (!addedNewLine)
                    {
                        // Append the last color tag
                        textUI.AppendTextNewLine("");

                        addedNewLine = true;
                    }

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
                    

                    // Write the text up to the character index
                    textToAdd = $"{textToWrite[characterIndex]}";

                    // Show the text
                    textUI.InsertText(textToAdd, textUI.text.text.Length - 8);

                    // Increment characterIndex
                    characterIndex++;
                }
            }

            /// <summary>
            /// Get the Text object associated with the TextWriterSingle
            /// </summary>
            /// <returns></returns>
            public Text GetTextUI()
            {
                return textUI.text;
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
                textUI.InsertText(textToWrite.Substring(characterIndex), textUI.text.text.Length - 8);
                characterIndex = textToWrite.Length;
            }
        }
    }
}