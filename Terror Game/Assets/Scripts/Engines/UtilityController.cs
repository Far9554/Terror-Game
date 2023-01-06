using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UtilityController : MonoBehaviour
{
    public Image imageLook;
    public Image imageSelected;
    public GameObject UI;
    public GameObject Light;

    public Transform player;

    public bool isSelected;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<MouseLook>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 3) 
        {
            if (Physics.Linecast(transform.position, player.position)) { return; }
            UI.SetActive(true);
            Light.SetActive(true);
            imageLook.transform.position = Camera.main.WorldToScreenPoint(transform.position); 
            imageSelected.transform.position = Camera.main.WorldToScreenPoint(transform.position); 
        } 
        else { UI.SetActive(false); Light.SetActive(false); }

        if (isSelected) { imageLook.enabled = false; imageSelected.enabled = true; } else { imageLook.enabled = true; imageSelected.enabled = false; }
        isSelected = false;

    }
}
