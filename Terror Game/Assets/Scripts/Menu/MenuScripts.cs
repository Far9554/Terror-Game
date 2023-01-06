using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour
{
    [SerializeField] Animator fadeOff;
    [SerializeField] bool StartOn;

    private void Start() { if (StartOn) { fadeOff.SetTrigger("On"); } else { fadeOff.SetTrigger("Off"); } }

    public void StartGame() { fadeOff.SetTrigger("Off"); StartCoroutine(LoadAsynchronously(1)); }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(operation.progress);

            yield return null;
        }
    }

    public void QuitGame() { Application.Quit(); }
}
