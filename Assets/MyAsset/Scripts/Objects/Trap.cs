using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollABollGame
{
    public sealed class Trap : ObjectInteractive
    {
        [SerializeField] private AudioClip _sound;
        Animator _anim;
        private Transform _body;
        public delegate void Action(Vector3 position, AudioClip sound);
        public event Action playerInTrapEvent;
        private void Awake()
        {
            _body = transform.Find("Body").transform;
            _anim = GetComponent<Animator>();
            _anim.SetBool("IsTrapped", false);
        }
        IEnumerator Rise()
        {
            Destroy(gameObject.GetComponent<BoxCollider>());
            yield return new WaitForSeconds(0.25f);
            playerInTrapEvent?.Invoke(transform.position, _sound);
            _anim.SetBool("IsTrapped", true);
            yield return new WaitForSeconds(3f);
            foreach (Transform child in _body)
            {
                if (child.GetComponent<Rigidbody>())
                {
                    child.GetComponent<Rigidbody>().isKinematic = false;
                }
            }
            Destroy(_anim);
            Destroy(this);
        }
        protected override void Interaction()
        {
            StartCoroutine(Rise());
        }
    }
}
