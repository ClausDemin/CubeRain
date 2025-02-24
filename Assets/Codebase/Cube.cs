using Assets.Codebase.Interfaces;
using Assets.Codebase.Utils;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Codebase
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Cube : MonoBehaviour, IPooledInstance
    {
        [SerializeField, Range(2, 5)] private float _minLifetime;
        [SerializeField, Range(2, 5)] private float _maxLifetime;

        private Color _defaultColor;

        private MeshRenderer _meshRenderer;

        private bool _isPlatformTouched;

        public bool IsFree { get; private set; }

        public event Action<IPooledInstance> Released;
        public event Action<IPooledInstance> Disposed;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _defaultColor = _meshRenderer.material.color;

            Disable();
        }

        private void OnValidate()
        {
            if (_maxLifetime < _minLifetime)
            {
                _maxLifetime = _minLifetime;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<Platform>(out Platform platform) && _isPlatformTouched == false) 
            {
                SelectRandomColor();

                float lifetime = Randomizer.GetRandomFloat(_minLifetime, _maxLifetime);

                StartCoroutine(DieOverTime(lifetime));

                _isPlatformTouched = true;
            }
        }

        private void OnDisable()
        {
            _meshRenderer.material.color = _defaultColor;
            _isPlatformTouched = false;

            IsFree = true;
        }

        private void OnDestroy()
        {
            Disposed?.Invoke(this);
        }

        public void Enable()
        {
            IsFree = false;

            gameObject.SetActive(true);
        }

        private IEnumerator DieOverTime(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            Disable();
        }

        private void Disable()
        {
            IsFree = true;

            gameObject.SetActive(false);

            Released?.Invoke(this);
        }

        private void SelectRandomColor()
        {
            if (_meshRenderer != null)
            {
                _meshRenderer.material.color =
                    new Color(Randomizer.GetRandomFloat(), Randomizer.GetRandomFloat(), Randomizer.GetRandomFloat());
            }
        }
    }
}
