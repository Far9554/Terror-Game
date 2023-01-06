using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door
    public int minRoom = 0;
    public int maxRoom = 0;
    public float delayGen = 0;

    private BaseTemplate templates;
    [SerializeField] InstalationController instalation;
    private int rand;
    public bool spawned = false;
    public bool isExterior = false;

    Ray ray;

    public float waitTime = 10f;
    int LvlCorruption = 0;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Bases").GetComponent<BaseTemplate>();
        instalation = GetComponentInParent<AddRoom>().instalation;
        LvlCorruption = GetComponentInParent<AddRoom>().LevelCorruption;
        Invoke("Spawn", 0.75f + delayGen);
    }

    void ChooseRand()
    {
        if (instalation.Habitaciones.Count <= instalation.MinRooms) { minRoom = 2; maxRoom = 3; }
        if (instalation.Habitaciones.Count > instalation.MinRooms) { maxRoom = 0; }
        if (instalation.Habitaciones.Count >= instalation.MaxRooms) { maxRoom = 6; minRoom = 0; }
    }

    // Update is called once per frame
    void Spawn()
    {
        WatchWall();
        if (spawned == false)
        {
            ChooseRand();
            rand = Random.Range(minRoom, templates.topRooms.Length - maxRoom);
            if (rand == 0) { isExterior = true; }
            GameObject newRoom;
            if (openingDirection == 1)
            {
                // Need to spawn a room with a BOTTOM door.
                newRoom = Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation, instalation.transform);
                newRoom.GetComponent<AddRoom>().instalation = instalation;
                newRoom.GetComponent<AddRoom>().LevelCorruption = LvlCorruption;
            }
            else if (openingDirection == 2)
            {
                // Need to spawn a room with a TOP door.
                newRoom = Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation, instalation.transform);
                newRoom.GetComponent<AddRoom>().instalation = instalation;
                newRoom.GetComponent<AddRoom>().LevelCorruption = LvlCorruption;
            }
            else if (openingDirection == 3)
            {
                // Need to spawn a room with a LEFT door.
                newRoom = Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation, instalation.transform);
                newRoom.GetComponent<AddRoom>().instalation = instalation;
                newRoom.GetComponent<AddRoom>().LevelCorruption = LvlCorruption;
            }
            else if (openingDirection == 4)
            {
                // Need to spawn a room with a RIGHT door.
                newRoom = Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation, instalation.transform);
                newRoom.GetComponent<AddRoom>().instalation = instalation;
                newRoom.GetComponent<AddRoom>().LevelCorruption = LvlCorruption;
            }
            spawned = true;
        }
    }

    void WatchWall() 
    {
        if (openingDirection == 1)      { ray = new Ray(transform.position, Vector3.forward); }
        else if (openingDirection == 2) { ray = new Ray(transform.position, Vector3.up); }
        else if (openingDirection == 3) { ray = new Ray(transform.position, Vector3.left); }
        else                            { ray = new Ray(transform.position, Vector3.right); }
            

        if (Physics.Raycast(ray, out RaycastHit info, 6))
        {
            GameObject newRoom;
            if (openingDirection == 1) {
                newRoom = Instantiate(templates.bottomRooms[0], transform.position, templates.bottomRooms[0].transform.rotation, instalation.transform);
                newRoom.GetComponent<AddRoom>().instalation = instalation;
                newRoom.GetComponent<AddRoom>().LevelCorruption = LvlCorruption;
            }
            else if (openingDirection == 2) {
                newRoom = Instantiate(templates.topRooms[0], transform.position, templates.topRooms[0].transform.rotation, instalation.transform);
                newRoom.GetComponent<AddRoom>().instalation = instalation;
                newRoom.GetComponent<AddRoom>().LevelCorruption = LvlCorruption;
            }
            else if (openingDirection == 3) {
                newRoom = Instantiate(templates.leftRooms[0], transform.position, templates.leftRooms[0].transform.rotation, instalation.transform);
                newRoom.GetComponent<AddRoom>().instalation = instalation;
                newRoom.GetComponent<AddRoom>().LevelCorruption = LvlCorruption;
            }
            else if (openingDirection == 4) {
                newRoom = Instantiate(templates.rightRooms[0], transform.position, templates.rightRooms[0].transform.rotation, instalation.transform);
                newRoom.GetComponent<AddRoom>().instalation = instalation;
                newRoom.GetComponent<AddRoom>().LevelCorruption = LvlCorruption;
            }
            spawned = true;
            isExterior = true;
            Destroy(gameObject);
        }  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>())
            {
                if (spawned == false)
                {
                    GameObject newRoom;
                    if (openingDirection == 1)
                    {
                        newRoom = Instantiate(templates.bottomRooms[0], transform.position, templates.bottomRooms[0].transform.rotation, instalation.transform);
                        newRoom.GetComponent<AddRoom>().instalation = instalation;
                        newRoom.GetComponent<AddRoom>().LevelCorruption = LvlCorruption;
                    }
                    else if (openingDirection == 2)
                    {
                        newRoom = Instantiate(templates.topRooms[0], transform.position, templates.topRooms[0].transform.rotation, instalation.transform);
                        newRoom.GetComponent<AddRoom>().instalation = instalation;
                        newRoom.GetComponent<AddRoom>().LevelCorruption = LvlCorruption;
                    }
                    else if (openingDirection == 3)
                    {
                        newRoom = Instantiate(templates.leftRooms[0], transform.position, templates.leftRooms[0].transform.rotation, instalation.transform);
                        newRoom.GetComponent<AddRoom>().instalation = instalation;
                        newRoom.GetComponent<AddRoom>().LevelCorruption = LvlCorruption;
                    }
                    else if (openingDirection == 4)
                    {
                        newRoom = Instantiate(templates.rightRooms[0], transform.position, templates.rightRooms[0].transform.rotation, instalation.transform);
                        newRoom.GetComponent<AddRoom>().instalation = instalation;
                        newRoom.GetComponent<AddRoom>().LevelCorruption = LvlCorruption;
                    }
                    isExterior = true;
                    Destroy(gameObject);
                }
            }
            else if (other.GetComponent<BaseSpawner>()) 
            {
                if (other.GetComponent<BaseSpawner>().spawned == false && spawned == false) {
                    GameObject newRoom;
                    if (openingDirection == 1)
                    {
                        newRoom = Instantiate(templates.bottomRooms[0], transform.position, templates.bottomRooms[0].transform.rotation, instalation.transform);
                        newRoom.GetComponent<AddRoom>().instalation = instalation;
                        newRoom.GetComponent<AddRoom>().LevelCorruption = LvlCorruption;
                    }
                    else if (openingDirection == 2)
                    {
                        newRoom = Instantiate(templates.topRooms[0], transform.position, templates.topRooms[0].transform.rotation, instalation.transform);
                        newRoom.GetComponent<AddRoom>().instalation = instalation;
                        newRoom.GetComponent<AddRoom>().LevelCorruption = LvlCorruption;
                    }
                    else if (openingDirection == 3)
                    {
                        newRoom = Instantiate(templates.leftRooms[0], transform.position, templates.leftRooms[0].transform.rotation, instalation.transform);
                        newRoom.GetComponent<AddRoom>().instalation = instalation;
                        newRoom.GetComponent<AddRoom>().LevelCorruption = LvlCorruption;
                    }
                    else if (openingDirection == 4)
                    {
                        newRoom = Instantiate(templates.rightRooms[0], transform.position, templates.rightRooms[0].transform.rotation, instalation.transform);
                        newRoom.GetComponent<AddRoom>().instalation = instalation;
                        newRoom.GetComponent<AddRoom>().LevelCorruption = LvlCorruption;
                    }
                    isExterior = true;
                    Destroy(gameObject);
                }
                else if (other.GetComponent<BaseSpawner>().isExterior == true && spawned == false)
                {
                    GameObject newRoom;
                    if (openingDirection == 1)
                    {
                        newRoom = Instantiate(templates.bottomRooms[0], transform.position, templates.bottomRooms[0].transform.rotation, instalation.transform);
                        newRoom.GetComponent<AddRoom>().instalation = instalation;
                        newRoom.GetComponent<AddRoom>().LevelCorruption = LvlCorruption;
                    }
                    else if (openingDirection == 2)
                    {
                        newRoom = Instantiate(templates.topRooms[0], transform.position, templates.topRooms[0].transform.rotation, instalation.transform);
                        newRoom.GetComponent<AddRoom>().instalation = instalation;
                        newRoom.GetComponent<AddRoom>().LevelCorruption = LvlCorruption;
                    }
                    else if (openingDirection == 3)
                    {
                        newRoom = Instantiate(templates.leftRooms[0], transform.position, templates.leftRooms[0].transform.rotation, instalation.transform);
                        newRoom.GetComponent<AddRoom>().instalation = instalation;
                        newRoom.GetComponent<AddRoom>().LevelCorruption = LvlCorruption;
                    }
                    else if (openingDirection == 4)
                    {
                        newRoom = Instantiate(templates.rightRooms[0], transform.position, templates.rightRooms[0].transform.rotation, instalation.transform);
                        newRoom.GetComponent<AddRoom>().instalation = instalation;
                        newRoom.GetComponent<AddRoom>().LevelCorruption = LvlCorruption;
                    }
                    isExterior = true;
                    Destroy(gameObject);
                }
            }

            spawned = true;
        }
    }
}
