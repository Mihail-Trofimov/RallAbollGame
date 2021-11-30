using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;

namespace RollABollGame
{
    public sealed class SettingsDataRepository
    {
        private readonly IData<SettingsData> _data;

        private const string _folderName = "dataSave";
        private const string _fileName = "settings.bat";
        private readonly string _path;

        public SettingsDataRepository()
        {
            _data = new SerializableXMLData<SettingsData>();
            _path = Path.Combine(Application.dataPath, _folderName);
        }
        public void SaveSettings(bool loadGame, AudioMixer audio, float sensitivity)
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }
            SettingsData saveGame = new SettingsData
            {
                LoadGame = loadGame,
                Sensitivity = sensitivity
            };
            audio.GetFloat("Music", out saveGame.MusicValue);
            audio.GetFloat("Sound", out saveGame.SoundValue);
            _data.Save(saveGame, Path.Combine(_path, _fileName));
        }
        public void SaveSettings(bool loadGame, float music, float sound, float sensitivity)
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }
            SettingsData saveGame = new SettingsData
            {
                LoadGame = loadGame,
                MusicValue = music,
                SoundValue = sound,
                Sensitivity = sensitivity
            };
            _data.Save(saveGame, Path.Combine(_path, _fileName));
        }
        public void SaveSettings()
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }
            SettingsData saveGame = new SettingsData();
            _data.Save(saveGame, Path.Combine(_path, _fileName));
        }
        public void LoadSettings(out bool loadGame, out float musicValue, out float soundValue, out float sensValue)
        {
            string file = Path.Combine(_path, _fileName);
            if (!File.Exists(file))
            {
                SaveSettings();
            }
            SettingsData loadSettings = _data.Load(file);
            loadGame = loadSettings.LoadGame;
            musicValue = loadSettings.MusicValue;
            soundValue = loadSettings.SoundValue;
            sensValue = loadSettings.Sensitivity;
        }
    }



    [Serializable]
    public sealed class SettingsData
    {
        public bool LoadGame;
        public float MusicValue;
        public float SoundValue;
        public float Sensitivity;
        public SettingsData()
        {
            LoadGame = false;
            MusicValue = 0f;
            SoundValue = 0f;
            Sensitivity = 5f;
        }
    }
}