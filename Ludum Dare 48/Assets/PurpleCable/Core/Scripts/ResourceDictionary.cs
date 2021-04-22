using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace PurpleCable
{
    public abstract class ResourceDictionary<TKey, TScriptableObject> : KeyedCollection<TKey, TScriptableObject>
        where TScriptableObject : ScriptableObject
    {
        #region Initialisation

        protected ResourceDictionary(string path)
        {
            foreach (var def in Resources.LoadAll<TScriptableObject>(path))
            {
                Add(def);
            }
        }

        #endregion

        #region Methods

        public TScriptableObject Get(TKey key)
            => (Contains(key) ? this[key] : null);

        public TScriptableObject GetRandom()
            => this.ToArray().GetRandom();

        public TScriptableObject[] GetItems()
            => this.ToArray();

        #endregion
    }
}
