using System;
using UnityEngine;

namespace Kickstarter.Extensions
{
    public static class Vector2Extensions
    {
        public static bool TryParse(string dataString, out Vector2 vector2Value)
        {
            dataString = StringExtensions.RemoveParenthesis(dataString);
            string[] components = dataString.Split(',');
            float[] sections = new float[2];
            try
            {
                if (components.Length != sections.Length)
                    throw new Exception();
                for (int i = 0; i < sections.Length; i++)
                    sections[i] = float.Parse(components[i]);
                vector2Value = new Vector2(sections[0], sections[1]);
                return true;
            } catch
            {
                vector2Value = default;
                return false;
            }
        }
    }
}
