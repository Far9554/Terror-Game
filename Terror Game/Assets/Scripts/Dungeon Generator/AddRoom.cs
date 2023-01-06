using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    [Header("Listas")]
    public InstalationController instalation;
    RoomTemplates Room;
    BaseTemplate Base;

    public GameObject Biga;
    Ray ray;

    Vector3 originPos;
    Vector3 targetPos;

    [Header("Caracteristicas")]
    public bool isBase=false;
    public int image=0;

    public bool isCorrupted=false;
    public int LevelCorruption = 0;
    public List<GameObject> Corruptions;

    void Start()
    {
        originPos = transform.position - new Vector3(0, 2, 0);
        targetPos = transform.position - new Vector3(0, 8, 0);

        if (instalation == null) { return; }
        Corrupted();
        GenerateCorruption();

        if (isBase) { instalation.Habitaciones.Add(this.gameObject); }
        else { instalation.Tubos.Add(this.gameObject); }

        instalation.AddtoMinimap(transform, image, isBase);

        Invoke("DisableBoxCollider", 10f);
        Invoke("GenerateBiga", 5f);
    }

    void Corrupted()
    {
        int r = Random.Range(0, 100);
        if (r <= instalation.Corruption) { isCorrupted = true; LevelCorruption++; }
        else if (r > instalation.Corruption) { isCorrupted = true; LevelCorruption--; }

        if (LevelCorruption <= 0) { LevelCorruption = 0; isCorrupted = false; }
        else if (LevelCorruption > 5) { LevelCorruption = 5; }
    }

    void GenerateCorruption()
    {
        if (LevelCorruption == 0 || Corruptions.Count < 4) { return; }

        Corruptions[LevelCorruption - 1].SetActive(true);
    }

    void GenerateBiga()
    {
        if (Biga == null) return;
        ray = new Ray(originPos, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit info, 20)) {
            if (info.transform.gameObject.layer == 8)
            {
                if (Vector3.Distance(originPos, info.point) >= 13.5f) { Instantiate(Biga, transform.position - new Vector3(0, 15.1f, 0), transform.rotation, transform); }
                if (Vector3.Distance(originPos, info.point) >= 8.5f) { Instantiate(Biga, transform.position - new Vector3(0, 10.1f, 0), transform.rotation, transform); }
                if (Vector3.Distance(originPos, info.point) >= 4.5f) { Instantiate(Biga, transform.position - new Vector3(0, 5.1f, 0), transform.rotation, transform); }
                if (Vector3.Distance(originPos, info.point) >= 0.5f) { Instantiate(Biga, transform.position - new Vector3(0, 0.1f, 0), transform.rotation, transform); }
            }
            else
            {
                if (Vector3.Distance(originPos, info.point) >= 15.5f) { Instantiate(Biga, transform.position - new Vector3(0, 15.1f, 0), transform.rotation, transform); }
                if (Vector3.Distance(originPos, info.point) >= 11.5f) { Instantiate(Biga, transform.position - new Vector3(0, 10.1f, 0), transform.rotation, transform); }
                if (Vector3.Distance(originPos, info.point) >= 7.5f) { Instantiate(Biga, transform.position - new Vector3(0, 5.1f, 0), transform.rotation, transform); }
                if (Vector3.Distance(originPos, info.point) >= 3.5f) { Instantiate(Biga, transform.position - new Vector3(0, 0.1f, 0), transform.rotation, transform); }
            }
            
        }
    }

    void DisableBoxCollider()
    {
        if (GetComponent<BoxCollider>()) { GetComponent<BoxCollider>().enabled = false; }  
    }
}
