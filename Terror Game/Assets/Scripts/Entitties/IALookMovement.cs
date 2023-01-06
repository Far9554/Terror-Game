using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IALookMovement : MonoBehaviour
{
    public BasicIA IA;
    Transform Target;
    public Renderer m_Renderer;
    public GameObject Light;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        //m_Renderer = GetComponentInChildren<Renderer>();
        Target = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        DetectIsVisible();
    }

    void DetectIsVisible()
    {
        if (m_Renderer.isVisible) {
            if (Physics.Linecast(transform.position, IA.playerController.transform.position)) { return; }
            IA.anim.speed = 0; IA.IA.speed = 0; IA.CanMove = false; Light.SetActive(false); 
        }
        else { IA.anim.speed = 1; IA.IA.speed = IA.IASpeed; IA.CanMove = true; Light.SetActive(true); }

        if (Physics.CheckSphere(transform.position, 20, LayerMask.GetMask("Player"))) { IA.lostPlayer = 5;  IA.Detected = true; }
    }
}
