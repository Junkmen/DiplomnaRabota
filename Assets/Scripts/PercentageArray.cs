using UnityEngine;
using System.Collections;

[System.Serializable]
public struct PercentagePair {
   public GameObject o;
   public float p;
}

[System.Serializable]
public struct PercentageArray {
    public PercentagePair[] array;
    
    public int GetFromPercentage(float percentage)
    {
        PercentagePair minPair;
        minPair.p = 101f;
        minPair.o = null;

        int index = -1;

        for(int i = 0; i < array.Length; i++)
        {
            if((percentage - array[i].p) < 0 && (minPair.p > array[i].p))
            {
                minPair = array[i];
                index = i;
            }
        }
        Utils.Assert(minPair.o != null, "Roll didn't end nice");
        return index;
    }
}
