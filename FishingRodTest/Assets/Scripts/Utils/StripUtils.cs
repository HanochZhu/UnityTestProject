using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StripUtils  {

    public static float GetWidth(float x,float l)
    {
        return Mathf.Pow(x, 2) * (3 * l - x) / (2 * Mathf.Pow(l, 3));
    }

    public static GameObject CreatStripNode(string name = "sphere")
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.name = name;
        return sphere;
    }

    public static float interpolationFaction = 0.3f;

    public static float getInterpolation(float x)
    {
        return (Mathf.Pow(2, -10 * x) * Mathf.Sin((x - 0.1f / 4f) * (2f * Mathf.PI) / interpolationFaction) + 1f);
    }

    public static Vector3 mylerp(Vector3 or, Vector3 tar, float input)
    {
        return tar * input + or * (1.0f - input);
    }
}
