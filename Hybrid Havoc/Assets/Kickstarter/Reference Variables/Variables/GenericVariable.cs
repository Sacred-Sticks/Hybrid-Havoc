using System;
using Kickstarter.Attributes;
using UnityEngine;

namespace Kickstarter.Variables
{
    public class GenericVariable<TDataType> : GenericVariable
    {
        [SerializeField] private TDataType value;
        [SerializeField] private bool resetValue;
        [ConditionalHide("resetValue", true)]
        [SerializeField] private TDataType initialValue;

        public Action<TDataType> ValueChanged;
        
        public TDataType Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                ValueChanged?.Invoke(Value);
            }
        }

        private void OnEnable()
        {
            if (resetValue)
                value = initialValue;
        }
    }

    public class GenericVariable : ScriptableObject
    {
    }
}