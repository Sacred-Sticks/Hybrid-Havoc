using System;
using Kickstarter.Identification;
using UnityEngine;

namespace Kickstarter.Progression.Types
{
    [RequireComponent(typeof(Player))]
    public class StationRegister : MonoBehaviour
    {
        public Action<Transform> SaveStationActivated;

        public void ActivateSave(Transform savePoint)
        {
            SaveStationActivated?.Invoke(savePoint);
        }
    }
}
