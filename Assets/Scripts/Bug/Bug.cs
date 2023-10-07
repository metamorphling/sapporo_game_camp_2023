using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bug : MonoBehaviour
{
    public int damage;
    public int score;
    [SerializeField] protected Sprite lose;
    [SerializeField] protected Sprite attack;
    [SerializeField] protected Sprite normal;
    [SerializeField] protected TMPro.TMP_Text text;
    protected Image image;
    protected RectTransform rect;
    protected float posX;
    protected float posY;
    protected float r1;
    protected float r2;
    protected int health = 0;

    protected IEnumerator BugLoose()
    {
        if (health <= 0)
        {
            health = 0;
            image.sprite = lose;
        }
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }

    virtual public void Initialize(int health)
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        image.sprite = normal;
        text.text = health.ToString();
        this.health = health;
        this.gameObject.SetActive(true);
        r1 = Random.Range(-800, 800);
        r2 = Random.Range(0, 350);
        posX = r1;
        posY = r2;
        rect.localPosition = new Vector3(posX, posY, 0);
    }
}
