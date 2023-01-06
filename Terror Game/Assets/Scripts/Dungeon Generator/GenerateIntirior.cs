using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateIntirior : MonoBehaviour
{
    public List<GameObject> Interiors;
    private InstalationController instalation;

    int r;

    // Start is called before the first frame update
    void Start()
    {
        instalation = GetComponent<AddRoom>().instalation;
        Generate();
    }

    void Generate()
    {
        r = Random.Range(0, Interiors.Count - 1);
        instalation.HabitacionesR[r] = true;
        Interiors[r].SetActive(true);
    }
}
