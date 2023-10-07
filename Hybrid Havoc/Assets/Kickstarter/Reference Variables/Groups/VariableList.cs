using System.Collections.Generic;
using Kickstarter.Variables;
using UnityEngine;

namespace Kickstarter.Groups
{
    [CreateAssetMenu(fileName = "Variable List", menuName = "Kickstarter/Variables/Groups/List")]
    public class VariableList : ScriptableObject
    {
        [SerializeField] private List<GenericVariable> variables;

        public List<GenericVariable> Variables
        {
            get
            {
                return variables;
            }
        }
    }
}
