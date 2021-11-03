using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollABollGame
{
    public abstract class ObjectAftermath : ObjectInteractive
    {
        public bool IsAftermath { get; } = true;
        private void Awake()
        {
            IsDestroyable = false;
        }
        protected abstract void Aftermath();
        private void OnTriggerExit(Collider other)
        {
            if (IsAftermath && other.CompareTag("Player"))
            {
                Aftermath();
            }
        }
    }
}
