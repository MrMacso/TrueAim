using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    public void LoadSceneRaw(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
    public void LoadSceneTransition(int sceneNum)
    {
        StartCoroutine(LoadWithTransition(sceneNum));
    }
    IEnumerator LoadWithTransition(int sceneNum)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneNum);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void SetTimeScale(int time)
    {
        Time.timeScale = time;
    }
}
