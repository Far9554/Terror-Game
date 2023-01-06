using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeakController : MonoBehaviour
{
    public float TiempoDeLerp = 0.05f;
    public Transform PuntoAInclinar;
    public Vector3 Izquierda = new Vector3(0, 0, 30);
    public Vector3 Derecha = new Vector3(0, 0, -30);

    private Vector3 rotacionActual;

    void Update()
    {
        if (Input.GetButton("PeakLeft")) { rotacionActual = Izquierda; }
        else if (Input.GetButton("PeakRight")) { rotacionActual = Derecha; }
        else { rotacionActual = new Vector3(0, 0, 0); }

        PuntoAInclinar.localRotation = Quaternion.Lerp(PuntoAInclinar.localRotation, Quaternion.Euler(rotacionActual), TiempoDeLerp);
    }
}
