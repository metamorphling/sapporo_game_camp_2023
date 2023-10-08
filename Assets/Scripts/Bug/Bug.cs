using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bug : MonoBehaviour
{
    [SerializeField] protected AudioClip attackSE;
    [SerializeField] protected AudioClip spawnSE;
    [SerializeField] protected AudioClip breakSE;
    [SerializeField] protected AudioClip killSE;
    [SerializeField] protected AudioClip damageSE;
    [HideInInspector]
    public int health = 0;
    public int damage;
    public int attackSpeed;
    public int score;
    [HideInInspector]
    public bool isDead = true;
    [SerializeField] protected Sprite lose;
    [SerializeField] protected Sprite attack;
    [SerializeField] protected Sprite normal;
    [SerializeField] protected TMPro.TMP_Text text;
    protected AudioSource AS;
    [SerializeField] protected TMPro.TMP_Text speedText;
    protected Image image;
    protected RectTransform rect;
    protected float posX;
    protected float posY;
    protected float r1;
    protected float r2;
    protected float AttackTimer;
    [SerializeField]
    protected int startHealth;

    protected IEnumerator BugLoose()
    {
        isDead = true;
        health = 0;
        image.sprite = lose;
        AS.PlayOneShot(killSE);
        yield return new WaitForSeconds(1);
        AS.PlayOneShot(breakSE);
        this.gameObject.SetActive(false);
    }

    protected IEnumerator BugAttack()
    {
        isDead = true;
        image.sprite = attack;
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isDead)
            return;
        AttackTimer -= Time.deltaTime;
        speedText.text = AttackTimer.ToString();
        if (AttackTimer < 0)
        {
            isDead = true;
            AttackTimer = 0; 
            speedText.text = AttackTimer.ToString();
            Attack();
        }
    }

    protected void Attack()
    {
        Debug.Log("Attack! " + name  + " Damage! " + damage);
        StartCoroutine(BugAttack());
    }

    public virtual void Initialize(int health)
    {
        if (health == 0)
            health = startHealth;
        AS = GetComponent<AudioSource>();
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        image.sprite = normal;
        text.text = health.ToString();
        this.health = health;
        this.gameObject.SetActive(true);
        AS.PlayOneShot(spawnSE);
        r1 = Random.Range(-800, 800);
        r2 = Random.Range(0, 350);
        posX = r1;
        posY = r2;
        rect.localPosition = new Vector3(posX, posY, 0);
        AttackTimer = attackSpeed;
        isDead = false;
    }
}
