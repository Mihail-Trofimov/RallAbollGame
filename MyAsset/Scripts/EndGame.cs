using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollABollGame
{
    public sealed class EndGame : ObjectInteractive
    {
        public delegate void GameOver();
        public event GameOver gameOverEvent;
        private void Awake()
        {
            IsDestroyable = false;
        }
        protected override void Interaction()
        {
            gameOverEvent?.Invoke();
        }
    }
}