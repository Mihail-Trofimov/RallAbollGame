using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RollABollGame
{
    public class GameMenu: MonoBehaviour
    {
        private bool _menu = false;
        private bool _settings = false;
        private bool _score = true;
        private bool _end = false;

        [SerializeField] private GameObject _panelMenu;
        [SerializeField] private GameObject _panelSettings;
        [SerializeField] private GameObject _panelScore;
        [SerializeField] private GameObject _panelEnd;

        [SerializeField] private Button _buttonInGame;
        [SerializeField] private Button _buttonReset;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonMainMenu;

        [SerializeField] private Button _buttonEndReset;
        [SerializeField] private Button _buttonEndMainMenu;

        public Text _textScore;
        [SerializeField] private Text _textEndScore;

        public delegate void PauseGame(bool value);
        public event PauseGame pauseGameEvent;

        public void Awake()
        {
            Exist(_panelMenu);
            Exist(_panelSettings);
            Exist(_panelScore);
            Exist(_panelEnd);

            if (Exist(_buttonInGame))
                _buttonInGame.onClick.AddListener(OnGame);
            if (Exist(_buttonReset))
                _buttonReset.onClick.AddListener(OnReset);
            if (Exist(_buttonSettings))
                _buttonSettings.onClick.AddListener(OnSettings);
            if (Exist(_buttonMainMenu))
                _buttonMainMenu.onClick.AddListener(OnMainMenu);
            if (Exist(_buttonEndReset))
                _buttonEndReset.onClick.AddListener(OnReset);
            if (Exist(_buttonEndMainMenu))
                _buttonEndMainMenu.onClick.AddListener(OnMainMenu);

            Cursor.visible = false;
            MenuActive();
        }
        private void MenuActive()
        {
            _panelMenu.SetActive(_menu);
            _panelSettings.SetActive(_settings);
            _panelScore.SetActive(_score);
            _panelEnd.SetActive(_end);
        }
        public void MenuAction()
        {
            if (!_end)
            {
                if (!_menu || _settings)
                {
                    OnMenu();
                }
                else
                {
                    OnGame();
                }
            }
        }
        private bool Exist<T>(T value)
        {
            if (value == null)
                throw new DataException(nameof(value) + " not found");
            return true;
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
            _menu = true;
            MenuActive();
            Cursor.visible = true;
        }
        public void OnGame()
        {
            pauseGameEvent?.Invoke(false);
            Cursor.visible = false;
            _settings = false;
            _menu = false;
            _score = true;
            MenuActive();
        }
        public void OnReset()
        {
            pauseGameEvent?.Invoke(false);
            SceneManager.LoadScene(1);
        }
        public void OnSettings()
        {
            Debug.Log("OnSettings");
        }

        public void OnMainMenu()
        {
            pauseGameEvent?.Invoke(false);
            SceneManager.LoadScene(0);
        }
    }
}