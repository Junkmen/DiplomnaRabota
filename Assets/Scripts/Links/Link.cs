using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour {
    public bool isSnapped;// { get; set; }
    public Link other;

    private int m_unsnappedCount = 0;

    void Awake()
    {
        isSnapped = false;
        SpriteRenderer r = GetComponent<SpriteRenderer>();
        Utils.SafeRemove(r);
    }

    public void Unsnap()
    {
        m_unsnappedCount++;
        other = null;
        isSnapped = false;
        Room r = GetComponent<Room>();
        if (m_unsnappedCount < 3) r.OnBrokenLink(gameObject);
        else r.OnFatalLink(gameObject);
    }
}
