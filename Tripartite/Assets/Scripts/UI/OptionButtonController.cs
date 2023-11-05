using System.Collections;
using System.Collections.Generic;
using Tripartite.Events;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Tripartite.UI
{
    public class OptionButtonController : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private RectTransform scrollViewRect;
        [SerializeField] private Vector2 scrollViewRectOriginalY;
        [SerializeField] private Vector2 scrollViewRectShiftedY;
        [SerializeField] private Vector2 scrollViewRectCurrentY;
        [SerializeField] [Range(0f, 1f)] private float shiftSpeed;
        [SerializeField] private float buttonFadeSpeed;
        public Button button1;
        public Button button2;
        public Button button3;
        #endregion

        public void Start()
        {
            // Assign original position and current position
            scrollViewRectOriginalY = scrollViewRect.anchoredPosition;
            scrollViewRectCurrentY = scrollViewRectOriginalY;
        }

        /// <summary>
        /// Show the Dialogue options UI
        /// </summary>
        /// <param name="data">The List of OptionData to display</param>
        public IEnumerator ShowOptions(List<OptionData> data)
        {
            // Begin an elapsed time total
            float elapsedTime = 0;

            // Shift the scrollview upwards
            while (scrollViewRectCurrentY.y < scrollViewRectShiftedY.y)
            {
                // Increase elapsed time by Time.deltaTime
                elapsedTime += Time.deltaTime;

                // Calculate the fraction of the journey
                float fractionOfJourney = Mathf.SmoothStep(0, 1, elapsedTime / shiftSpeed);

                // Apply lerping
                scrollViewRectCurrentY = Vector2.Lerp(scrollViewRectOriginalY, scrollViewRectShiftedY, fractionOfJourney);
                scrollViewRect.anchoredPosition = scrollViewRectCurrentY;
                yield return null;
            }

            // Ensure the final position is exactly at scrollViewRectShiftedY
            scrollViewRectCurrentY = scrollViewRectShiftedY;
            scrollViewRect.anchoredPosition = scrollViewRectCurrentY;

            // Set the buttons to be active
            button1.gameObject.SetActive(true);
            button2.gameObject.SetActive(true);
            button3.gameObject.SetActive(true);

            // Show the buttons
            button1.GetComponent<DialogueOption>().Show(buttonFadeSpeed);
            button2.GetComponent<DialogueOption>().Show(buttonFadeSpeed);
            button3.GetComponent<DialogueOption>().Show(buttonFadeSpeed);

            // Assign button data
            AssignDataToButtons(data);
        }

        /// <summary>
        /// Hide the Dialogue options UI
        /// </summary>
        public IEnumerator HideOptions(DialogueOption.Data data)
        {
            // Set the buttons to be inactive
            button1.GetComponent<DialogueOption>().Hide(buttonFadeSpeed);
            button2.GetComponent<DialogueOption>().Hide(buttonFadeSpeed);
            button3.GetComponent<DialogueOption>().Hide(buttonFadeSpeed);

            // Begin an elapsed time total
            float elapsedTime = 0;

            // Shift the scrollview upwards
            while (scrollViewRectCurrentY.y > scrollViewRectOriginalY.y)
            {
                // Increase elapsed time by Time.deltaTime
                elapsedTime += Time.deltaTime;

                // Calculate the fraction of the journey
                float fractionOfJourney = Mathf.SmoothStep(0, 1, elapsedTime / shiftSpeed);

                // Apply lerping
                scrollViewRectCurrentY = Vector2.Lerp(scrollViewRectShiftedY, scrollViewRectOriginalY, fractionOfJourney);
                scrollViewRect.anchoredPosition = scrollViewRectCurrentY;
                yield return null;
            }

            // Ensure the final position is exactly at scrollViewRectShiftedY
            scrollViewRectCurrentY = scrollViewRectOriginalY;
            scrollViewRect.anchoredPosition = scrollViewRectCurrentY;

            // Raise the dialogue event
            data.gameEvent.Raise(this, data.response);
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
        /// Prepare and show Dialogue Options
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
            StartCoroutine(ShowOptions(optionData));
        }

        /// <summary>
        /// Hide the Dialogue Options
        /// </summary>
        /// <param name="sender">The component raising the event</param>
        /// <param name="data">The data being sent</param>
        public void OnHideOptions(Component sender, object data)
        {
            // If the data is the incorrect type, return
            if (!(data is DialogueOption.Data)) return;

            // Cast data
            DialogueOption.Data dialogueData = (DialogueOption.Data)data;

            // Hide the options
            StartCoroutine(HideOptions(dialogueData));
        }
    }
}

