using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollABollGame
{
    public sealed class Lift : ObjectAftermath
    {
        [SerializeField] private AudioClip _sound;

        public delegate void Action(Vector3 position, AudioClip sound);
        public event Action playerInLiftEvent;

        Animator _anim;
        bool _playerIn = false;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            //_anim.SetBool("IsLiftUp", true);
        }
        //private void OnEnable()
        //{
        //    _anim.SetBool("IsLiftUp", false);
        //}
        IEnumerator Rise(bool value)
        {
            yield return new WaitForSeconds(1.0f);
            if (_playerIn == value && _playerIn != _anim.GetBool("IsLiftUp"))
            {
                _anim.SetBool("IsLiftUp", value);
                yield return new WaitForSeconds(0.07f);
                if (_playerIn == value)
                {
                    playerInLiftEvent?.Invoke(transform.position, _sound);
                }
            }
        }
        protected override void Interaction()
        {
            _playerIn = true;
            StartCoroutine(Rise(_playerIn));
        }
        protected override void Aftermath()
        {
            _playerIn = false;
            StartCoroutine(Rise(_playerIn));
        }
    }
}