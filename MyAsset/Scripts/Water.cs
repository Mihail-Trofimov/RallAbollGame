using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollABollGame
{ 
    public sealed class Water : ObjectAftermath
    {
        public delegate void PlayerInWater(bool value);
        public event PlayerInWater playerInWaterEvent;
        protected override void Interaction()
        {
            playerInWaterEvent?.Invoke(true);
        }
        protected override void Aftermath()
        {
            playerInWaterEvent?.Invoke(false);
        }
    }
}