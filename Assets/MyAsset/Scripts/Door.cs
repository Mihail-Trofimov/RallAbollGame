using System;
using UnityEngine;

namespace RollABollGame
{
    public class Door: ObjectInteractive
    {
        public enum Index
        {
            white = 0,
            purple = 1,
            blue = 2,
            yellow = 3,
            orange = 4
        };
        public Index doorColor = 0;

        public void OpenDoor(int keyColor)
        {
            if (keyColor == Convert.ToInt32(doorColor))
            {
                float _xColorR = gameObject.GetComponent<MeshRenderer>().materials[0].color.r;
                float _xColorG = gameObject.GetComponent<MeshRenderer>().materials[0].color.g;
                float _xColorB = gameObject.GetComponent<MeshRenderer>().materials[0].color.b;
                gameObject.GetComponent<MeshRenderer>().materials[0].color = new Color(_xColorR, _xColorG, _xColorB, 0.3f);
                Destroy(gameObject.GetComponent<BoxCollider>());
            }
        }
        protected override void Interaction()
        {
            Debug.Log("Нужен ключ");
        }
    }
}