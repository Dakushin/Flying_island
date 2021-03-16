using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Build.Content;
using TMPro;

public class loaderscenehub : MonoBehaviour
{
    public GameObject loadingScreen;

    public Slider slider;
    public Text ProgressText;
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(loadAsynchronously(sceneIndex));
    }

    IEnumerator loadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(progress);

            slider.value = progress;
            ProgressText.text = progress * 100f + "%";

            yield return null;
        }
    }
}