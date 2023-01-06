using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door
    public int minRoom = 0;
    public int maxRoom = 0;

    private RoomTemplates templates;
    [SerializeField] InstalationController instalation;
    private int rand;
    public bool spawned = false;

    Ray ray;

    public float waitTime = 10f;
    int LvlCorruption = 0;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        instalation = GetComponentInParent<AddRoom>().instalation;
        LvlCorruption = GetComponentInParent<AddRoom>().LevelCorruption;
        Invoke("Spawn", 0.75f);
    }

    void ChooseRand()
    {
        if (instalation.Tubos.Count == instalation.MinTubos + 2 && instalation.Habitaciones.Count == 0) { minRoom = 7; maxRoom = 0; return; }
        if (instalation.Tubos.Count <= instalation.MinTubos) { minRoom = 1; maxRoom = 2; }
        if (instalation.Tubos.Count > instalation.MinTubos) { minRoom = 3; maxRoom = 0; }
        if (instalation.Tubos.Count >= instalation.MaxTubos) { minRoom = 0; maxRoom = 7; }
    }

    // Update is called once per frame
    void Spawn()
    {
        WatchWall();
        if (spawned == false)
        {
            ChooseRand();
            rand = Random.Range(minRoom, templates.topRooms.Length - maxRoom);
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
        if (openingDirection == 1) { ray = new Ray(transform.position, Vector3.forward); }
        else if (openingDirection == 2) { ray = new Ray(transform.position, Vector3.back); }
        else if (openingDirection == 3) { ray = new Ray(transform.position, Vector3.left); }
        else { ray = new Ray(transform.position, Vector3.right); }


        if (Physics.Raycast(ray, out RaycastHit info, 6))
        {
            if (openingDirection == 1) { Instantiate(templates.bottomRooms[0], transform.position, templates.bottomRooms[0].transform.rotation, instalation.transform); }
            else if (openingDirection == 2) { Instantiate(templates.topRooms[0], transform.position, templates.topRooms[0].transform.rotation, instalation.transform); }
            else if (openingDirection == 3) { Instantiate(templates.leftRooms[0], transform.position, templates.leftRooms[0].transform.rotation, instalation.transform); }
            else if (openingDirection == 4) { Instantiate(templates.rightRooms[0], transform.position, templates.rightRooms[0].transform.rotation, instalation.transform); }
            spawned = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            GameObject[] Array1 = null;
            GameObject[] Array2 = null;

            if (openingDirection == 1) { Array1 = templates.bottomRooms; }
            else if (openingDirection == 2) { Array1 = templates.topRooms; }
            else if (openingDirection == 3) { Array1 = templates.leftRooms; }
            else if (openingDirection == 4) { Array1 = templates.rightRooms; }

            if (other.GetComponent<BaseSpawner>())
            {
                if (other.GetComponent<BaseSpawner>().spawned == false && spawned == false)
                {
                    //Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                else if (other.GetComponent<BaseSpawner>().spawned == true && spawned == false)
                {

                    if (ComprobarDirrecciones(other.GetComponent<BaseSpawner>().openingDirection) && other.GetComponent<BaseSpawner>().isExterior) { return; }
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
                    Destroy(gameObject);
                }
            }
            else if (other.GetComponent<RoomSpawner>())
            {
                if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
                {
                    Array2 = GetArray2(other.GetComponent<RoomSpawner>().openingDirection);
                    other.GetComponent<RoomSpawner>().spawned = true;
                    Instantiate(ConseguirObjeto(Array1, Array2), transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                else if (other.GetComponent<RoomSpawner>().spawned == true && spawned == false)
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
                    Destroy(gameObject);
                }
            }
            spawned = true;
        }
    }

    GameObject ConseguirObjeto(GameObject[] array1, GameObject[] array2)
    {
        GameObject Tuberia = null;

        for (var i = 0; i < array1.Length; i++)
        {
            for (var j = 0; j < array2.Length; j++)
            {
                //if (array1[i] == array2[j]) Tuberia = array1[i];
                if (array2[j].name.StartsWith(array1[i].name.Substring(0, 2))) { Tuberia = array1[i]; }
                return Tuberia;
            }
        }

        return Tuberia;
    }

    GameObject[] GetArray2(int OD)
    {
        GameObject[] Array2 = { };

        if (OD == 1) { Array2 = templates.bottomRooms; }
        else if (OD == 2) { Array2 = templates.topRooms; }
        else if (OD == 3) { Array2 = templates.leftRooms; }
        else if (OD == 4) { Array2 = templates.rightRooms; }

        return Array2;
    }

    bool ComprobarDirrecciones(int OtherOpeningDir)
    {
        bool canPass = false;
        if (((OtherOpeningDir <= 2 && openingDirection >= 3) || (OtherOpeningDir >= 3 && openingDirection <= 2)))
        {
            canPass = true;
        }
        return canPass;
    }
}
