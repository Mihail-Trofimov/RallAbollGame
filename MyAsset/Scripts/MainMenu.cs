using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RollABollGame
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _panelMenu;
        [SerializeField] private GameObject _panelSettings;
        [SerializeField] private GameObject _panelAbout;

        [SerializeField] private Button _buttonGame;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonAbout;
        [SerializeField] private Button _buttonExit;

        private bool _menu = true;
        private bool _about = false;
        private bool _settings = false;

        private void Awake()
        {
            Exist(_panelMenu);
            Exist(_panelSettings);
            Exist(_panelAbout);
            if(Exist(_buttonGame))
                _buttonGame.onClick.AddListener(OnGame);
            if(Exist(_buttonSettings))
                _buttonSettings.onClick.AddListener(OnSettings);
            if(Exist(_buttonAbout))
                _buttonAbout.onClick.AddListener(OnAbout);
            if(Exist(_buttonExit))
                _buttonExit.onClick.AddListener(OnExit);

            Cursor.visible = true;
            MenuActive();
        }
        private bool Exist <T> (T value)
        {
            if (value == null)
                throw new DataException(nameof(value) + " not found");
            return true;
        }
        private void Update()
        {
            if (Input.GetButtonDown("Cancel") && !_menu)
            {
                _menu = true;
                _about = false;
                _settings = false;
                MenuActive();
            }
        }
        private void MenuActive()
        {
            _panelMenu.SetActive(_menu);
            _panelSettings.SetActive(_settings);
            _panelAbout.SetActive(_about);
        }
        public void OnGame()
        {
            SceneManager.LoadScene(1);
        }
        public void OnSettings()
        {
            _menu = false;
            _about = false;
            _settings = true;
            MenuActive();
        }
        public void OnAbout()
        {
            _menu = false;
            _about = true;
            _settings = false;
            MenuActive();
        }
        public void OnExit()
        {
            Application.Quit();
        }
    }
}
