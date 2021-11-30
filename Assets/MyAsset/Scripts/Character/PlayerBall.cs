using UnityEngine;

namespace RollABollGame
{
    public sealed class PlayerBall : PlayerBase
    {
        [SerializeField] private AudioClip _jumpSound;
        private Rigidbody _rigidbody;
        private float _maxAngularVelocity = 30f;
        private float _groundRayLength = 1f;
        [SerializeField] private float _boostPower = 18f;
        [SerializeField] private float _jumpPower = 6f;
        private float _fine = 1f;

        public delegate void PlayerJump(Vector3 position, AudioClip sound);
        public event PlayerJump playerJumpEvent;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.maxAngularVelocity = _maxAngularVelocity;
        }
        public override void Move(Vector3 moveDirection)
        {
            _rigidbody.AddForce(moveDirection * Speed / _fine, ForceMode.Force);
        }
        public override void Jump()
        {
            if(Physics.Raycast(transform.position, -Vector3.up, _groundRayLength))
            {
                _rigidbody.AddForce(Vector3.up * _jumpPower / _fine, ForceMode.VelocityChange);
                playerJumpEvent?.Invoke(transform.position, _jumpSound);
            }
        }
        public override void Boost(Vector3 moveDirection)
        {
            _rigidbody.AddForce(moveDirection * _boostPower / _fine, ForceMode.VelocityChange);
        }
        public override void Water(bool value)
        {
            if (value)
            {
                _fine = Fine;
            }
            else
            {
                _fine = 1f;
            }
        }
    }
}