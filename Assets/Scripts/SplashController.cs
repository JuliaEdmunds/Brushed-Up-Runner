using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour
{
    private const string MENU_SCENE = "Menu";

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(MENU_SCENE);
    }
}
