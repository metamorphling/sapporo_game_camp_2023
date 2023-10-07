using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    protected int health = 0;
   [SerializeField] protected TMPro.TMP_Text text;

    virtual public void Initialize(int health)
    {
        text.text = health.ToString();
        this.health = health;
        this.gameObject.SetActive(true);
    }
}
