using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollABollGame
{ 
    public sealed class Water : ObjectAftermath
    {
        [SerializeField] private AudioClip _soundIn;
        [SerializeField] private AudioClip _soundOut;

        public delegate void PlayerInWater(bool value, Vector3 position, AudioClip clip);
        public event PlayerInWater playerInWaterEvent;
        protected override void Interaction()
        {
            playerInWaterEvent?.Invoke(true, transform.position, _soundIn);
        }
        protected override void Aftermath()
        {
            playerInWaterEvent?.Invoke(false, transform.position, _soundOut);
        }
    }
}