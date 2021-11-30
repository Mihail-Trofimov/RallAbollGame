using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace RollABollGame
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private AudioMixer _audioMixer;

        private SettingsDataRepository _settingsDataRepository;

        public delegate void Action();
        public event Action menuCancelEvent;

        private bool _isGameLoad;
        private float _music;
        private float _sound;
        private float _sensitivity;

        private void Awake()
        {
            _mainMenu.buttonContinue.onClick.AddListener(OnLoadGame);
            _mainMenu.buttonNewGame.onClick.AddListener(OnNewGame);
            _mainMenu.buttonSettingsApply.onClick.AddListener(OnSaveSettings);
            _mainMenu.buttonSettingsCancel.onClick.AddListener(OnLoadSettings);
            _mainMenu.buttonExit.onClick.AddListener(OnExit);
            _mainMenu.sliderMusic.onValueChanged.AddListener(VolumeMusic);
            _mainMenu.sliderSound.onValueChanged.AddListener(VolumeSound);
            _mainMenu.sliderSensitivity.onValueChanged.AddListener(VolumeSensitivity);
            menuCancelEvent += _mainMenu.MenuCancelAction;
            SaveDataRepository _saveFile = new SaveDataRepository();
            if (!_saveFile.ExistFile())
            {
                _mainMenu.buttonContinue.gameObject.SetActive(false);
            }
            _settingsDataRepository = new SettingsDataRepository();
        }
        private void Start()
        {
            OnLoadSettings();
        }
        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
                menuCancelEvent?.Invoke();
        }
        private void OnLoadGame()
        {
            _isGameLoad = true;
            _settingsDataRepository.SaveSettings(_isGameLoad, _music, _sound, _sensitivity);
            SceneManager.LoadScene(1);
        }
        private void OnNewGame()
        {
            _isGameLoad = false;
            _settingsDataRepository.SaveSettings(_isGameLoad, _music, _sound, _sensitivity);
            SceneManager.LoadScene(1);
        }
        public void OnSaveSettings()
        {
            _settingsDataRepository.SaveSettings(_isGameLoad, _music, _sound, _sensitivity);
        }
        private void OnLoadSettings()
        {
            _settingsDataRepository.LoadSettings(out _isGameLoad, out _music, out _sound, out _sensitivity);
            SetSettings();
        }
        private void SetSettings()
        {
            _audioMixer.SetFloat("Music", _music);
            _audioMixer.SetFloat("Sound", _sound);
            _mainMenu.sliderMusic.value = _music;
            _mainMenu.sliderSound.value = _sound;
            _mainMenu.sliderSensitivity.value = _sensitivity;
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
        public void OnExit()
        {
            Application.Quit();
        }
    }
}