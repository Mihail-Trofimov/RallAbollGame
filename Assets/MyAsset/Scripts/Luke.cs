using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollABollGame
{
    public sealed class Luke : ObjectAftermath
    {
        [SerializeField] private AudioClip _soundOpen;
        [SerializeField] private AudioClip _soundClose;
        public delegate void PlayerInLuke(Vector3 position, AudioClip sound);
        public event PlayerInLuke playerInLukeEvent;
        Animator _anim;
        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }
        private void OnEnable()
        {
            _anim.SetBool("IsLukeUp", true);
        }
        protected override void Interaction()
        {
            _anim.SetBool("IsLukeUp", false);
            playerInLukeEvent?.Invoke(transform.position, _soundOpen);
        }
        protected override void Aftermath()
        {
            _anim.SetBool("IsLukeUp", true);
            playerInLukeEvent?.Invoke(transform.position, _soundClose);
        }
    }
}