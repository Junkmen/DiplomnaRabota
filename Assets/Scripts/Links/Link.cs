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

        if (m_unsnappedCount < Generator.retryAttempts) room.OnBrokenLink(gameObject);
        else room.OnFatalLink(gameObject);

        isSnapped = false;
        //Room r = gameObject.GetComponent<Room>();
       
    }
}
