using UnityEngine;

namespace RollABollGame
{
    public abstract class PlayerBase : MonoBehaviour
    {
        public float Speed = 16f;
        public float Fine = 1.6f;

        public abstract void Move(Vector3 moveDirection);
        public abstract void Jump();
        public abstract void Boost(Vector3 moveDirection);
        public abstract void Water(bool value);
    }
}