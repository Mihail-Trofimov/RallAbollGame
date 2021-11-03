using System;
using System.Collections;
using Object = UnityEngine.Object;

namespace RollABollGame
{
    public sealed class ListInteractiveObject : IEnumerator, IEnumerable
    {
        private ObjectInteractive[] _interactiveObjects;
        private int _index = -1;
        private ObjectInteractive _current;

        public ListInteractiveObject()
        {
            var interactiveObjects = Object.FindObjectsOfType<ObjectInteractive>();
            foreach(var obj in interactiveObjects)
            {
                AddInteractiveObject(obj);
            }
        }

        public void AddInteractiveObject(ObjectInteractive interactive)
        {
            if (_interactiveObjects == null)
            {
                _interactiveObjects = new[] { interactive };
                return;
            }
            Array.Resize(ref _interactiveObjects, Length + 1);
            _interactiveObjects[Length - 1] = interactive;
        }
        public ObjectInteractive this[int index]
        {
            get => _interactiveObjects[index];
            private set => _interactiveObjects[index] = value;
        }

        public int Length => _interactiveObjects.Length;

        public bool MoveNext()
        {
            if (_index == _interactiveObjects.Length - 1)
            {
                Reset();
                return false;
            }

            _index++;
            return true;
        }

        public void Reset() => _index = -1;

        public object Current => _interactiveObjects[_index];

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

