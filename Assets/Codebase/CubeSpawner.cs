using Assets.Codebase.Service;
using System.Collections;
using UnityEngine;

namespace Assets.Codebase
{
    public class CubeSpawner: MonoBehaviour
    {
        [SerializeField] private Cube _prefab;
        [SerializeField, Min(0.1f)] private float _spawnDelay;
        [SerializeField] private float _spawnRadius;

        private CircleSpawnArea _spawnArea;

        private Pool<Cube> _pool;

        private void Awake()
        {
            _spawnArea = new CircleSpawnArea(_spawnRadius);

            _pool = new Pool<Cube>(new Factory<Cube>(_prefab), 5);
        }

        private void Start()
        {
            StartCoroutine(SpawnCube());
        }

        private IEnumerator SpawnCube() 
        {
            while (true) 
            {
                Cube instance = _pool.Get();

                instance.gameObject.transform.position = _spawnArea.GetPointWithin(transform.position);

                yield return new WaitForSeconds(_spawnDelay);
            }
        }
    }
}
