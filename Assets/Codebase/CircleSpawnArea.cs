using Assets.Codebase.Utils;
using System;
using UnityEngine;

namespace Assets.Codebase
{
    public class CircleSpawnArea
    {
        private float _radius;

        public CircleSpawnArea(float radius)
        {
            _radius = radius;
        }

        public Vector3 GetPointWithin(Vector3 center) 
        {
            float x = Randomizer.GetRandomFloat(0, _radius) * Randomizer.GetRandomSign();

            float z = Randomizer.GetRandomFloat(0, _radius) * Randomizer.GetRandomSign();

            return center + new Vector3(x, 0, z);
        }
    }
}
