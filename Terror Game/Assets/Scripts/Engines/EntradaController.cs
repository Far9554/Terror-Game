using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaController : MonoBehaviour
{
    public bool isUnderWater;
    public bool PlayerUnderWater;

    public DoorController DoorInterior;
    public DoorController DoorExterior;

    public bool OpenDoor1;
    public bool OpenDoor2;

    [SerializeField] private Animator anim;
    PlayerMovement player;

    public bool Finish=true;

    public GameObject Button;

    public AudioSource audioSource;
    public AudioClip ButtonClick;

    [Header("Particulas")]
    public bool activateParticle;
    public ParticleSystem Burbujas;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        audioSource.GetComponent<AudioSource>();
        ActivateButton();
    }

    // Update is called once per frame
    void Update()
    {
        ChangePostVolPlayer();
        ControlBurbujas();

        if (OpenDoor1) { DoorInterior.isOpen = true; } else { DoorInterior.isOpen = false; }
        DoorInterior.Activate();
        if (OpenDoor2) { DoorExterior.isOpen = true; } else { DoorExterior.isOpen = false; }
        DoorExterior.Activate();
    }

    public void ActivateButton()
    {
        if (Finish==false) { return; }
        Finish = false;

        //audioSource.clip = ButtonClick;
        //audioSource.Play();

        if (isUnderWater && !Finish) { OpenDoor(); } else if (!Finish) { CloseDoor(); }

        isUnderWater = !isUnderWater;
    }

    void OpenDoor()
    {
        Finish = true;
        anim.SetTrigger("Open");
        if (Button) Button.GetComponent<SpriteRenderer>().color = Color.green;
    }

    void CloseDoor()
    {
        Finish = true;
        anim.SetTrigger("Close");
        if (Button) Button.GetComponent<SpriteRenderer>().color = Color.red;
    }

    void ChangePostVolPlayer()
    {
        if (!Physics.CheckSphere(transform.position, 5, LayerMask.GetMask("Player"))) { return; }
        player.isUnderWater = PlayerUnderWater;
    }

    void ControlBurbujas()
    {
        if (activateParticle) { Burbujas.Play(); } else { Burbujas.Stop(); }
    }
}
