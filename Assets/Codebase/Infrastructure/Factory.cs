using Assets.Codebase.Interfaces;
using UnityEngine;

namespace Assets.Codebase.Service
{
    public class Factory<T> : IPooledInstanceFactory
        where T : MonoBehaviour, IPooledInstance
    {
        private T _prefab;

        public Factory(T prefab)
        {
            _prefab = prefab;
        }

        public IPooledInstance Create()
        {
            T instance = GameObject.Instantiate(_prefab);

            return instance;
        }
    }
}
