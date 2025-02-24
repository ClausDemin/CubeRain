using Assets.Codebase.Utils;
using UnityEngine;

namespace Assets.Codebase
{
    public class ColorChanger
    {
        private Color _defaultColor;

        private MeshRenderer _meshRenderer;

        public ColorChanger(MeshRenderer renderer) 
        { 
            _meshRenderer = renderer;

            _defaultColor = _meshRenderer.material.color;
        }

        public void SelectRandomColor()
        {
            if (_meshRenderer != null)
            {
                _meshRenderer.material.color =
                    new Color(Randomizer.GetRandomFloat(), Randomizer.GetRandomFloat(), Randomizer.GetRandomFloat());
            }
        }

        public void ResetColor() 
        { 
            _meshRenderer.material.color = _defaultColor;
        }
    }
}
