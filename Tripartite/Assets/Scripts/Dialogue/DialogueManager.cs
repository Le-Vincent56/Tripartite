using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Tripartite.Dialogue;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    #region FIELDS
    [Header("Fact Sheets")]
    [SerializeField] private FactSheet factSheet;

    [Space(20)]
    [Header("Query")]
    [SerializeField] private ResponseQuery currentQuery;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnReceiveDialogue(Component sender, object data)
    {
        if (!(data is ResponseQuery)) return;

        currentQuery = (ResponseQuery)data;

        // Compile more facts onto the query
        switch(currentQuery.Get("listener").value)
        {
            // Id
            case 15067f:
                foreach(KeyValuePair<string, float> keyValue in factSheet.facts["Id"])
                {
                    currentQuery.Add(keyValue.Key, keyValue.Value);
                }
                break;

            // Ego
            case 15068f:
                foreach (KeyValuePair<string, float> keyValue in factSheet.facts["Ego"])
                {
                    currentQuery.Add(keyValue.Key, keyValue.Value);
                }
                break;

            // Superego
            case 15069f:
                foreach (KeyValuePair<string, float> keyValue in factSheet.facts["Superego"])
                {
                    currentQuery.Add(keyValue.Key, keyValue.Value);
                }
                break;
        }
    }
}
