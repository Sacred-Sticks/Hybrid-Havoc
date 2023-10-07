using System;
using Kickstarter.Events;
using Kickstarter.Identification;
using UnityEngine;

namespace Kickstarter.Progression.Types
{
    public class QuickSaveRegister : MonoBehaviour
    {
        public Action<Transform> QuickSave;

        private Player.PlayerIdentifier playerID;

        public void Initialize(Player player, string eventKeySpecifier)
        {
            EventManager.AddListener<TriggerQuickSaveEvent>($"{player.PlayerID}.{eventKeySpecifier}", TriggerQuickSave);
        }

        private void TriggerQuickSave(TriggerQuickSaveEvent args)
        {
            QuickSave?.Invoke(args.Transform);
        }

        public class TriggerQuickSaveEvent
        {
            public Transform Transform { get; }

            public TriggerQuickSaveEvent(Transform transform)
            {
                Transform = transform;
            }
        }
    }
}
