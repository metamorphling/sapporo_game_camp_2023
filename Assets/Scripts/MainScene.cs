using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    void Awake()
    {
        GameManager.Initialize();

        StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("Background", LoadSceneMode.Additive);
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("Typing", LoadSceneMode.Additive);
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("Gimmick", LoadSceneMode.Additive);
    }
}
