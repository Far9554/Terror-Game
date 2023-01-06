using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;

    [Header("WindowsMenu")]
    public GameObject MenuUI;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

    [Header("Player Settings")]
    public PlayerMovement player;
    public MouseLook mouseLook;
    private Scene scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameIsPaused) { Cursor.lockState = CursorLockMode.None; }

        if (Input.GetButtonDown("Pause")) { if (GameIsPaused) { Resume(); }  else { Pause(); } }
    }

    public void Resume()
    {
        if (MenuUI) MenuUI.SetActive(false);
        if (pauseMenuUI) pauseMenuUI.SetActive(false);
        if (optionsMenuUI) optionsMenuUI.SetActive(false);

        mouseLook.enabled = true;

        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
        player.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        Cursor.visible = true;
        player.enabled = false;
        Cursor.lockState = CursorLockMode.None;

        if (MenuUI) MenuUI.SetActive(true);
        if (pauseMenuUI) pauseMenuUI.SetActive(true);
        if (optionsMenuUI) optionsMenuUI.SetActive(false);

        mouseLook.enabled = false;

        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1f;
    }

    public void QuitGame() { SceneManager.LoadScene(0); }
}
