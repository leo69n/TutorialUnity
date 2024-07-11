using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingScene : MonoBehaviour
{
    public TextMeshProUGUI LoadingText;
    void Start()
    {
        StartCoroutine(LoadYourAsyncScene());
    }
    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(ApplicationVariables.LoadingSceneName);
        asyncLoad.allowSceneActivation = false; // after scene is loaded, don't activate it

        while (asyncLoad.progress < 0.9f) // asyncLoad.progress max is 0.9f
        {
            LoadingText.text = "Loading..." + Mathf.RoundToInt(asyncLoad.progress * 100);
            yield return null; //keep waiting while scene is loading
        }

        LoadingText.text = "Loading...100%";
        yield return new WaitForSeconds(2);
        asyncLoad.allowSceneActivation = true;
    }
}
