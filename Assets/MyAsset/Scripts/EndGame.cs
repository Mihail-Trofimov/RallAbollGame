using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollABollGame
{
    public sealed class EndGame : ObjectInteractive
    {
        [SerializeField] private AudioClip _sound;

        public delegate void Action(Vector3 position, AudioClip clip);
        public event Action gameOverEvent;

        private void Awake()
        {
            IsDestroyable = false;
        }
        protected override void Interaction()
        {
            gameOverEvent?.Invoke(transform.position, _sound);
        }
    }
}