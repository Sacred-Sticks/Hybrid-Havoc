using System;
using UnityEngine;

namespace Kickstarter.Extensions
{
    public static class Vector3Extensions
    {
        public static bool TryParse(string dataString, out Vector3 vector3Value)
        {
            dataString = StringExtensions.RemoveParenthesis(dataString);
            string[] components = dataString.Split(',');
            float[] sections = new float[3];
            try
            {
                if (components.Length != sections.Length)
                    throw new Exception();
                for (int i = 0; i < sections.Length; i++)
                    sections[i] = float.Parse(components[i]);
                vector3Value = new Vector3(sections[0], sections[1], sections[2]);
                return true;
            } catch
            {
                vector3Value = default;
                return false;
            }
        }
    }
}
