using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollABollGame
{
    public sealed class Lift : ObjectAftermath
    {
        Animator _anim;
        
        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }
        private void OnEnable()
        {
            _anim.SetBool("IsLiftUp", false);
        }
        IEnumerator Rise(bool _upDown)
        {
            yield return new WaitForSeconds(1.0f);
            _anim.SetBool("IsLiftUp", _upDown);
        }
        protected override void Interaction()
        {
            StartCoroutine(Rise(true));
        }
        protected override void Aftermath()
        {
            StartCoroutine(Rise(false));
        }
    }
}