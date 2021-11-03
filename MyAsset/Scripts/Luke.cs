using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollABollGame
{
    public sealed class Luke : ObjectAftermath
    {
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
        }
        protected override void Aftermath()
        {
            _anim.SetBool("IsLukeUp", true);
        }
    }
}