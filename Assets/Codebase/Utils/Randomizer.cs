using System;

namespace Assets.Codebase.Utils
{
    public static class Randomizer
    {
        private static Random s_random = new Random();

        public static float GetRandomFloat(float minValue = 0, float maxValue = 1)
        {
            return (float)s_random.NextDouble() * (maxValue - minValue) + minValue;
        }

        public static int GetRandomSign(float positiveChance = 0.5f) 
        {
            if (GetRandomFloat() < positiveChance) 
            { 
                return -1;
            }

            return 1;
        }
    }
}
