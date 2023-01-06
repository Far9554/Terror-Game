using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] GameObject AmbientSource;
    [SerializeField] AudioSource Breath;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isUnderWater) { AmbientSource.SetActive(true); Breath.volume = 1; } else { AmbientSource.SetActive(false); Breath.volume = 0.25f; }

        if (!Breath.isPlaying)
        {
            float r = Random.Range(0.75f, 1.25f);
            Breath.pitch = r;
            Breath.Play();
        }
    }
}
