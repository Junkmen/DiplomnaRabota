using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

    static int id = 0;

    static int GetID() { return id++; }
    public enum RoomType
    {
        Junction,
        Cooridor,
        Room,
    }

    private int m_id;
    private LinkContainer m_lc;
    public RoomType m_type;
    private Generator m_generator;

    void Awake()
    {
        id = GetID();
        m_lc = GetComponent<LinkContainer>();
    }

    public void OnFinish()
    {
        Utils.SafeRemove(GetComponent<Rigidbody>());
    }

    void OnCollision()
    {
        foreach(GameObject g in m_lc.links)
        {
            g.GetComponent<Link>().other.Unsnap();
        }
    }

    public void OnBrokenLink(GameObject link)
    {

    }

    public void OnFatalLink(GameObject link)
    {

    }

    public bool IsFinished()
    {
        return !m_lc.HasEmptyLinks();
    }

    public void SetGenerator(Generator g)
    {
        m_generator = g;
        g.AddUnfinishedRoom(gameObject);
    }
}
