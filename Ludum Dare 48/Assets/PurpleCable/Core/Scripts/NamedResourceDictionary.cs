using UnityEngine;

namespace PurpleCable
{
    public class NamedResourceDictionary<TScriptableObject> : ResourceDictionary<string, TScriptableObject>
        where TScriptableObject : ScriptableObject
    {
        #region Initialisation

        public NamedResourceDictionary(string path)
            : base(path)
        { }

        #endregion

        #region Methods

        protected override string GetKeyForItem(TScriptableObject item)
        {
            return item.name;
        }

        #endregion
    }
}
