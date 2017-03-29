using System.Collections.Generic;
using UnityEngine;

namespace DataVisualisation.Utilities
{
    public class ColorGenerator : MonoBehaviour
    {
        private const float DEFAULT_HUE = 0.6f;
        private const float DEFAULT_SATURATION = 0.5f;
        private const float DEFAULT_VALUE = 0.9f;

        public static List<Color> GetColorsGoldenRatio(int amount,float hue = DEFAULT_HUE, float saturation = DEFAULT_SATURATION, float value = DEFAULT_VALUE)
        {
            var variableHue = hue;
            var goldenRatio = 1.6180339887498948482f;

            var colors = new List<Color>();

            for (int i = 0; i < amount; i++)
            {
                colors.Add(Color.HSVToRGB(variableHue, saturation, value));
                variableHue += goldenRatio;
                variableHue %= 1.0f;
            }

            return colors;        
        }
    }
}