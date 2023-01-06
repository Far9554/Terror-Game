using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    [SerializeField] Animator anim;
    public InstalationController instalation;
    [SerializeField] List<GameObject> Salas;
    [SerializeField] List<LightController> Luces;
    // Start is called before the first frame update
    void Start()
    {
        if (anim == null) GetComponent<Animator>();
        if (instalation) { 
            Salas.AddRange(instalation.Habitaciones); 
            Salas.AddRange(instalation.Tubos); 

            Invoke("GetLuces", 1f);
            
        }
    }

    public void On()
    {
        anim.SetTrigger("On");
        OnAllLights();
    }

    void GetLuces()
    {
        for (int i = 0; i < Salas.Count - 1; i++)
        {
            Luces.AddRange(Salas[i].GetComponentsInChildren<LightController>());
        }
        Invoke("EliminarLuces", 1f);
    }

    void EliminarLuces()
    {
        int qttLuces = Luces.Count/4;

        for (int i =0; i < qttLuces; i++)
        {
            int r = Random.Range(0,Luces.Count - 1);
            Luces.RemoveAt(r);
        }
        Luces.AddRange(instalation.LucesExtras);
    }

    void OnAllLights()
    {
        foreach(LightController light in Luces)
        {
            light.Activate();
        }
    }
}
