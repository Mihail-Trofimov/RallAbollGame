using System;
using UnityEngine;

namespace RollABollGame
{
    public sealed class Key : ObjectInteractive, IRotation, IExecute
    {
        [SerializeField] private AudioClip _sound;

        private Transform _body;
        public enum IndexColor
        {
            white = 0,
            purple = 1,
            blue = 2,
            yellow = 3,
            orange = 4
        };
        public IndexColor keyColor = 0;

        public delegate void Action(int value, Vector3 position, AudioClip clip);
        public event Action playerTakeKeyEvent;

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
            playerTakeKeyEvent?.Invoke(Convert.ToInt32(keyColor), transform.position, _sound);
        }
    }
}
