using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Tripartite.Dialogue;
using System.Linq;

namespace Tripartite.EditorUI
{
    [InitializeOnLoad]
    public static class EditorHelp
    {
        private static IEnumerable<Criterion> criteria = null;

        static EditorHelp()
        {
            GetCriteria();
        }

        private static bool TryGetAllAssetFiles<T>(out List<T> assets, string filter = "DefaultAsset l:noLabel t:noType") where T : ScriptableObject {
            assets = new List<T>();
            assets.Add(item: null); // for the unassigned asset file option
            string[] findAssets = AssetDatabase.FindAssets(filter);

            if(findAssets.Length > 0)
            {
                foreach(string asset in findAssets)
                {
                    string assetPath = AssetDatabase.GUIDToAssetPath(asset);
                    assets.Add(AssetDatabase.LoadAssetAtPath(assetPath, typeof(T)) as T);
                }

                return true;
            } else
            {
                return false;
            }
        }

        private static bool CacheAssetFiles<T>(out IEnumerable<T> targetEnumerable, string filter) where T : ScriptableObject
        {
            targetEnumerable = null;
            if(TryGetAllAssetFiles<T>(out List<T> assetsList, filter))
            {
                targetEnumerable = assetsList;
                return true;
            }

            return false;
        }

        public static Dictionary<string, Criterion> GetCriteria(bool requireTest = false, Criterion testCriterion = null)
        {
            if (criteria == null || (requireTest == true && !criteria.Contains(testCriterion)))
                CacheAssetFiles(out criteria, "t:Criterion");

            return criteria.ToDictionary(type => type == null ? "Unassigned" : type.GetName());
        }
    }
}
