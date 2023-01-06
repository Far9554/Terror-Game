using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] Animator animator;
    public GameObject Button;
    public GameObject Button2;
    public bool isOpen;

    public AudioSource audioSource;
    public AudioClip ButtonClick;

    // Start is called before the first frame update
    void Start()
    {
        if (animator == null) { animator = GetComponent<Animator>(); }
        StartDoor();
        StartDoor();
    }

    void StartDoor() { if (isOpen) { OpenDoor(); } else { CloseDoor(); } }

    public void Activate()
    {
        if (isOpen) { OpenDoor(); } else { CloseDoor(); }
        isOpen = !isOpen;
        if (audioSource != null)
        {
            audioSource.clip = ButtonClick;
            audioSource.Play();
        }
    }

    public void ForceOpen()
    {
        isOpen = true;
        audioSource.clip = ButtonClick;
        audioSource.Play();
        OpenDoor();
    }

    void OpenDoor()
    {
        animator.SetTrigger("Open");
        if (Button)  Button.GetComponent<SpriteRenderer>().color = Color.green;
        if (Button2) Button2.GetComponent<SpriteRenderer>().color = Color.green;
    }

    void CloseDoor()
    {
        animator.SetTrigger("Close");
        if (Button)  Button.GetComponent<SpriteRenderer>().color = Color.red;
        if (Button2) Button2.GetComponent<SpriteRenderer>().color = Color.red;
    }
}
