using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace PurpleCable
{
    public abstract class ResourceCollection<TScriptableObject> : Collection<TScriptableObject>
        where TScriptableObject : ScriptableObject
    {
        #region Initialisation

        protected ResourceCollection(string path)
        {
            foreach (var def in Resources.LoadAll<TScriptableObject>(path))
            {
                Add(def);
            }
        }

        #endregion

        #region Methods

        public TScriptableObject GetRandom()
            => this.ToArray().GetRandom();

        public TScriptableObject[] GetItems()
            => this.ToArray();

        #endregion
    }
}
