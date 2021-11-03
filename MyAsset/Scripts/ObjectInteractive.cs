using UnityEngine;

namespace RollABollGame
{
    public abstract class ObjectInteractive : MonoBehaviour
    {
        private bool _isInteractable = true;
        protected bool IsInteractable
        {
            get { return _isInteractable; }
            private set
            {
                _isInteractable = value;
            }
        }
        public bool IsDestroyable = true;
        protected abstract void Interaction();
        public delegate void DestroyObjectInteractive(ObjectInteractive obj);
        public event DestroyObjectInteractive destroyObjectInteractiveEvent;
        private void OnTriggerEnter(Collider other)
        {
            if (_isInteractable && other.CompareTag("Player"))
            {
                Interaction();
                if (IsDestroyable)
                {
                    destroyObjectInteractiveEvent?.Invoke(this);
                    Destroy(gameObject);
                    Destroy(this);
                }
            }
        }
    }
}