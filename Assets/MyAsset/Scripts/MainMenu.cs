using System.Data;
using UnityEngine;
using UnityEngine.UI;

namespace RollABollGame
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _panelMenu;
        [SerializeField] private GameObject _panelSettings;
        [SerializeField] private GameObject _panelAbout;

        public Button buttonContinue;
        public Button buttonNewGame;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonAbout;
        public Button buttonExit;
        public Button buttonSettingsApply;
        public Button buttonSettingsCancel;
        [SerializeField] private Button _buttonAboutBack;

        public Slider sliderMusic;
        public Slider sliderSound;
        public Slider sliderSensitivity;

        [SerializeField] private AudioSource _soundClick;

        private bool _menu = true;
        private bool _about = false;
        private bool _settings = false;

        private void Awake()
        {
            Exist(_panelMenu);
            Exist(_panelSettings);
            Exist(_panelAbout);
            Exist(sliderMusic);
            Exist(sliderSound);
            Exist(sliderSensitivity);
            Exist(buttonContinue);
            Exist(buttonNewGame);
            Exist(buttonExit);

            if (Exist(buttonSettingsApply))
                buttonSettingsApply.onClick.AddListener(OnMenu);
            if (Exist(buttonSettingsCancel))
                buttonSettingsCancel.onClick.AddListener(OnMenu);
            if (Exist(_buttonSettings))
                _buttonSettings.onClick.AddListener(OnSettings);
            if(Exist(_buttonAbout))
                _buttonAbout.onClick.AddListener(OnAbout);
            if (Exist(_buttonAboutBack))
                _buttonAboutBack.onClick.AddListener(OnMenu);

            _panelMenu.SetActive(_menu);
            _panelSettings.SetActive(_settings);
            _panelAbout.SetActive(_about);
            Cursor.visible = true;
        }
        private bool Exist <T> (T value)
        {
            if (value == null)
                throw new DataException(nameof(value) + " not found");
            return true;
        }
        public void MenuCancelAction()
        {
            if (!_menu)
            {
                if (_settings)
                {
                    buttonSettingsCancel.onClick?.Invoke();
                    OnMenu();
                }
                _menu = true;
                _about = false;
                _settings = false;
                MenuAction();
            }
        }
        private void MenuAction()
        {
            _panelMenu.SetActive(_menu);
            _panelSettings.SetActive(_settings);
            _panelAbout.SetActive(_about);
            _soundClick.Play();
        }
        public void OnSettings()
        {
            _menu = false;
            _about = false;
            _settings = true;
            MenuAction();
        }
        public void OnAbout()
        {
            _menu = false;
            _about = true;
            _settings = false;
            MenuAction();
        }
        public void OnMenu()
        {
            _menu = true;
            _about = false;
            _settings = false;
            MenuAction();
        }
    }
}
