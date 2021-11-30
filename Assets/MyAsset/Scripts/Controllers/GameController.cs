using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace RollABollGame
{
    public sealed class GameController : MonoBehaviour
    {
        private bool IsGameLoad;
        [SerializeField] private ObjectInteractive _maze;
        [SerializeField] private ObjectInteractive _roof;
        [SerializeField] private GameMenu _gameMenu;

        private Door[] _doors;
        private List<int> _keysPlayer;

        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioMixerGroup _soundMixer;

        private List<ObjectInteractive> _objectsInteractive;

        private List<IExecute> _objectsExecute;
        private List<IFixExecute> _objectsFixExecute;

        private CameraController _cameraController;
        private InputController _inputController;

        private Score _score;

        public delegate void CoinMaxUp();
        public event CoinMaxUp coinMaxUpEvent;

        private SaveDataRepository _saveDataRepository;
        private SettingsDataRepository _settingsDataRepository;

        private PlayerBall _player;

        private void Awake()
        {
            Reference reference = new Reference();
            _player = reference.PlayerBall;
            _player.playerJumpEvent += SFXCreate;
            _keysPlayer = new List<int>();
            _saveDataRepository = new SaveDataRepository();
            _score = new Score();
            coinMaxUpEvent += _score.ScoreMaxUp;
            Physics.autoSimulation = true;
            Cursor.visible = false;

            ObjectInteractive[] objectsInteractive = FindObjectsOfType<ObjectInteractive>();

            _objectsInteractive = new List<ObjectInteractive>();
            _objectsExecute = new List<IExecute>();
            _objectsFixExecute = new List<IFixExecute>();

            _doors = FindObjectsOfType<Door>();

            foreach (var obj in objectsInteractive)
            {
                _objectsInteractive.Add(obj);
                obj.destroyObjectInteractiveEvent += ObjectDestroy;

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
                    coin.playerTakeCoinEvent += PlayerTakeCoin;
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
                if (obj is Trap trap)
                {
                    trap.playerInTrapEvent += SFXCreate;
                }
                if (obj is Luke luke)
                {
                    luke.playerInLukeEvent += SFXCreate;
                }
                if (obj is Key key)
                {
                    key.playerTakeKeyEvent += PlayerTakeKey;
                }
                if (obj is EndGame endGame)
                {
                    endGame.gameOverEvent += GameOver;
                }
                if (obj is Lift lift)
                {
                    lift.playerInLiftEvent += SFXCreate;
                }
            }
            _cameraController = new CameraController(_player.transform, reference.MainCamera.transform);
            _objectsExecute.Add(_cameraController);
            _inputController = new InputController(_player, reference.MainCamera.transform);
            _inputController.menuActionEvent += _gameMenu.MenuAction;
            _objectsExecute.Add(_inputController);
            _objectsFixExecute.Add(_inputController);
            _gameMenu.sliderSensitivity.onValueChanged.AddListener(_cameraController.SensChanged);
            _gameMenu.sliderMusic.onValueChanged.AddListener(VolumeMusic);
            _gameMenu.sliderSound.onValueChanged.AddListener(VolumeSound);
            _gameMenu.pauseGameEvent += _inputController.GamePause;
            _gameMenu.pauseGameEvent += _cameraController.GamePause;
            _gameMenu.saveGameEvent += SaveGame;
            _gameMenu.loadGameEvent += LoadGame;
            _gameMenu.pauseGameEvent += Pause;
            _gameMenu.resetGameEvent += ResetGame;
            _gameMenu.buttonMainMenu.onClick.AddListener(OnMainMenu);
            _gameMenu.buttonEndMainMenu.onClick.AddListener(OnMainMenu);
            _gameMenu.loadSettingsEvent += LoadSetting;
            _gameMenu.saveSettingsEvent += SaveSettings;
        }
        private void Start()
        {
            LoadSetting();
            if (IsGameLoad)
            {
                LoadingGame();
                ScoreUpdate();
                if (_player.transform.position.y > 6)
                {
                    _roof.transform.Find("Body").transform.gameObject.SetActive(true);
                }
                else if (_player.transform.position.y < 0)
                {
                    _maze.transform.Find("Body").transform.gameObject.SetActive(false);
                }
            }
        }
        private void LoadSetting()
        {
            float _music, _sound, _sens;
            _settingsDataRepository = new SettingsDataRepository();
            _settingsDataRepository.LoadSettings(out IsGameLoad, out _music, out _sound, out _sens);
            _audioMixer.SetFloat("Music", _music);
            _audioMixer.SetFloat("Sound", _sound);
            _cameraController.SensChanged(_sens);
            _gameMenu.sliderSensitivity.value = _sens;
            _gameMenu.sliderMusic.value = _music;
            _gameMenu.sliderSound.value = _sound;
        }
        private void SaveSettings()
        {
            _settingsDataRepository.SaveSettings(IsGameLoad, _audioMixer, _cameraController.GetSens());
            LoadSetting();
        }
        private void VolumeMusic(float sliderValue)
        {
            _audioMixer.SetFloat("Music", sliderValue);
        }
        private void VolumeSound(float sliderValue)
        {
            _audioMixer.SetFloat("Sound", sliderValue);
        }
        public void ScoreUpdate()
        {
            _gameMenu.ScoreUpdate(_score.DisplayScore());
        }
        public void OnMainMenu()
        {
            Pause(false);
            SceneManager.LoadScene(0);
        }
        private void ResetGame()
        {
            IsGameLoad = false;
            SaveSettings();
            SceneManager.LoadScene(1);
        }
        private void SaveGame()
        {
            List<Coin> _coinList = new List<Coin>();
            foreach (var obj in _objectsInteractive)
            {
                if (obj is Coin coin)
                {
                    _coinList.Add(coin);
                }
            }
            _saveDataRepository.Save(_player, _score, _coinList, _keysPlayer);
        }
        private void LoadGame()
        {
            IsGameLoad = true;
            SaveSettings();
            SceneManager.LoadScene(1);
        }
        void LoadingGame()
        {
            List<Coin> _coinList = new List<Coin>();
            List<Key> _keyList = new List<Key>();
            foreach (var obj in _objectsInteractive)
            {
                if (obj is Coin coin)
                {
                    _coinList.Add(coin);
                }
                if (obj is Key key)
                {
                    _keyList.Add(key);
                }
            }
            _saveDataRepository.Load(_player, _score, _coinList, _keyList, _doors, _keysPlayer);
        }
        private void Pause(bool value)
        {
            Physics.autoSimulation = !value;
        }
        private void GameOver(Vector3 position, AudioClip clip)
        {
            SFXCreate(position, clip);
            _gameMenu.OnEnd(_score.DisplayEndScore());
        }
        private IEnumerator PlaySFX(SFX sound)
        {
            sound.source.Play();
            yield return new WaitForSeconds(sound.source.clip.length);
            Destroy(sound.gameObject);
            sound = null;
        }
        private void SFXCreate(Vector3 position, AudioClip clip)
        {
            SFX _sound = new SFX(position, clip, _soundMixer);
            StartCoroutine(PlaySFX(_sound));
        }
        private void PlayerTakeCoin(Vector3 position, AudioClip clip)
        {
            _score.ScoreUp();
            ScoreUpdate();
            SFXCreate(position, clip);
        }
        private void PlayerInWater(bool value, Vector3 position, AudioClip clip)
        {
            _inputController.Water(value);
            SFXCreate(position, clip);
        }
        private void PlayerTakeBoost(Vector3 vector, Vector3 position, AudioClip clip)
        {
            _inputController.Boost(vector);
            SFXCreate(position, clip);
        }
        private void PlayerTakeKey(int value, Vector3 position, AudioClip clip)
        {
            _keysPlayer.Add(value);
            SFXCreate(position, clip);
            foreach (Door door in _doors)
            {
                door.OpenDoor(value);
            }
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
        public void Dispose()
        {
            foreach (var obj in _objectsInteractive)
            {
                if (obj is Coin coin)
                {
                    coin.playerTakeCoinEvent -= PlayerTakeCoin;
                }
                if (obj is Water water)
                {
                    water.playerInWaterEvent += PlayerInWater;
                }
                if (obj is Boost boost)
                {
                    boost.playerTakeBoostEvent -= PlayerTakeBoost;
                }
                if (obj is Trap trap)
                {
                    trap.playerInTrapEvent -= SFXCreate;
                }
                if (obj is Luke luke)
                {
                    luke.playerInLukeEvent -= SFXCreate;
                }
                if (obj is Key key)
                {
                    key.playerTakeKeyEvent -= PlayerTakeKey;
                }
                if (obj is EndGame endGame)
                {
                    endGame.gameOverEvent -= GameOver;
                }
                if (obj is Lift lift)
                {
                    lift.playerInLiftEvent -= SFXCreate;
                }
            }
        }
    }
}