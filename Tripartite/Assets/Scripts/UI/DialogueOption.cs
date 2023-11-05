using System.Collections;
using System.Collections.Generic;
using Tripartite.Dialogue;
using Tripartite.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Tripartite.UI
{
    public class DialogueOption : MonoBehaviour
    {
        public struct Data
        {
            public GameEvent gameEvent;
            public ResponseQuery response;

            public Data(GameEvent gameEvent, ResponseQuery responseQuery)
            {
                this.gameEvent = gameEvent;
                this.response = responseQuery;
            }
        }

        #region FIELDS
        [SerializeField] private Image borderImage;
        [SerializeField] private Text text;
        [SerializeField] private GameEvent onHideOptions;
        [SerializeField] private GameEvent gameEvent;
        [SerializeField] private ResponseQuery response;
        #endregion

        public void OnClick()
        {
            // Hide the options
            onHideOptions.Raise(this, new Data(gameEvent, response));
        }

        /// <summary>
        /// Set this Option's ResponseQuery
        /// </summary>
        /// <param name="query">The ResponseQuery to set</param>
        public void SetResponse(ResponseQuery query)
        {
            response = new ResponseQuery();

            // Copy over the Facts to this object's response
            foreach(Fact fact in query.facts)
            {
                response.facts.Add(fact);
            }
        }

        /// <summary>
        /// Show the Button and Dialogue Option
        /// </summary>
        /// <param name="fadeSpeed">The speed to fade in at</param>
        public void Show(float fadeSpeed)
        {
            StartCoroutine(FadeIn(fadeSpeed));
        }

        /// <summary>
        /// Hide the Button and Dialogue Option
        /// </summary>
        /// <param name="fadeSpeed">The speed to fade out at</param>
        public void Hide(float fadeSpeed)
        {
            StartCoroutine(FadeOut(fadeSpeed));
        }

        /// <summary>
        /// Fade in the border and text of the Button at a certain speed
        /// </summary>
        /// <param name="fadeSpeed">The speed to fade in the border and text</param>
        /// <returns></returns>
        public IEnumerator FadeIn(float fadeSpeed)
        {
            while (borderImage.color.a < 1 && text.color.a < 1)
            {
                // Get the current color
                Color imageColor = borderImage.color;
                Color textColor = text.color;

                // Subtract from the current color
                imageColor.a += Time.deltaTime * fadeSpeed;
                textColor.a += Time.deltaTime * fadeSpeed;

                // Set the current color
                borderImage.color = imageColor;
                text.color = textColor;

                // Allow other code to run
                yield return null;
            }
        }

        /// <summary>
        /// Fade out the border and text of the Button at a certain speed
        /// </summary>
        /// <param name="fadeSpeed">The speed to fade out the border and text</param>
        /// <returns></returns>
        public IEnumerator FadeOut(float fadeSpeed)
        {
            while (borderImage.color.a > 0 && text.color.a > 0)
            {
                // Get the current color
                Color imageColor = borderImage.color;
                Color textColor = text.color;

                // Subtract from the current color
                imageColor.a -= Time.deltaTime * fadeSpeed;
                textColor.a -= Time.deltaTime * fadeSpeed;

                // Set the current color
                borderImage.color = imageColor;
                text.color = textColor;

                // Allow other code to run
                yield return null;
            }

            // Deactivate the object
            gameObject.SetActive(false);
        }
    }
}
