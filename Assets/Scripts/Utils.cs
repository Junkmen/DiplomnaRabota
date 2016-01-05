using UnityEngine;
using System.Collections;
using System.Diagnostics;

public static class Utils {

	public static void Assert(bool condition, string text)
    {
        if (!condition)
        {
            UnityEngine.Debug.LogError(text);
            UnityEngine.Debug.Break();
            Debugger.Break();
        }
    }

    public static void SafeDestroy(GameObject g)
    {
        if (g) { MonoBehaviour.Destroy(g); }
    }

    public static void SafeRemove(Component c)
    {
        if (c) { MonoBehaviour.Destroy(c); }
    }
}
