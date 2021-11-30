using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollABollGame
{
    public class Roof : ObjectAftermath
    {
        private Transform _body;
        private void Awake()
        {
            _body = transform.Find("Body").transform;
        }
        private void Start()
        {
            _body.gameObject.SetActive(false);
        }
        protected override void Interaction()
        {
            _body.gameObject.SetActive(true);
        }
        protected override void Aftermath()
        {
            _body.gameObject.SetActive(false);
        }
    }
}