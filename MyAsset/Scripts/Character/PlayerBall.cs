using UnityEngine;

namespace RollABollGame
{
    public sealed class PlayerBall : PlayerBase
    {
        private Rigidbody _rigidbody;
        private float _maxAngularVelocity = 50f;
        private float _groundRayLength = 1f;
        private float _boostPower = 24f;
        private float _jumpPower = 6f;
        private float _fine = 1f;

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
                Debug.Log("Jump");
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
                Debug.Log("Water " + value);
            }
            else
            {
                _fine = 1f;
                Debug.Log("Water " + value);
            }
        }
    }
}