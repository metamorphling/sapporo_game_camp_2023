using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AdditiveSceme : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("追加シーンAwake");
        if (!Camera.main)
        {
            GameObject camera = new GameObject("Main Camera");
            camera.AddComponent<Camera>();
        }
        if (!EventSystem.current)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }
    }
}
