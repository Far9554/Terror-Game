using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    RaycastHit hit;
    public Transform cam;
    public StatsController stats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAtUtility();
        if (Input.GetButtonDown("Interaction")) { PressBotton(); }
    }

    void PressBotton()
    {
        if (Physics.Raycast(cam.position, direction: cam.forward, out hit, 20))
        {
            if (hit.transform.GetComponent<DoorController>()) { hit.transform.GetComponent<DoorController>().Activate(); }
            if (hit.transform.GetComponent<EntradaController>()) { hit.transform.GetComponent<EntradaController>().ActivateButton(); }
            if (hit.transform.GetComponent<GeneratorController>()) { hit.transform.GetComponent<GeneratorController>().On(); }
            if (hit.transform.GetComponent<Ascensor>()) { hit.transform.GetComponent<Ascensor>().ChangePos(); }
            if (hit.collider.TryGetComponent<Door>(out Door door)) { if (door.IsOpen) { door.Close(); } else { door.Open(transform.position); } }
            if (hit.transform.CompareTag("Batterie")) { stats.GetBatterie(); Destroy(hit.transform.gameObject); }
            if (hit.transform.CompareTag("Cinta")) { stats.GetCinta(); Destroy(hit.transform.gameObject); }
        }
    }

    void LookAtUtility()
    {
        if (Physics.Raycast(cam.position, direction: cam.forward, out hit, 20))
        {
            if (hit.collider.TryGetComponent<UtilityController>(out UtilityController utility)) { utility.isSelected = true; }
        }
    }
}
