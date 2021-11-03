using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace RollABollGame
{
    public sealed class GameController : MonoBehaviour
    {
        [SerializeField] private GameMenu _gameMenu;
        Door[] _doors;

        private List<ObjectInteractive> _objectsInteractive;

        private List<IExecute> _objectsExecute;
        private List<IFixExecute> _objectsFixExecute;

        CameraController _cameraController;
        InputController _inputController;

        Score _score;

        public delegate void CoinMaxUp();
        public event CoinMaxUp coinMaxUpEvent;

        private void Awake()
        {
            _score = new Score(_gameMenu._textScore);
            coinMaxUpEvent += _score.ScoreMaxUp;

            Physics.autoSimulation = true;
            Cursor.visible = false;

            ObjectInteractive[] objectsInteractive = FindObjectsOfType<ObjectInteractive>();

            _objectsInteractive = new List<ObjectInteractive>();
            _objectsExecute = new List<IExecute>();
            _objectsFixExecute = new List<IFixExecute>();

            foreach (var obj in objectsInteractive)
            {
                _objectsInteractive.Add(obj);
                obj.destroyObjectInteractiveEvent += ObjectDestroy;
            }
            _doors = FindObjectsOfType<Door>();

            foreach (var obj in _objectsInteractive)
            {
                
                if (obj is IExecute execute)
                {
                    _objectsExecute.Add(execute);
                }
                if (obj is IFixExecute fix)
                {
                    _objectsFixExecute.Add(fix);
                }
                if (obj is Coin coin)
                {
                    coin.playerTakeCoinEvent += _score.ScoreUp;
                    coinMaxUpEvent.Invoke();
                }
                if (obj is Water water)
                {
                    water.playerInWaterEvent += PlayerInWater;
                }
                if (obj is Boost boost)
                {
                    boost.playerTakeBoostEvent += PlayerTakeBoost;
                }
               
                if (obj is Key key)
                {
                    foreach (Door door in _doors)
                    {
                        if (Convert.ToInt32(key.keyColor) == Convert.ToInt32(door.doorColor))
                        {
                            key.playerTakeKeyEvent += door.OpenDoor;
                        }
                    }
                }
                if (obj is EndGame endGame)
                {
                    endGame.gameOverEvent += GameOver;
                }
            }
            


            Reference reference = new Reference();

            _cameraController = new CameraController(reference.PlayerBall.transform, reference.MainCamera.transform);
            _objectsExecute.Add(_cameraController);
            _gameMenu.pauseGameEvent += _cameraController.GamePause;

            _inputController = new InputController(reference.PlayerBall, reference.MainCamera.transform);
            _inputController.menuActionEvent += _gameMenu.MenuAction;
            _objectsExecute.Add(_inputController);
            _objectsFixExecute.Add(_inputController);
            _gameMenu.pauseGameEvent += _inputController.GamePause;
            _gameMenu.pauseGameEvent += Pause;
        }
        private void Pause(bool value)
        {
            Physics.autoSimulation = !value;
        }
        private void GameOver()
        {
            _gameMenu.OnEnd(_score.DisplayEndScore());
        }
        private void PlayerInWater(bool value)
        {
            _inputController.Water(value);
        }
        private void PlayerTakeBoost(Vector3 vector)
        {
            _inputController.Boost(vector);
        }

        private void ObjectDestroy(ObjectInteractive obj)
        {
            if (obj is IExecute execute)
            {
                _objectsExecute.Remove(execute);
            }
            if (obj is IFixExecute fix)
            {
                _objectsFixExecute.Remove(fix);
            }
            _objectsInteractive.Remove(obj);
        }

        private void Update()
        {
            foreach (var execute in _objectsExecute)
            {
                if (execute == null)
                {
                    _objectsExecute.Remove(execute);
                }
                else
                {
                    execute.Execute();
                }
            }
        }
        private void FixedUpdate()
        {
            foreach (var fix in _objectsFixExecute)
            {
                if (fix == null)
                {
                    _objectsFixExecute.Remove(fix);
                }
                else
                {
                    fix.FixExecute();
                }
            }
        }

        public void Dispose()
        {
            foreach (var obj in _objectsInteractive)
            {
                if (obj is Coin coin)
                {
                    coin.playerTakeCoinEvent -= _score.ScoreUp;
                }
                if (obj is Water water)
                {
                    water.playerInWaterEvent -= PlayerInWater;
                }
                if (obj is Boost boost)
                {
                    boost.playerTakeBoostEvent -= PlayerTakeBoost;
                }
                if (obj is Key key)
                {
                    foreach (Door door in _doors)
                    {
                        if (Convert.ToInt32(key.keyColor) == Convert.ToInt32(door.doorColor))
                        {
                            key.playerTakeKeyEvent -= door.OpenDoor;
                        }
                    }
                }
            }
        }
    }
}