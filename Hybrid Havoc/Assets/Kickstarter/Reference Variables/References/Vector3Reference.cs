using System;
using System.Runtime.CompilerServices;
using Kickstarter.Variables;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kickstarter.References
{
    [Serializable]
    public class Vector3Reference
    {
        [SerializeField] private bool useConstant = true;
        [SerializeField] private Vector3 constantValue;
        [SerializeField] private Vector3Variable variable;

        public Vector3 Value
        {
            get
            {
                return useConstant ? constantValue : variable.Value;
            }
            set
            {
                if (useConstant)
                {
                    constantValue = value;
                    return;
                }
                variable.Value = value;
            }
        }

        public Vector3Variable Variable
        {
            get
            {
                return variable;
            }
        }
    }
}
