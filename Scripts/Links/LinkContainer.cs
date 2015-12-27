using UnityEngine;
using System.Collections;

public class LinkContainer : MonoBehaviour {

    public GameObject[] links;


	// Use this for initialization
	void Start () {
        Utils.Assert(Size() > 0, "Empty containers are bad.");
	}

    public GameObject GetLink(int index, bool snap = false)
    {
        Utils.Assert(index < Size(), "Invalid memory access");
        if (snap)
        {
            links[index].GetComponent<Link>().isSnapped = true;
        }
        return links[index];
    }

    public GameObject GetRandomLink(bool snap = false)
    {
        ArrayList al = new ArrayList();
        for (int i = 0; i < links.Length; i++)
        {
            GameObject g = links[i];
            if (!g.GetComponent<Link>().isSnapped)
            {
                al.Add(i);
            }
        }
        int rnd = Random.Range(0, al.Count);
        return al.Count > 0 ? GetLink((int)al[rnd], true) : null;
    }

    public int Size()
    {
        return links.Length;
    }
}
