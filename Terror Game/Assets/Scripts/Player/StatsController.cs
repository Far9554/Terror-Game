using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsController : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] PlayerMovement player;
    public GameObject StatViwer;
    public GameObject Lantern;
    public MouseLook MouseLook;
    public AudioSource audio;

    [Header("Variables")]
    public bool isStatsOn;
    public bool isLightOn;

    [Header("Utilities")]
    public int amountBatteries=0;
    public int amountCintas=0;

    [Header("Stats")]
    [SerializeField] float oxigeno = 100;
    [SerializeField] float stamina = 100;
    [SerializeField] float live = 100;

    [Header("UIAlerts")]
    public GameObject Energie;
    public GameObject Oxigen;
    public GameObject LowHeatlhEnergie;
    public GameObject LowHeatlhOxigen;

    [Header("UI")]
    public Image Oxigeno;
    public Image Stamina;
    public Image Suit;

    [Header("Sounds")]
    public AudioClip Replace;
    public AudioClip Cinta;
    public AudioClip LanternSwitch;

    void Update()
    {
        live = player.health;

        if (Input.GetButtonDown("Stats")) { InteractStats(); } if (Input.GetButtonDown("Lantern")) { InteractLantern(); }
        if (Input.GetButton("Sprint")) { stamina -= 1 * Time.deltaTime; }

        CheckStats();
        UpdateStats();

        if (isLightOn) { stamina -= 1 * Time.deltaTime; }
        
    }

    public void ReplaceBatterie()
    {
        if (amountBatteries <= 0) { return; }

        audio.clip = Replace;
        audio.Play();
        stamina = 100;
        amountBatteries--;
    }

    public void FixSuit()
    {
        if (amountCintas <= 0) { return; }

        player.health = 100;
        amountCintas--;
    }

    public void GetBatterie() { amountBatteries++; audio.clip = Replace; audio.Play(); }
    public void GetCinta() { amountCintas++; }

    void InteractStats()
    {
        isStatsOn = !isStatsOn;
        if (isStatsOn) { 
            StatViwer.SetActive(true);
            MouseLook.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else { 
            StatViwer.SetActive(false);
            MouseLook.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void CheckStats()
    {
        if (player.isUnderWater) { oxigeno -= 1 * Time.deltaTime; } else { oxigeno += 1.5f * Time.deltaTime; }
        if (live <= 75 && player.isUnderWater) { oxigeno -= 1 * Time.deltaTime; } 
        if (live <= 50) { stamina -= 1 * Time.deltaTime; }

        if (oxigeno > 100) { oxigeno = 100; } else if (oxigeno < 0) { oxigeno = 0; }
        if (stamina > 100) { stamina = 100; } else if (stamina < 0) { stamina = 0; }

        if (amountBatteries == 0) { Energie.SetActive(true); } else { Energie.SetActive(false); }
        if (oxigeno <= 25) { Oxigen.SetActive(true); } else { Oxigen.SetActive(false); }
        if (stamina == 0) { Lantern.SetActive(false); }
        if (live <= 75) { LowHeatlhOxigen.SetActive(true); } else { LowHeatlhOxigen.SetActive(false); }
        if (live <= 50) { LowHeatlhEnergie.SetActive(true); } else { LowHeatlhEnergie.SetActive(false); }
    }

    void UpdateStats() { Oxigeno.fillAmount = oxigeno / 100; Stamina.fillAmount = stamina / 100; Suit.fillAmount = live / 100; }

    void InteractLantern()
    {
        if (stamina == 0) { return; }
        isLightOn = !isLightOn;
        if (isLightOn) { Lantern.SetActive(true); } else { Lantern.SetActive(false); }
        audio.clip = LanternSwitch;
        audio.Play();
    }
}
