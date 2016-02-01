using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

 //   public GameObject[] prefabRooms; //Prefabs are not started thus we need two arrays.
    public GameObject startingRoom;
    public Vector3 startingPos;

    private GameObject[] InitializedRooms; //Rooms here have their child scripts starts executed, thus they are valid for copying.
    private ArrayList UnfinishedRooms = new ArrayList(); //Rooms that have UnconnectedLinks.

    GameObject prevRoom;
    GameObject nextRoom;

    public int roomLimit;
    public PercentageArray rooms;

    public static int retryAttempts = 3;
    public int m_retryAttempts = 3;

    public bool IsGenerated()
    {
        return isGenerated;
    }

    void InitPrefabs()
    {
        InitializedRooms = new GameObject[rooms.array.Length];
       // for (int i = 0; i < prefabRooms.Length; i++)
    //    {
     //       InitializedRooms[i] = Instantiate(prefabRooms[i]) as GameObject;
    //    }
        
        for(int i = 0; i < rooms.array.Length; i++)
        {
            InitializedRooms[i] = Instantiate(rooms.array[i].o);
        }
    }

    public void AddUnfinishedRoom(GameObject r)
    {
        UnfinishedRooms.Add(r);
    }

    public void RemoveUnfinishedRoom(GameObject g)
    {
        UnfinishedRooms.Remove(g);
    }

    GameObject GetRandomRoom()
    {

        int roomIndex = rooms.GetFromPercentage(Random.Range(0,100));//InitializedRooms[Random.Range(0, InitializedRooms.Length)];
        GameObject g = Instantiate(InitializedRooms[roomIndex]) as GameObject;
        return g;
    }

    public static Generator massGenerator;


    void Start ()
    {
        massGenerator = this;
        retryAttempts = m_retryAttempts;
        InitPrefabs();
        prevRoom = Instantiate(startingRoom, startingPos, startingRoom.transform.rotation) as GameObject;
        prevRoom.GetComponent<Room>().SetGenerator(this);
    }
	
    void SnapRooms(GameObject room1, GameObject room2, GameObject link1, GameObject link2)
    {
        Utils.Assert(room1 && room2 && link1 && link2, "Jesus wtf happened");
        link2.transform.parent = null;
        room2.transform.parent = link2.transform;

        link2.transform.position = link1.transform.position;
        link2.transform.rotation = link1.transform.rotation;
        link2.transform.Rotate(new Vector3(0, 180, 0));

        room2.transform.parent = room1.transform; //Remove in final version. Debugging purpose.
        link2.transform.parent = room2.transform;

        link1.GetComponent<Link>().other = link2.GetComponent<Link>();
        link2.GetComponent<Link>().other = link1.GetComponent<Link>();
    }

    bool isGenerated = false;
    public int roomCount = 0;

	void Update () {
        //if (!Input.GetKeyDown(KeyCode.Space)) return;
        if (isGenerated) return;

        ClearRooms();

        int ur_count = UnfinishedRooms.Count;
        for (int i = 0; i < ur_count; i++)
        {
            prevRoom = UnfinishedRooms[i] as GameObject;
            nextRoom = GetRandomRoom();
            nextRoom.GetComponent<Room>().SetGenerator(this);

            LinkContainer lc1 = prevRoom.GetComponent<LinkContainer>();
            LinkContainer lc2 = nextRoom.GetComponent<LinkContainer>();

            GameObject link1 = lc1.GetRandomLink();
            GameObject link2 = lc2.GetRandomLink();

            bool canContinue = link1 && link2;
            Utils.Assert(canContinue, "Room has no links but is still unfinished.");
            if (!canContinue) continue;

            SnapRooms(prevRoom, nextRoom, link1, link2);

            isGenerated = roomCount++ > roomLimit || UnfinishedRooms.Count == 0;
        }

        ClearRooms();

        if (isGenerated && InitializedRooms[0].activeInHierarchy)
        {
            foreach (GameObject go in InitializedRooms) go.SetActive(false);
        }


        //Debug.Break();
    }

    void ClearRooms()
    {
        ArrayList objectsToRemove = new ArrayList();

        foreach (GameObject g in UnfinishedRooms)
        {
            if (g == null)
            {
                objectsToRemove.Add(g);
                continue;
            }

            Room r = g.GetComponent<Room>();
            if (r.IsFinished())
            {
                objectsToRemove.Add(g);
            }
        }

        foreach (GameObject g in objectsToRemove)
        {
            UnfinishedRooms.Remove(g);
        }
    }
}


