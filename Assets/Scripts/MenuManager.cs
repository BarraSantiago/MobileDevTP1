using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public List<CinemachineVirtualCamera> vcams;
    public List<GameObject> panels;
    public List<GameObject> objects;

    [SerializeField] DifficultyScriptableObject difficulty;
    public MultiplayerScriptableObject multiplayer;

    private void Start()
    {
        Time.timeScale = 1;
        GameManager.OnEndgame += ChangeToEndGame;
    }

    private void OnDestroy()
    {
        GameManager.OnEndgame -= ChangeToEndGame;
    }

    private IEnumerator ActivatePanel(CinemachineVirtualCamera camera)
    {
        float time = 0;
        while (time < 2)
        {
            time += Time.deltaTime;
            yield return null;
        }
        for(int i = 0; i < vcams.Count; i++)
        {
            if(vcams[i] == camera)
            {
                panels[i].SetActive(true);
            }
        }
        if(camera == vcams[0])
        {
            foreach (GameObject obj in objects)
            {
                obj.SetActive(false);
            }
        }
        yield return null;
    }

    public void ActivateCamera(CinemachineVirtualCamera camera)
    {
        for (int i = 0; i < vcams.Count; i++)
        {
            if (vcams[i] == camera)
            {
                if (i != 0)
                {
                    objects[i - 1].SetActive(true);
                }
            }
        }
        foreach (CinemachineVirtualCamera vcam in vcams)
        {
            vcam.gameObject.SetActive(false);
        }
        foreach(GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        camera.gameObject.SetActive(true);
        StartCoroutine(ActivatePanel(camera));
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ChangeDifficulty(int diff)
    {
        difficulty.currentDifficulty = (Difficulty)diff;
    }

    public void ChangeMultiplayer(bool multi)
    {
        multiplayer.isMultiplayer = multi;
    }

    public void PauseGame()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void PauseGame(GameObject panel)
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        panel.SetActive(!panel.activeSelf);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeToEndGame()
    {
        if (multiplayer.isMultiplayer)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(5);
        }
    }
}