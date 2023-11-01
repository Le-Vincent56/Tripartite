using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Tripartite.Dialogue;

namespace Tripartite.Data
{
    public class DataManager : MonoBehaviour
    {
        #region REFERENCES
        [SerializeField] private GameData gameData;
        [SerializeField] private FactSheet factSheet;
        public FileDataHandler dataHandler;
        public InputActionAsset actions;
        #endregion

        #region FIELDS
        [SerializeField] private string fileName = "PlayerSave.json";
        [SerializeField] private string settingsFileName = "SettingsSave.json";
        [SerializeField] private bool useEncryption = true;
        [SerializeField] private bool initializeDataIfNull = false; // Use if you don't want to go through the main menu to test data persistence
        [SerializeField] private bool useAutoSave = false;
        private string selectedProfileID = "";
        private string softProfileID = "";
        [SerializeField] private float autoSaveTimeSeconds = 60f;
        private Coroutine autoSaveCoroutine;
        #endregion

        #region PROPERTIES
        public static DataManager Instance { get; private set; }
        public string SelectedProfileID { get { return selectedProfileID; } }
        public string SoftProfileID { get { return softProfileID; } }
        public GameData Data { get { return gameData; } }
        #endregion

        private void Awake()
        {
            // Check if there's already a DataManager
            if (Instance != null)
            {
                // If there is, destroy this one to retain singleton design
                Debug.LogWarning("Found more than one Data Manager in the scene. Destroying the newest one");
                Destroy(gameObject);
                return;
            }
            Instance = this;

            // Don't destroy data management on load
            DontDestroyOnLoad(gameObject);

            // Create a FileDataHandler
            dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);

            InitializeSelectedProfileID();
        }

        private void OnEnable()
        {
            // Subscribe to scene events
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            // Unsubscribe to scene events
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        /// <summary>
        /// Load the save data whenever a scene is loaded
        /// </summary>
        /// <param name="scene">The scene being loaded</param>
        /// <param name="mode">The LoadSceneMode</param>
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Load the Game
            LoadGame();
        }

        /// <summary>
        /// Create a new game
        /// </summary>
        public void NewGame()
        {
            gameData = new GameData();

            // If there is no profileID, make one
            if (selectedProfileID == null)
            {
                selectedProfileID = "0";
            }
        }

        /// <summary>
        /// Save data to a new or existing file
        /// </summary>
        public void SaveGame()
        {
            // If there's no data to save, log a warning and return
            if (gameData == null)
            {
                Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
                return;
            }

            // Pass the data to other scripts so they can update it
            factSheet.SaveData(gameData);

            if(gameData.globalFacts.TryGetValue("timeSinceLastPlayed", out float value))
            {
                gameData.globalFacts["timeSinceLastPlayed"] = System.DateTime.Now.ToBinary();
            }

            // Save that data to a file using the data handler
            dataHandler.Save(gameData, selectedProfileID);
        }

        /// <summary>
        /// Load the game with previously saved data
        /// </summary>
        public void LoadGame()
        {
            // Load any saved data from a file using the data handler
            gameData = dataHandler.Load(selectedProfileID);

            // If no data can be loaded, log a warning and return
            if (gameData == null)
            {
                Debug.LogWarning("No data was found. A New Game needs to be started before data can be loaded");
                return;
            }

            // Push the loaded data to all other scripts that need it
            factSheet.LoadData(gameData);
        }

        /// <summary>
        /// Delete profile data using the current profile ID
        /// </summary>
        /// <param name="profileID"></param>
        public void DeleteProfileData(string profileID)
        {
            // Delete the data for this proile ID
            dataHandler.Delete(profileID);

            // Initialize the selected profile ID
            InitializeSelectedProfileID();

            // Reload the game so that our data matches the newly selected profile ID
            LoadGame();
        }

        /// <summary>
        /// Set the selected profile ID to the most recently updated profile ID
        /// </summary>
        public void InitializeSelectedProfileID()
        {
            selectedProfileID = dataHandler.GetMostRecentlyUpdatedProfileID();
        }

        /// <summary>
        /// Check if there is GameData to save/load
        /// </summary>
        /// <returns>True is there is no gameData yet, false if there is</returns>
        public bool HasGameData()
        {
            // Return if gameData is null or not
            return gameData != null;
        }
    }
}
