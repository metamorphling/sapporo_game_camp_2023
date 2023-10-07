using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bug : MonoBehaviour
{
    RectTransform rect;
    float posX;
    float posY;
    float r;
    protected int health = 0;
   [SerializeField] protected TMPro.TMP_Text text;

    virtual public void Initialize(int health)
    {
        rect = GetComponent<RectTransform>();
        text.text = health.ToString();
        this.health = health;
        this.gameObject.SetActive(true);
        r = Random.Range(0, 350);
        posX = r;
        posY = r;
        rect.localPosition = new Vector3(posX, posY, 0);
    }
}
