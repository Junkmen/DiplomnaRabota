using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour {
    public bool isSnapped;// { get; set; }
    public Link other;
    private Room room;

    private int m_unsnappedCount = 0;

    void Awake()
    {
        isSnapped = false;
        SpriteRenderer r = GetComponent<SpriteRenderer>();
        Utils.SafeRemove(r);
        room = GetComponentInParent<Room>();
    }

    public void Unsnap()
    {
        m_unsnappedCount++;
        other = null;
        isSnapped = false;
        //Room r = gameObject.GetComponent<Room>();
        if (m_unsnappedCount < 3) room.OnBrokenLink(gameObject);
        else room.OnFatalLink(gameObject);
    }
}
