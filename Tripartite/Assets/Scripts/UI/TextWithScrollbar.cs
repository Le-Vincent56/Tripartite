using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWithScrollbar : MonoBehaviour
{
    #region FIELDS
    public Text text;
    #endregion

    /// <summary>
    /// Append text on a new line
    /// </summary>
    /// <param name="newText"></param>
    public void AppendTextNewLine(string newText)
    {
        text.text += "\n" + newText;
    }

    /// <summary>
    /// Append new text on the same line
    /// </summary>
    /// <param name="newText"></param>
    public void AppendText(string newText)
    {
        text.text += newText;
    }

    /// <summary>
    /// Insert text in the middle of the Text
    /// </summary>
    /// <param name="newText">The text to insert</param>
    /// <param name="indexToInsert">The index to insert the new text at</param>
    public void InsertText(string newText, int indexToInsert)
    {
        if (indexToInsert >= 0 && indexToInsert <= text.text.Length)
        {
            string currentText = text.text;
            text.text = currentText.Substring(0, indexToInsert) + newText + currentText.Substring(indexToInsert);
        }
        else
        {
            Debug.LogWarning("Invalid index for inserting text.");
        }
    }
}
