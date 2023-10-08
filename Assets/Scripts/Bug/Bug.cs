using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        if (isDead)
        {
            yield break;
        }

        transform.DOShakeRotation(0.3f, 100, 100);
        isDead = true;
        health = 0;
        image.sprite = lose;
        AS.PlayOneShot(killSE);
        yield return new WaitForSeconds(1);
        AS.PlayOneShot(breakSE);
        transform.DOScale(0, 0.2f).SetEase(Ease.InSine).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
            if(TypingSceneController.Main)
            {
                TypingSceneController.Main.Score += score;
            }
            transform.localScale = new Vector3(1, 1, 1);
        });
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
        transform.DOScale(4, 0.2f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            transform.DOShakePosition(0.4f, 50, 40).OnComplete(() =>
            {
                transform.DOScale(0, 0.4f).SetEase(Ease.InSine);
            });
        });
        if (TypingSceneController.Main)
            TypingSceneController.Main.HP -= damage;
        StartCoroutine(BugAttack());
    }

    /// <summary>
    /// healthÇ™0ÇÃéûÇÕèâä˙ílÇ™ê›íËÇ≥ÇÍÇÈ
    /// </summary>
    /// <param name="health"></param>
    public virtual void Initialize(int health)
    {
        if (health == 0)
            health = startHealth;
        AS = GetComponent<AudioSource>();
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        image.sprite = normal;
        text.text = "HP: " + health.ToString();
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
        transform.localScale = new Vector3(1, 1, 1);
        transform.rotation = Quaternion.Euler(0,0,0);
        transform.DOShakePosition(AttackTimer, 20, 30);
    }
}