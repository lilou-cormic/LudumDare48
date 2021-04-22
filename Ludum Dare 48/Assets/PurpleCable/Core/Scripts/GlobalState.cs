using System;
using UnityEngine;

namespace PurpleCable
{
    public class GlobalState : MonoBehaviour
    {
        [SerializeField] string _StateName = null;
        public string StateName => _StateName;

        private bool _Value;

        public bool Value
        {
            get { return _Value; }
            set
            {
                if (_Value != value)
                {
                    _Value = value;

                    StateValueChanged?.Invoke(this);
                }
            }
        }

        public static event Action<GlobalState> StateValueChanged;
    }
}
