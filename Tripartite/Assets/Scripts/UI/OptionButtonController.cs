using System.Collections;
using System.Collections.Generic;
using Tripartite.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Tripartite.UI
{
    public class OptionButtonController : MonoBehaviour
    {
        #region FIELDS
        public Button button1;
        public Button button2;
        public Button button3;
        #endregion

        /// <summary>
        /// Show the Dialogue options UI
        /// </summary>
        /// <param name="data">The List of OptionData to display</param>
        public void ShowOptions(List<OptionData> data)
        {
            // Set the buttons to be active
            button1.gameObject.SetActive(true);
            button2.gameObject.SetActive(true);
            button3.gameObject.SetActive(true);

            AssignDataToButtons(data);
        }

        /// <summary>
        /// Hide the Dialogue options UI
        /// </summary>
        public void HideOptions()
        {
            // Set the buttons to be inactive
            button1.gameObject.SetActive(false);
            button2.gameObject.SetActive(false);
            button3.gameObject.SetActive(false);
        }

        /// <summary>
        /// Assign the Option Data to the buttons
        /// </summary>
        /// <param name="data">The List of OptionData to assign</param>
        public void AssignDataToButtons(List<OptionData> data)
        {
            button1.GetComponentInChildren<Text>().text = data[0].text;
            button1.GetComponent<DialogueOption>().SetResponse(data[0].response);

            button2.GetComponentInChildren<Text>().text = data[1].text;
            button2.GetComponent<DialogueOption>().SetResponse(data[1].response);

            button3.GetComponentInChildren<Text>().text = data[2].text;
            button3.GetComponent<DialogueOption>().SetResponse(data[2].response);
        }

        /// <summary>
        /// Prepare and Show Options
        /// </summary>
        /// <param name="sender">The component raising the event</param>
        /// <param name="data">The data being sent</param>
        public void OnShowOptions(Component sender, object data)
        {
            // If the data is the incorrect type, return
            if (!(data is List<OptionData>)) return;

            // Cast data
            List<OptionData> optionData = (List<OptionData>)data;

            // Show options
            ShowOptions(optionData);
        }

        public void OnHideOptions(Component sender, object data)
        {
            // Hide the options
            HideOptions();
        }
    }
}

