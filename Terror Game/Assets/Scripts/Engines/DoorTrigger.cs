using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorTrigger : MonoBehaviour
{
    public DoorController door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("IA")) { if (door.isOpen) { door.Activate(); Debug.Log("Abriendo"); } }
    }
}
