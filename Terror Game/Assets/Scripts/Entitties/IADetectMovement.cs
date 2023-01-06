using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class IADetectMovement : MonoBehaviour
{
    public BasicIA IA;
    [SerializeField] int detectingdistanceClose;
    [SerializeField] int detectingdistanceFar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectMovement();
    }

    void DetectMovement()
    {
        //if (Physics.Linecast(transform.position, IA.playerController.transform.position)) { return; }
        if (Physics.CheckSphere(transform.position, detectingdistanceClose, LayerMask.GetMask("Player"))) { CheckVelocity(true); CameraShaker.Instance.ShakeOnce(1.5f, 1.5f, .1f, .1f); }
        if (Physics.CheckSphere(transform.position, detectingdistanceFar, LayerMask.GetMask("Player"))) { CheckVelocity(false); CameraShaker.Instance.ShakeOnce(0.5f, 0.5f, .1f, .1f); }
    }

    void CheckVelocity(bool isClose)
    {
        if (isClose && IA.playerController.moveSpeed > 1)
        {
            if (IA.lostPlayer == 0) { IA.Alerted = true; IA.lostPlayer = 10; }
            else if (IA.lostPlayer > 0 && IA.lostPlayer <= 8) { IA.Detected = true; IA.lostPlayer = 10; }
        }
        else if (!isClose && IA.playerController.moveSpeed > 4)
        {
            if (IA.lostPlayer == 0) { IA.Alerted = true; IA.lostPlayer = 10; }
            else if (IA.lostPlayer > 0 && IA.lostPlayer <= 8) { IA.Detected = true; IA.lostPlayer = 10; }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectingdistanceClose);
        Gizmos.DrawWireSphere(transform.position, detectingdistanceFar);
    }
}
