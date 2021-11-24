using System.Data;
using UnityEngine;
using UnityEngine.UI;
namespace RollABollGame
{
    public sealed class Coin : ObjectInteractive, IRotation, IExecute
    {
        private Transform _body;
        [SerializeField] private AudioClip _sound;

        public delegate void Action(Vector3 position, AudioClip sound);
        public event Action playerTakeCoinEvent;
        

        public enum IndexFloor
        {
            Basement = 0,
            Maze = 1,
            Roof = 2
        };
        public IndexFloor coinFloor = 0;

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
            playerTakeCoinEvent?.Invoke(transform.position, _sound);
        }
    }
}