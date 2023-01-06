using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ascensor : MonoBehaviour
{
    public List<Transform> Pos;

    public GameObject Plataforma;

    [SerializeField] float speed;
    [SerializeField] int GOPOS=0;
    [SerializeField] int ButtonGo=-1;

    private AudioSource source;
    public AudioClip clip;
    private Animator anim;

    void Start() { 
        if (GetComponentInParent<AudioSource>()) source = GetComponentInParent<AudioSource>();
        if (GetComponentInParent<Animator>()) anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Plataforma.transform.position = Vector3.MoveTowards(Plataforma.transform.position, Pos[GOPOS].position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, Pos[GOPOS].position) > 0.01 || Vector3.Distance(transform.position, Pos[GOPOS].position) < -0.01)
        {
            //source.clip = clip;
            //source.Play();
            anim.SetTrigger("Open");
        }
        else { 
            //source.Stop();
            anim.SetTrigger("Close");
        }

    }

    public void ChangePos()
    {
        if (ButtonGo != -1) { GOPOS = ButtonGo; return; }
        GOPOS++;
        if (GOPOS > Pos.Count-1) { GOPOS = 0; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") { other.transform.parent = this.transform; }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") { other.transform.parent = null; }
    }
}
