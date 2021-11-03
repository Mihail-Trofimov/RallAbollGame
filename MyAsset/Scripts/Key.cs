using UnityEngine;

namespace RollABollGame
{
    public sealed class Key : ObjectInteractive, IRotation, IExecute
    {
        private Transform _body;
        public enum Index
        {
            white = 0,
            purple = 1,
            blue = 2,
            yellow = 3,
            orange = 4
        };
        public Index keyColor = 0;

        public delegate void PlayerTakeKey();
        public event PlayerTakeKey playerTakeKeyEvent;

        private void Awake()
        {
            _body = transform.Find("Body").transform;
        }
        public void Execute()
        {
            Rotation();
        }
        public void Rotation()
        {
            _body.transform.Rotate(0, 100.0f * Time.deltaTime, 0);
        }
        protected override void Interaction()
        {
            playerTakeKeyEvent?.Invoke();
        }
    }
}
