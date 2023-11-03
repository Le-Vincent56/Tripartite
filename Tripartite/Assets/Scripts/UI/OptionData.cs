using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tripartite.Dialogue;
using Tripartite.Events;

namespace Tripartite.UI
{
    [CreateAssetMenu(menuName = "Dialogue Option")]
    public class OptionData : ScriptableObject
    {
        #region FIELDS
        public string text;
        public ResponseQuery response;
        #endregion
    }
}
