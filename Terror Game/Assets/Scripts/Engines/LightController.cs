using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] GameObject PointLight;
    [SerializeField] GameObject Emision;

    [SerializeField] bool StartOn=false;
    bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        if (StartOn) { On(); } else { Off(); }
        isOn = StartOn;
        //Activate();
    }

    public void Activate()
    {
        if (isOn) { Off(); isOn = false; } else { On(); isOn = true; }
    }

    void On()
    {
        PointLight.SetActive(true);
        Emision.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white * 2);
    }

    void Off()
    {
        PointLight.SetActive(false);
        Emision.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
    }
}
