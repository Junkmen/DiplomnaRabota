using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour {
    public bool isSnapped;// { get; set; }

    void Awake()
    {
        isSnapped = false;
        SpriteRenderer r = GetComponent<SpriteRenderer>();
        Utils.SafeRemove(r);
    }
}
