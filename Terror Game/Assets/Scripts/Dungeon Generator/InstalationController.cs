using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InstalationController : MonoBehaviour
{
    [Header("Control Habitaciones")]
    public List<GameObject> Habitaciones;
    public int MaxRooms;
    public int MinRooms;
    [Header("Control Tubos")]
    public List<GameObject> Tubos;
    public int MaxTubos;
    public int MinTubos;

    int CountHabi=0;
    int CountTubos=0;

    [Header("MiniMapa")]
    public GameObject canvas;

    public List<GameObject> ImagenHabitaciones;
    public List<GameObject> ImagenTubos;

    public GameObject Pos;

    [Header("Salas Espciales")]
    public List<bool> HabitacionesR;

    [Header("Generador Luz")]
    public List<GameObject> Ubicaciones;
    public GameObject Generador;
    public List<LightController> LucesExtras;

    [Header("Estadisticas")]
    [Range(0, 100)] public int Corruption = 0;
    float waitTime = 8;
    bool ended = false;
    public bool ended2 = false;

    [Header("Collectives")]
    [SerializeField] int qttB;
    [SerializeField] int qttC;
    [SerializeField] GameObject Bateria, Cinta;
    [SerializeField] List<Transform> SpawnsCollectives;

    [Header("NavMesh")]
    public NavMeshSurface surface;
    public GameObject Monster;

    private void Update()
    {
        waitTime -= 1 * Time.deltaTime;

        if (waitTime <= 2 && ended2 == false) { ended2 = true; GenerateMiniMaps(); GetSpawnCollectives(); SpawnMonster(); }
        if (waitTime <= 0 && ended == false) { ended = true; SearchList(); GenerateSurface(); }
    }

    public void AddtoMinimap(Transform pos, int image, bool isRoom)
    {
        waitTime = 8;
        GameObject createImage;

        if (isRoom) { createImage = Instantiate(ImagenHabitaciones[image]) as GameObject; CountHabi++; }
        else { createImage = Instantiate(ImagenTubos[image]) as GameObject; CountTubos++; }
        
        createImage.transform.SetParent(canvas.transform, false);

        createImage.GetComponent<RectTransform>().localPosition = new Vector3((pos.localPosition.x / 50) + (pos.localPosition.y/1.5f) - 4, (pos.localPosition.z / 50), 0);
    }

    void GenerateMiniMaps()
    {
        for (int i =0; i < Habitaciones.Count-1; i++)
        {
            if (Habitaciones[i].GetComponentInChildren<InsertMiniMap>())
            {
                Habitaciones[i].GetComponentInChildren<InsertMiniMap>().InstantiateMiniMap(canvas, Pos, Habitaciones[i].transform);
            }
        }
    }

    void GenerateSurface()
    {
        if (surface != null) { Debug.Log("Generating Surface");  surface.BuildNavMesh(); }
    }

    public void SearchList()
    {
        int dir=0;
        foreach (GameObject T in Tubos)
        {
            if (T.name.StartsWith("T(")) { Ubicaciones.Add(T); }
            else if (T.name.StartsWith("B(")) { Ubicaciones.Add(T); }
            else if (T.name.StartsWith("L(")) { Ubicaciones.Add(T); }
            else if (T.name.StartsWith("R(")) { Ubicaciones.Add(T); }
        }

        if (Ubicaciones.Count != 0)
        {
            int r;
            if (Ubicaciones.Count == 1) { r = 0; }
            else { r = Random.Range(0, Ubicaciones.Count - 1); }

            GameObject Gene;

            if (Ubicaciones[r].name.StartsWith("T(")) { dir = 90; }
            else if (Ubicaciones[r].name.StartsWith("B(")) { dir = 270; }
            else if (Ubicaciones[r].name.StartsWith("L(")) { dir = 0; }
            else if (Ubicaciones[r].name.StartsWith("R(")) { dir = 180; }

            Gene = Instantiate(Generador, Ubicaciones[r].transform.position, Quaternion.Euler(0, dir, 0), Ubicaciones[r].transform);
            Gene.GetComponent<GeneratorController>().instalation = this;
            Gene.transform.localRotation = Quaternion.Euler(0, dir, 0);
        }
    }

    void GetSpawnCollectives()
    {
        TaquillaController[] SpaCol = GetComponentsInChildren<TaquillaController>();

        foreach (TaquillaController T in SpaCol) { SpawnsCollectives.AddRange(T.PosObj); }

        GenerateCollectives();
    }

    void GenerateCollectives()
    {
        for(int i=0; i < qttB; i++)
        {
            if (SpawnsCollectives.Count == 0) { return; }
            int r = Random.Range(0, SpawnsCollectives.Count - 1);
            Instantiate(Bateria, SpawnsCollectives[r]);
            SpawnsCollectives.RemoveAt(r);
        }

        for (int i = 0; i < qttC; i++)
        {
            if (SpawnsCollectives.Count == 0) { return; }
            int r = Random.Range(0, SpawnsCollectives.Count - 1);
            Instantiate(Cinta, SpawnsCollectives[r]);
            SpawnsCollectives.RemoveAt(r);
        }
    }

    void SpawnMonster()
    {
        if (Monster == null) { return; }

        GameObject M = Instantiate(Monster, transform.position, transform.rotation);
        M.GetComponent<BasicIA>().instalation = this;
        foreach(GameObject Habi in Habitaciones) { if (!Habi.name.StartsWith("BASE")) { M.GetComponent<BasicIA>().PosHabi.Add(Habi); } }
        int r = Random.Range(0, M.GetComponent<BasicIA>().PosHabi.Count - 1);
        M.transform.position = Habitaciones[r].transform.position + new Vector3(0,0.5f,0);
        M.GetComponent<BasicIA>().playerController = FindObjectOfType<PlayerMovement>();
    }
}
