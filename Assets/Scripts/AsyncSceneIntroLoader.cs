using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneIntroLoader : MonoBehaviour
{
    [SerializeField] AudioSource audio;

    private void Start()
    {
        LoadMenu();
    }

    public void LoadMenu()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Menu");
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            if (!audio.isPlaying)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}