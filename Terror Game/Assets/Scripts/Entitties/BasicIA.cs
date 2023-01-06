using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using EZCameraShake;

public class BasicIA : MonoBehaviour
{
    RaycastHit hit;
    public NavMeshAgent IA;
    public Animator anim;
    public PlayerMovement playerController;
    public InstalationController instalation;
    public Vector3 Target;
    DoorTrigger door;
    Vector3 offset = new Vector3(0,0.5f,0);
    public List<GameObject> PosHabi = null;

    public float IASpeed=0;
    float IARunSpeed = 5;
    bool Opening = false;
    public bool CanMove = true;
    public bool GoRandom = true;

    public bool Alerted = false;
    public bool Detected = false;
    public float lostPlayer = 0;

    float delayAttack = 0;

    public float moveSpeed;

    public List<AudioClip> Sounds = null;

    // Start is called before the first frame update
    void Start()
    {
        if (IA == null) IA.GetComponent<NavMeshAgent>();
        IASpeed = IA.speed;
        if (GoRandom) { InvokeRepeating("RandomGoPos", 10f, 10f); }
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, playerController.transform.position + offset, out hit, 60)) { 
            if (hit.transform.CompareTag("Player")) { Debug.DrawLine(transform.position, playerController.transform.position); } 
        }
        if (!IA.isOnNavMesh) { return; }

        IA.SetDestination(Target);

        if (GoRandom) { GetRooms(); }
        StatController();

        moveSpeed = IA.velocity.magnitude;
        anim.SetFloat("velocity", moveSpeed);
        anim.SetBool("Alerted", Alerted);
        anim.SetBool("Detected", Detected);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<DoorTrigger>(out DoorTrigger doorT)) {
            door = doorT;
            if (!doorT.door.isOpen) { IA.speed = IASpeed; return; }
            if (doorT.door.isOpen && Opening==false) { 
                Opening = true; 
                Invoke("OpenDoor", 1f); 
                IA.speed = 0; 
            }
            else if (Opening==false) { 
                IA.speed = IASpeed; 
            }
        }
    }

    void StatController()
    {
        if (lostPlayer < 0) { lostPlayer = 0; Alerted = false; Detected = false; } 
        else if (lostPlayer > 0 ) { 
            lostPlayer -= 1 * Time.deltaTime;
        }

        if (!CanMove) return;

        if (Alerted && !Detected)
        {
            // Solo esta en alerta
            IA.speed = 0;
        }
        else if (Detected)
        {
            // Lo ha detectado
            Target = playerController.transform.position;
            if (!Opening) { IA.speed = IARunSpeed; }
            IA.stoppingDistance = 1;
        }
        else
        {
            // Esta Normal
            IA.speed = IASpeed;
            IA.stoppingDistance = 3;
        }

    }

    void OpenDoor()
    {
        door.door.Activate();
        Opening = false;
    }

    void GetRooms()
    {
        if (PosHabi == null) { PosHabi.AddRange(instalation.Habitaciones); }
    }

    void RandomGoPos()
    {
        if (Detected || PosHabi == null) return;
        Target = RandomRoom();
    }

    Vector3 RandomRoom()
    {
        int r = Random.Range(0, PosHabi.Count - 1);
        return PosHabi[r].transform.position;
    }

    void AttackPlayer()
    {
        if (delayAttack < 0) { delayAttack = 0; } else { delayAttack -= 1 * Time.deltaTime; }
    }
}
