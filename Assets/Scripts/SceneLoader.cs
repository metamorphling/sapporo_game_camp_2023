using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// NextScneNameのシーンをスペースキーまたはクリックでロードする（ロードされているシーンはアンロードされる）
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [SerializeField] string NextSceneName = "Menu";

    /// <summary>
    /// timer秒後に自動でロードするか
    /// </summary>
    [SerializeField] bool HasExitTime = false;
    [SerializeField] float timer = 3f;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)|Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(NextSceneName, LoadSceneMode.Single);
        }

        if(HasExitTime)
        {
            timer-=(float)Time.deltaTime;

            if (timer < 0)
            {
                SceneManager.LoadScene(NextSceneName, LoadSceneMode.Single);
            }
        }
    }
}
