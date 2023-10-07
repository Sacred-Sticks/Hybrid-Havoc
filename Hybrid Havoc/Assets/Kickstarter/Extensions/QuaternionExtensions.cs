using System;
using UnityEngine;

namespace Kickstarter.Extensions
{
    public static class QuaternionExtensions
    {
        public static bool TryParse(string dataString, out Quaternion quaternionValue)
        {
            dataString = StringExtensions.RemoveParenthesis(dataString);
            string[] components = dataString.Split(',');
            float[] sections = new float[4];
            try
            {
                if (components.Length != sections.Length)
                    throw new Exception();
                for (int i = 0; i < sections.Length; i++)
                    sections[i] = float.Parse(components[i]);
                quaternionValue = new Quaternion(sections[0], sections[1], sections[2], sections[3]);
                return true;
            } catch
            {
                quaternionValue = default;
                return false;
            }
        }
    }
}
