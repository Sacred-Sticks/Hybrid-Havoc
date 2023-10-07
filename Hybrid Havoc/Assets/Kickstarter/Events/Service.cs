using System;
using UnityEngine;

namespace Kickstarter.Events
{
    [CreateAssetMenu(fileName = "Game Event", menuName = "Kickstarter/Events/Service")]
    public sealed class Service : ScriptableObject
    {
        public Action<EventArgs> Event;

        public void Trigger(EventArgs parameters)
        {
            Event?.Invoke(parameters);
        }
    }
}
