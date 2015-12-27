using UnityEngine;
using System.Collections;

public class CreateLinkFromChildren : MonoBehaviour {

    public GameObject[] pos;

	void Start ()
    {
        if (pos.Length < 2)
        {
            Utils.Assert(false, "Called on either initialized object or a broken one");
            return;
        }
        Vector3 middle = Vector3.Lerp(pos[0].transform.position,pos[1].transform.position,0.5f);
        transform.position = middle;
        transform.rotation = pos[0].transform.rotation;

        foreach (GameObject go in pos)
        {
            //Foreach means there is an object, but in case there is a coroutine something might happen.
            Utils.SafeDestroy(go);
        }
        Utils.SafeRemove(this);
  	}
	
	void Update ()
    {
	
	}
}
