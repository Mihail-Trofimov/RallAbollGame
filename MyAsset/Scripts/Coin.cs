using System.Data;
using UnityEngine;

namespace RollABollGame
{
    public sealed class Coin : ObjectInteractive, IRotation, IExecute
    {
        private Transform _body;
        public delegate void PlayerTakeCoin();
        public event PlayerTakeCoin playerTakeCoinEvent;
        private void Awake()
        {
            if (transform.Find("Body").transform)
            {
                _body = transform.Find("Body").transform;
            }
            else
            {
                throw new DataException(nameof(_body) + " not found");
            }
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
            playerTakeCoinEvent?.Invoke();
        }
    }
}