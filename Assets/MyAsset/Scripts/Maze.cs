using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollABollGame
{
    public class Maze : ObjectAftermath
    {
        private Transform _body;
        private void Awake()
        {
            _body = transform.Find("Body").transform;
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