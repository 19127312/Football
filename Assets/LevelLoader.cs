using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject LoadingScreen;
    public Slider LoadingBarFill;



    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneId)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        if (LoadingScreen)
        {
            LoadingScreen.SetActive(true);
        }


        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / .9f);
            if (LoadingBarFill)
            {
                LoadingBarFill.value = progressValue;
            }


            yield return null;
        }
    }
}
