using Assets.Codebase.Interfaces;
using UnityEngine;

namespace Assets.Codebase.Service
{
    public class CubeFactory : IPooledInstanceFactory
    {
        private Cube _prefab;

        public CubeFactory(Cube prefab)
        {
            _prefab = prefab;
        }

        public IPooledInstance Create()
        {
            GameObject instance = GameObject.Instantiate(_prefab.gameObject);

            return instance.GetComponent<IPooledInstance>();
        }
    }
}
