using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tripartite.UI
{
    public class TextWriter : MonoBehaviour
    {
        #region FIELDS
        private static TextWriter instance;
        private List<TextWriterSingle> textWriterSingleList;
        #endregion

        private void Awake()
        {
            instance = this;
            textWriterSingleList = new List<TextWriterSingle>();
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < textWriterSingleList.Count; i++)
            {
                bool dialogEnd = textWriterSingleList[i].Update();

                // Check if dialog has ended
                if (dialogEnd)
                {
                    textWriterSingleList.RemoveAt(i);
                    i--;
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
        public static TextWriterSingle AddWriter_Static(Text textUI, string text, float timePerCharacter, bool invisibleCharacters, bool removeWriterBeforeAdd)
        {
            if (removeWriterBeforeAdd)
            {
                instance.RemoveWriter(textUI);
            }

            // Create single text writer with data
            return instance.AddWriter(textUI, text, timePerCharacter, invisibleCharacters);
        }

        /// <summary>
        /// Remove a TextWriterSingle from the list - statically
        /// </summary>
        /// <param name="textUI">The Text object associated with the TextWriterSingle</param>
        public static void RemoveWriter_Static(Text textUI)
        {
            instance.RemoveWriter(textUI);
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
            textWriterSingleList.Add(textWriterSingle);

            return textWriterSingle;
        }

        /// <summary>
        /// Remove a TextWriterSingle from the list
        /// </summary>
        /// <param name="textUI">The Text object associated with the TextWriterSingle</param>
        private void RemoveWriter(Text textUI)
        {
            for (int i = 0; i < textWriterSingleList.Count; i++)
            {
                if (textUI == textWriterSingleList[i].GetTextUI())
                {
                    textWriterSingleList.RemoveAt(i);
                    i--;
                }
            }
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
            public bool Update()
            {
                // If textUI is null, return
                if (textUI == null) return false;

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

                    // Check if the text has ended
                    if (characterIndex >= textToWrite.Length)
                    {
                        // If so, return true
                        return true;
                    }
                }

                // Return false if while ends
                return false;
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