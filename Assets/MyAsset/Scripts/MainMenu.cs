using System.Data;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RollABollGame
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _panelMenu;
        [SerializeField] private GameObject _panelSettings;
        [SerializeField] private GameObject _panelAbout;

        [SerializeField] private Button _buttonContinue;
        [SerializeField] private Button _buttonNewGame;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonAbout;
        [SerializeField] private Button _buttonExit;
        [SerializeField] private Button _buttonSettingsApply;
        [SerializeField] private Button _buttonSettingsCancel;
        [SerializeField] private Button _buttonAboutBack;

        [SerializeField] private Slider _sliderMusic;
        [SerializeField] private Slider _sliderSound;
        [SerializeField] private Slider _sliderSensitivity;

        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioSource _soundClick;


        private bool _menu = true;
        private bool _about = false;
        private bool _settings = false;

        private bool _isGameLoad;
        private float _music;
        private float _sound;
        private float _sensitivity;

        SettingsDataRepository _settingsDataRepository;

        private void Awake()
        {
            Exist(_panelMenu);
            Exist(_panelSettings);
            Exist(_panelAbout);
            Exist(_sliderMusic);
            Exist(_sliderSound);
            Exist(_sliderSensitivity);
            Exist(_audioMixer);
            Exist(_soundClick);

            if (Exist(_buttonContinue))
                _buttonContinue.onClick.AddListener(OnLoadGame);
            if (Exist(_buttonNewGame))
                _buttonNewGame.onClick.AddListener(OnNewGame);
            if (Exist(_buttonSettings))
                _buttonSettings.onClick.AddListener(OnSettings);
            if(Exist(_buttonAbout))
                _buttonAbout.onClick.AddListener(OnAbout);
            if(Exist(_buttonExit))
                _buttonExit.onClick.AddListener(OnExit);
            if (Exist(_buttonSettingsApply))
                _buttonSettingsApply.onClick.AddListener(OnSaveSettings);
            if (Exist(_buttonSettingsCancel))
                _buttonSettingsCancel.onClick.AddListener(OnLoadSettings);
            if (Exist(_buttonAboutBack))
                _buttonAboutBack.onClick.AddListener(OnMenu);
            if (Exist(_sliderMusic))
                _sliderMusic.onValueChanged.AddListener(VolumeMusic);
            if (Exist(_sliderSound))
                _sliderSound.onValueChanged.AddListener(VolumeSound);
            if (Exist(_sliderSensitivity))
                _sliderSensitivity.onValueChanged.AddListener(VolumeSensitivity);

            Cursor.visible = true;
            _panelMenu.SetActive(_menu);
            _panelSettings.SetActive(_settings);
            _panelAbout.SetActive(_about);
        }
        private bool Exist <T> (T value)
        {
            if (value == null)
                throw new DataException(nameof(value) + " not found");
            return true;
        }
        private void Start()
        {
            SaveDataRepository _saveFile = new SaveDataRepository();
            if (!_saveFile.ExistFile())
            {
                _buttonContinue.gameObject.SetActive(false);
            }
            _settingsDataRepository = new SettingsDataRepository();
            _settingsDataRepository.LoadSettings(out bool _isGameLoad, out float _music, out float _sound, out float _sensitivity);
            _audioMixer.SetFloat("Music", _music);
            _audioMixer.SetFloat("Sound", _sound);
            _sliderMusic.value = _music;
            _sliderSound.value = _sound;
            _sliderSensitivity.value = _sensitivity;
        }
        private void Update()
        {
            if (Input.GetButtonDown("Cancel") && !_menu)
            {
                if(_settings)
                {
                    OnLoadSettings();
                }
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
            _soundClick.Play();
        }
        public void OnLoadGame()
        {
            _isGameLoad = true;
            _settingsDataRepository.SaveSettings(_isGameLoad, _music, _sound, _sensitivity);
            SceneManager.LoadScene(1);
        }
        public void OnNewGame()
        {
            _isGameLoad = false;
            _settingsDataRepository.SaveSettings(_isGameLoad, _music, _sound, _sensitivity);
            SceneManager.LoadScene(1);
        }
        public void OnSettings()
        {
            _menu = false;
            _about = false;
            _settings = true;
            MenuActive();
        }
        public void OnLoadSettings()
        {
            _settingsDataRepository.LoadSettings(out bool _isGameLoad, out float _music, out float _sound, out float _sensitivity);
            _audioMixer.SetFloat("Music", _music);
            _audioMixer.SetFloat("Sound", _sound);
            _sliderMusic.value = _music;
            _sliderSound.value = _sound;
            _sliderSensitivity.value = _sensitivity;
            OnMenu();
        }
        public void OnSaveSettings()
        {
            _settingsDataRepository.SaveSettings(_isGameLoad, _music, _sound, _sensitivity);
            OnMenu();
        }
        public void VolumeSensitivity(float sliderValue)
        {
            _sensitivity = sliderValue;
        }
        public void VolumeMusic(float sliderValue)
        {
            _audioMixer.SetFloat("Music", sliderValue);
            _music = sliderValue;
        }
        public void VolumeSound(float sliderValue)
        {
            _audioMixer.SetFloat("Sound", sliderValue);
            _sound = sliderValue;
        }

        public void OnAbout()
        {
            _menu = false;
            _about = true;
            _settings = false;
            MenuActive();
        }
        public void OnMenu()
        {
            _menu = true;
            _about = false;
            _settings = false;
            MenuActive();
        }
        public void OnExit()
        {
            Application.Quit();
        }

    }
}
