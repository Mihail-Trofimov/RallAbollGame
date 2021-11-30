using System.Data;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RollABollGame
{
    public class GameMenu: MonoBehaviour
    {
        private bool _main = false;
        private bool _menu = false;
        private bool _settings = false;
        private bool _score = true;
        private bool _end = false;

        [SerializeField] private GameObject _panelMain;
        [SerializeField] private GameObject _panelMenu;
        [SerializeField] private GameObject _panelSettings;
        [SerializeField] private GameObject _panelScore;
        [SerializeField] private GameObject _panelEnd;

        [SerializeField] private Button _buttonInGame;
        [SerializeField] private Button _buttonSave;
        [SerializeField] private Button _buttonLoad;
        [SerializeField] private Button _buttonReset;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonSettingsCancel;
        [SerializeField] private Button _buttonSettingsApply;
        public Button buttonMainMenu;

        [SerializeField] private Button _buttonEndReset;
        public Button buttonEndMainMenu;

        public Slider sliderMusic;
        public Slider sliderSound;
        public Slider sliderSensitivity;

        [SerializeField] private AudioSource _soundClick;

        [SerializeField] private Text _textScore;
        [SerializeField] private Text _textEndScore;

        public delegate void PauseGame(bool value);
        public event PauseGame pauseGameEvent;

        public delegate void SaveGame();
        public event SaveGame saveGameEvent;

        public delegate void LoadGame();
        public event LoadGame loadGameEvent;

        public delegate void ResetGame();
        public event ResetGame resetGameEvent;

        public delegate void SaveSettings();
        public event SaveSettings saveSettingsEvent;

        public delegate void LoadSettings();
        public event LoadSettings loadSettingsEvent;

        public void Awake()
        {
            Exist(_panelMain);
            Exist(_panelMenu);
            Exist(_panelSettings);
            Exist(_panelScore);
            Exist(_panelEnd);
            Exist(sliderSensitivity);
            Exist(sliderMusic);
            Exist(sliderSound);
            Exist(_soundClick);
            Exist(_textScore);
            Exist(_textEndScore);
            Exist(buttonMainMenu);
            Exist(buttonEndMainMenu);

            if (Exist(_buttonInGame))
                _buttonInGame.onClick.AddListener(OnGame);
            if (Exist(_buttonReset))
                _buttonReset.onClick.AddListener(OnReset);
            if (Exist(_buttonReset))
                _buttonSave.onClick.AddListener(OnSave);
            if (Exist(_buttonLoad))
                _buttonLoad.onClick.AddListener(OnLoad);
            if (Exist(_buttonSettings))
                _buttonSettings.onClick.AddListener(OnSettings);
            if (Exist(_buttonSettingsApply))
                _buttonSettingsApply.onClick.AddListener(OnSaveSettings);
            if (Exist(_buttonSettingsCancel))
                _buttonSettingsCancel.onClick.AddListener(OnLoadSettings);
            if (Exist(_buttonEndReset))
                _buttonEndReset.onClick.AddListener(OnReset);

            Cursor.visible = false;
            _panelMain.SetActive(_main);
            _panelMenu.SetActive(_menu);
            _panelSettings.SetActive(_settings);
            _panelScore.SetActive(_score);
            _panelEnd.SetActive(_end);
        }
        private void MenuActive()
        {
            _panelMain.SetActive(_main);
            _panelMenu.SetActive(_menu);
            _panelSettings.SetActive(_settings);
            _panelScore.SetActive(_score);
            _panelEnd.SetActive(_end);
            _soundClick.Play();
        }
        public void MenuAction()
        {
            if (!_end)
            {
                if (!_menu || _settings)
                {
                    OnMenu();
                    if (_settings)
                        loadSettingsEvent?.Invoke();
                }
                else
                    OnGame();
            }
        }
        private bool Exist<T>(T value)
        {
            if (value == null)
                throw new DataException(nameof(value) + " not found");
            return true;
        }
        public void ScoreUpdate(string text)
        {
            _textScore.text = text;
        }
        public void OnSave()
        {
            saveGameEvent?.Invoke();
        }
        public void OnLoad()
        {
            loadGameEvent?.Invoke();
        }
        public void OnEnd(string endText)
        {
            pauseGameEvent?.Invoke(true);
            _settings = false;
            _score = false;
            _menu = false;
            _end = true;
            _textEndScore.text = endText;
            MenuActive();
            Cursor.visible = true;
        }
        public void OnMenu()
        {
            pauseGameEvent?.Invoke(true);
            _settings = false;
            _score = false;
            _main = true;
            _menu = true;
            MenuActive();
            Cursor.visible = true;
        }
        public void OnGame()
        {
            pauseGameEvent?.Invoke(false);
            Cursor.visible = false;
            _settings = false;
            _main = false;
            _menu = false;
            _score = true;
            MenuActive();
        }
        public void OnReset()
        {
            pauseGameEvent?.Invoke(false);
            resetGameEvent?.Invoke();
        }
        public void OnLoadSettings()
        {
            loadSettingsEvent?.Invoke();
            OnMenu();
        }
        public void OnSettings()
        {
            _score = false;
            _menu = false;
            _main = true;
            _settings = true;
            MenuActive();
        }
        public void OnSaveSettings()
        {
            saveSettingsEvent?.Invoke();
            OnMenu();
        }

    }
}