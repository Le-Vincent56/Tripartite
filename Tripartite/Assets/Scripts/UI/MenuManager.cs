using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Tripartite.Data;
using Tripartite.Dialogue;

namespace Tripartite.UI
{
    public class MenuManager : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private FactSheet factSheet;
        [SerializeField] private Button playButton;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private ConfirmationMenu restartConfirmationMenu;
        #endregion

        public void Start()
        {
            // Load the menu
            LoadMenu();
        }

        /// <summary>
        /// Load the menu
        /// </summary>
        public void LoadMenu()
        {
            // Check if any data exists
            if (DataManager.Instance.Data == null)
            {
                // If not, then set up the standard menu
                playButton.gameObject.SetActive(true);
                continueButton.gameObject.SetActive(false);
                restartButton.gameObject.SetActive(false);
            }
            else
            {
                // If data exists, check how many times the game has been loaded
                if (factSheet.TryGetKey("Global"))
                {
                    if (factSheet.facts["Global"].TryGetValue("timesGameLoaded", out float value))
                    {
                        // If the game has been loaded once or more, show the continue/restart menu
                        if (value >= 1)
                        {
                            playButton.gameObject.SetActive(false);
                            continueButton.gameObject.SetActive(true);
                            restartButton.gameObject.SetActive(true);
                        }
                        else
                        {
                            // Otherwise show the standard menu
                            playButton.gameObject.SetActive(true);
                            continueButton.gameObject.SetActive(false);
                            restartButton.gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        // Debug for Fact errors
                        Debug.LogError("No Key called timesGameLoaded in the Global Fact Sheet");
                    }
                }
            }
        }

        /// <summary>
        /// Play the game for the first time
        /// </summary>
        public void OnPlay()
        {
            // Start a new game
            DataManager.Instance.NewGame();

            // Set TimesGameLoaded to 1
            if(factSheet.TryGetKey("Global"))
            {
                if(factSheet.facts["Global"].TryGetValue("timesGameLoaded", out float value))
                {
                    factSheet.facts["Global"]["timesGameLoaded"] = value + 1;
                } else
                {
                    Debug.LogError("No Key called timesGameLoaded in the Global Fact Sheet");
                }
            }

            // Save the game
            DataManager.Instance.SaveGame();

            // Load the game scene
            SceneManager.LoadScene("GameScene");
        }

        /// <summary>
        /// Continue from a saved playthrough
        /// </summary>
        public void OnContinue()
        {
            // Set TimesGameLoaded to 1
            if (factSheet.TryGetKey("Global"))
            {
                if (factSheet.facts["Global"].TryGetValue("timesGameLoaded", out float value))
                {
                    factSheet.facts["Global"]["timesGameLoaded"] = value + 1;
                }
                else
                {
                    Debug.LogError("No Key called timesGameLoaded in the Global Fact Sheet");
                }
            }

            // Save the game
            DataManager.Instance.SaveGame();

            // Load the game scene
            SceneManager.LoadScene("GameScene");
        }

        /// <summary>
        /// Reset save data
        /// </summary>
        public void OnRestart()
        {
            // Activate the confirmation menu
            restartConfirmationMenu.ActivateMenu(
                    "Are you sure you want to restart?",
                    // Function to execute if we confirm
                    () =>
                    {
                        // Reset the data associated with the current selected profile ID
                        DataManager.Instance.ResetProfileData();

                        // Reload the menu
                        LoadMenu();
                    },
                    // Function to execute if we cancel
                    () =>
                    {
                        // Do nothing
                    }
            );
        }
    }
}
