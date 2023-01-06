using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPEZ : MonoBehaviour
{
    public float speed = 1;
    private float OrSpeed, OrRotSpeed;
    public float rotSpeed;
    public float dist = 3;
    public LayerMask terrainLayer;
    RaycastHit Hit;
    public Transform B1, B2;
    public Transform A1, A2;
    public Transform player;
    public Vector3 Center;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(Random.Range(-45, 45), Random.Range(-180, 180), 0, Space.Self);
        OrSpeed = speed;
        OrRotSpeed = rotSpeed;
        //InvokeRepeating("GotoCenter", Random.Range(10, 30), Random.Range(10, 30));
        Center = new Vector3( transform.position.x + Random.Range(-dist, dist), transform.position.y + Random.Range(0, dist), transform.position.z + Random.Range(-dist, dist));
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y, 0, 1);
        if (transform.localRotation.z >= 0.1f) transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
        if (transform.localRotation.z <= -0.1f) transform.Rotate(Vector3.forward * -rotSpeed * Time.deltaTime);

             if (Physics.Raycast(B1.position, transform.forward, out Hit, dist)) { GoUp(); }
        else if (Physics.Raycast(B2.position, transform.forward, out Hit, dist)) { GoDown(); }

             if (Physics.Raycast(A1.position, transform.forward, out Hit, dist)) { GoLeft(); }
        else if (Physics.Raycast(A2.position, transform.forward, out Hit, dist)) { GoRight(); }

        if (Vector3.Distance(transform.position, player.position) < dist) { EscapePlayer(); }
        else if ((Vector3.Distance(transform.position, Center) > dist * 2)) { GotoCenter(); }
        else
        {
            speed = OrSpeed;
            rotSpeed = OrRotSpeed;
            if (anim != null) anim.speed = 1;
        }

        if (transform.position.y <= Center.y) { GoUp(); }
    }

    private void FixedUpdate() { transform.position += transform.forward * speed * Time.deltaTime; }

    void EscapePlayer()
    {
        //transform.LookAt(-player.position);
        speed = OrSpeed * 4;
        rotSpeed = OrRotSpeed * 2;
        if (anim != null) anim.speed = 2;
    }

    void GotoCenter() {
        Quaternion desiredRotation = Quaternion.LookRotation(Center - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * 5); 
    }

    void GoUp() { transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime); }

    void GoDown() { transform.Rotate(Vector3.up * -rotSpeed * Time.deltaTime); }

    void GoLeft() { transform.Rotate(Vector3.left * rotSpeed * Time.deltaTime); }

    void GoRight() { transform.Rotate(Vector3.left * -rotSpeed * Time.deltaTime); }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(B1.position, B1.forward * dist);
        Gizmos.DrawRay(B2.position, B2.forward * dist);
        Gizmos.DrawRay(A1.position, A1.forward * dist);
        Gizmos.DrawRay(A2.position, A2.forward * dist);

        Gizmos.DrawSphere(Center,1);
    }
}
