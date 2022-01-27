using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] private string nextScene;
    public IEnumerator LoadScene(float exitTime = 0)
    {
        yield return new WaitForSeconds(exitTime);

        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }

    public IEnumerator LoadScene(string sceneName, float exitTime = 0)
    {
        yield return new WaitForSeconds(exitTime);

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
