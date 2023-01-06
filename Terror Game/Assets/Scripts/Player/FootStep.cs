using UnityEngine;
using System.Collections;

public class FootStep : MonoBehaviour {
	
	public AudioClip[] SonidoMetal;
	public AudioClip[] SonidoMadera;
	public AudioClip[] SonidoConcreto;
	public AudioClip[] SonidoTerreno;
    private float lowPitchRange = 0.75f;
    private float highPitchRange = 1.25f;
    private PlayerMovement controller;
    private AudioSource source;
    private Rigidbody rb;
	private float delayTime;
	private float NextPlay;
	
	void PlayFootStepSound(float vel)
	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position, -Vector3.up, out hit, 10f) && Time.time > NextPlay)
		{
			NextPlay = delayTime + Time.time;
			if (vel > 5.5f) { delayTime = Random.Range(0.25f, 0.35f); }
			else if (vel > 3.5f) { delayTime = Random.Range(0.45f, 0.55f); }
			else if (vel > 1f) { delayTime = Random.Range(0.65f, 0.75f); }
			else if (vel < 1f) { return; }
			
            source.pitch = Random.Range(lowPitchRange, highPitchRange);
            switch (hit.collider.tag)
			{
			case "metal":
				source.clip = SonidoMetal[Random.Range (0, SonidoMetal.Length)];
				source.Play();
				break;
			case "madera":
				source.clip = SonidoMadera[Random.Range(0, SonidoMadera.Length)];
				source.Play ();
				break;
			case "concreto":
				source.clip = SonidoConcreto[Random.Range(0, SonidoConcreto.Length)];
				source.Play();
				break;
			default:
				source.clip = SonidoTerreno[Random.Range (0, SonidoTerreno.Length)];
				source.Play();
				break;
			}
		}
	}
	
	// Funcion start para inicializar el juego
	void Start () {
		controller = GetComponent<PlayerMovement>();
        source = GetComponent<AudioSource>();
	}
	
	// Funcion update para determinar frame por frame y mandar llamar objetos establecidos en el constructor
	void Update () 
	{
		if (controller.isGrounded) { PlayFootStepSound(controller.moveSpeed); }
	}
}
