using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

    public GameObject[] prefabRooms; //Prefabs are not started thus we need two arrays.

    private GameObject[] InitializedRooms; //Rooms here have their child scripts starts executed, thus they are valid for copying.
    private GameObject[] UnfinishedRooms; //Rooms that have UnconnectedLinks.

    GameObject prevRoom;
    GameObject nextRoom;


    void InitPrefabs()
    {
        InitializedRooms = new GameObject[prefabRooms.Length];
        for (int i = 0; i < prefabRooms.Length; i++)
        {
            InitializedRooms[i] = Instantiate(prefabRooms[i]) as GameObject;
        }
    }

    GameObject GetRandomRoom()
    {
        GameObject room = InitializedRooms[Random.Range(0, InitializedRooms.Length)];
        return Instantiate(room) as GameObject;
    }

    void Start ()
    {
        InitPrefabs();
        prevRoom = GetRandomRoom();
     
    }
	
    void SnapRooms(GameObject room1, GameObject room2, GameObject link1, GameObject link2)
    {
        Utils.Assert(room1 && room2 && link1 && link2, "Jesus wtf happened");
        link2.transform.parent = null;
        room2.transform.parent = link2.transform;

        link2.transform.position = link1.transform.position;
        link2.transform.rotation = link1.transform.rotation;
        link2.transform.Rotate(new Vector3(0, 180, 0));

        room2.transform.parent = room1.transform;
        link2.transform.parent = room2.transform;

    }

    bool isGenerated = false;
    int xi = 0;

	void Update () {
        if (isGenerated) return;
        nextRoom = GetRandomRoom();

        LinkContainer lc1 = prevRoom.GetComponent<LinkContainer>();
        LinkContainer lc2 = nextRoom.GetComponent<LinkContainer>();

        GameObject link1 = lc1.GetRandomLink();
        GameObject link2 = lc2.GetRandomLink();
        SnapRooms(prevRoom, nextRoom, link1, link2);
        prevRoom = nextRoom;
        
        isGenerated = xi++ > 10000;
        if (isGenerated && InitializedRooms[0].activeInHierarchy)
        {
            foreach (GameObject go in InitializedRooms) go.SetActive(false);
        }
        Debug.Break();
    }
}
