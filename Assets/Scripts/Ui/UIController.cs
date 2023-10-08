using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text hpUI;
    [SerializeField] TMPro.TMP_Text scoreUI;
    [SerializeField] TMPro.TMP_Text timerUI;

    public static UIController Main { get; private set; }

    private void Start()
    {
        if(Main == null)
        {
            Main = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
