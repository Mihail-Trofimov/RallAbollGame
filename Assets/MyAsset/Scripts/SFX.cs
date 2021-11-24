using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace RollABollGame
{
    public sealed class SFX
    {

        private GameObject _gameObject;
        private AudioSource _audioSource;

        public GameObject gameObject
        {
            get { return _gameObject; }
        }
        public AudioSource source
        {
            get { return _audioSource; }
            set { _audioSource = value; }
        }

        public SFX(Vector3 pos, AudioClip clip, AudioMixerGroup mixer)
        {
            _gameObject = new GameObject();
            _gameObject.transform.position = pos;
            _audioSource = _gameObject.AddComponent<AudioSource>();
            _audioSource.clip = clip;
            _audioSource.outputAudioMixerGroup = mixer;
        }
    }
}