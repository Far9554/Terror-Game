using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] List<InstalationController> Instalations;
    float Loaded;

    [Header("UI")]
    [SerializeField] TMP_Text Log;
    [SerializeField] Image Barra;
    [SerializeField] GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Barra.fillAmount = Loaded;
    }
}
