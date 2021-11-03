using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollABollGame
{
    public sealed class StartPlace : ObjectAftermath
    {
        Animator _anim;
        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }
        IEnumerator Rise(bool _upDown)
        {
            yield return new WaitForSeconds(1f);
            _anim.SetBool("IsStartPlaceUp", _upDown);
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
