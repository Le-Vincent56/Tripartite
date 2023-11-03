using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tripartite.Dialogue
{
    [CreateAssetMenu(menuName = "Dialogue System/Rule Sheet")]
    public class RuleSheet : ScriptableObject
    {
        #region FIELDS
        [SerializeField] public List<Rule> rules;
        #endregion
    }
}
