using UnityEngine;

namespace RollABollGame
{
    public sealed class InputController : IFixExecute, IExecute
    {
        private readonly PlayerBase _playerBase;
        private readonly Transform _cam;
        private Vector3 _moveDirection;
        private Vector3 _camForward;

        public delegate void MenuAction();
        public event MenuAction menuActionEvent;

        private bool _pause = false;
        public InputController(PlayerBase player, Transform camera)
        {
            _playerBase = player;
            _cam = camera;
        }
        public void GamePause (bool value)
        {
            _pause = value;
        }
        public void Execute()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                menuActionEvent.Invoke();
            }
            if (!_pause)
            {
                if (Input.GetButton("Jump"))
                {
                    _playerBase.Jump();
                }
                _camForward = Vector3.Scale(_cam.up, new Vector3(1, 0, 1)).normalized;
                _moveDirection = (Input.GetAxis("Horizontal") * _cam.right + _camForward * Input.GetAxis("Vertical")).normalized;
            }
        }
        public void FixExecute()
        {
            _playerBase.Move(_moveDirection);
        }
        public void Boost(Vector3 vector)
        {
            _playerBase.Boost(vector);
        }
        public void Water(bool value)
        {
            _playerBase.Water(value);
        }
    }
}