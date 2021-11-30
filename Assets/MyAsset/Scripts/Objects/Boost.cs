using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RollABollGame
{
    public sealed class Boost : ObjectInteractive, IPingPong, IExecute
    {
        [SerializeField] private AudioClip _clip;
        private Transform _body;
        private float _lengthMove = 1.2f;
        private Collider _collider;

        public delegate void PlayerTakeBoost(Vector3 vector, Vector3 position, AudioClip clip);
        public event PlayerTakeBoost playerTakeBoostEvent;

        private void Awake()
        {
            _body = transform.Find("Body").transform;
            _collider = GetComponent<BoxCollider>();
            IsDestroyable = false;
        }
        void OnEnable()
        {
            _body.gameObject.SetActive(true);
            _collider.enabled = true;
        }
        public void Execute()
        {
            PingPong();
        }
        public void PingPong()
        {
            _body.transform.localPosition = new Vector3(Mathf.PingPong(Time.time, _lengthMove), _body.transform.localPosition.y, _body.transform.localPosition.z);
        }
        protected override void Interaction()
        {
            playerTakeBoostEvent?.Invoke(transform.right, transform.position, _clip);
            StartCoroutine(Wait());
        }

        void Invisible()
        {
            _body.gameObject.SetActive(!_body.gameObject.activeSelf);
            _collider.enabled = !_collider.enabled;
        }
        IEnumerator Wait()
        {
            Invisible();
            yield return new WaitForSeconds(5f);
            Invisible();
        }
    }
}