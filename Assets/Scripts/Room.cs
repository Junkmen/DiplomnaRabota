﻿using UnityEngine;
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
    public Generator m_generator;

    void Awake()
    {
        m_id = GetID();
        m_lc = GetComponent<LinkContainer>();
        m_generator = Generator.massGenerator;
    }

    void Update()
    {
        if (m_generator.IsGenerated())
        {
            OnFinish();
            Utils.SafeRemove(this);
        }
    }

    public void OnFinish()
    {
        Utils.SafeRemove(GetComponent<Rigidbody>());
        Utils.SafeRemove(GetComponent<Collider>());
        m_lc.DestroyContainer();
        //gameObject.transform.parent = null;
    }


    void OnTriggerEnter(Collider other)
    {
        Room otherRoom = null;
       // Debug.Log("Collision");
        if (other && other.gameObject)
        {
            otherRoom = other.gameObject.GetComponent<Room>();
            if ( otherRoom == null) return;
        }

        if (otherRoom.m_id < m_id)
        {
            foreach (GameObject g in m_lc.links)
            {
                if (g)
                {
                    Link otherLink = g.GetComponent<Link>().other;
                    if (otherLink)
                    {
                        otherLink.Unsnap();
                    }
                }
            }
            m_generator.RemoveUnfinishedRoom(gameObject);
            Utils.SafeDestroy(gameObject);
           // Debug.Log("Room destroyed");
        }
    }

    public void OnBrokenLink(GameObject link)
    {
        if (IsFinished())
        {
            m_generator.AddUnfinishedRoom(gameObject);
        }
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
