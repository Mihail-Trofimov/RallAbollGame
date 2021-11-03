using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollABollGame
{
    public class Reference
    {
        private PlayerBall _playerBall;
        private Camera _mainCamera;
        public PlayerBall PlayerBall
        {
            get
            {
                if (_playerBall == null)
                {
                    var gameObject = Resources.Load<PlayerBall>("Player");
                    _playerBall = Object.Instantiate(gameObject);
                }
                return _playerBall;
            }
        }
        public Camera MainCamera
        {
            get
            {
                if (_mainCamera == null)
                {
                    _mainCamera = Camera.main;
                }
                return _mainCamera;
            }
        }
    }
}

