using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region FIELDS
    public Image innerImage;
    public Text innerText;
    private Color normalFillColor;
    private Color normalTextColor;
    [SerializeField] private Color hoverFillColor;
    [SerializeField] private Color hoverTextColor;
    [SerializeField] float fadeDuration = 0.2f;
    private Coroutine hoverCoroutine;
    #endregion

    void Start()
    {
        // Store the initial color of the inner Image
        normalFillColor = innerImage.color;
        normalTextColor = innerText.color;
    }

    /// <summary>
    /// Change the color of the inner Image and Text UI elements on hover
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Cancel any existing fading coroutine
        if (hoverCoroutine != null)
            StopCoroutine(hoverCoroutine);

        // Start the fading-in coroutine
        hoverCoroutine = StartCoroutine(FadeToColor(hoverFillColor, hoverTextColor));
    }

    /// <summary>
    /// Change the color of the inner Image and Text UI to their original color on hover
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        // Cancel any existing fading coroutine
        if (hoverCoroutine != null)
            StopCoroutine(hoverCoroutine);

        // Start the fading-out coroutine
        hoverCoroutine = StartCoroutine(FadeToColor(normalFillColor, normalTextColor));
    }

    /// <summary>
    /// Fade from the previous color into the new color
    /// </summary>
    /// <param name="targetColor">The color to fade to</param>
    /// <returns></returns>
    private IEnumerator FadeToColor(Color targetFillColor, Color targetTextColor)
    {
        // Create a timer
        float elapsedTime = 0;
        
        // Run the timer for the fade duration
        while (elapsedTime < fadeDuration)
        {
            // Lerp the colors
            innerImage.color = Color.Lerp(innerImage.color, targetFillColor, elapsedTime / fadeDuration);
            innerText.color = Color.Lerp(innerText.color, targetTextColor, elapsedTime / fadeDuration);

            // Update time
            elapsedTime += Time.deltaTime;

            // Allow the other code to run
            yield return null;
        }

        innerImage.color = targetFillColor;
        innerText.color = targetTextColor;
    }
}
