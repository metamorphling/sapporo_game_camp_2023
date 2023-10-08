using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HighBugTyphoon : Bug
{
    Vector2 pos;   //マウスクリック時の位置
    Quaternion rotation;//クリックしたときのターゲットの角度

    Vector2 vecA;       //ターゲット中心からのposへのベクトル
    Vector2 vecB;       //ターゲットの中心から現マウス位置へのベクトル

    float angle;        //vecAとvecBのなす角度
    float prevAngle;        //vecAとvecBのなす角度
    float angleDiff;
    Vector3 AxB;        //vecAとvecBの外積
    Vector3 prevAxB;        //vecAとvecBの外積

    public RectTransform canvasRect;
    bool Drag;          //ドラッグ中のフラグ
    bool needLeft;

    void Start()
    {

    }

    void Update()
    {
        if (isDead)
            return;

        {   // ATTACK
            AttackTimer -= Time.deltaTime;
            speedText.text = AttackTimer.ToString();
            if (AttackTimer < 0)
            {
                isDead = true;
                AttackTimer = 0;
                speedText.text = AttackTimer.ToString();
                Attack();
                return;
            }
        }

        if (Drag)
        {
            Rotate();
        }
        else
        {
            pos = WorldPos();//マウス位置をワールド座標で取得
        }
    }

    public void OnClickBag2()   //マウスをクリックしたときの処理
    {
        //クリック時のマウスの初期位置とターゲットの現在の角度を取得
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            rotation = this.transform.rotation;
            Drag = true;
        }
    }

    void Rotate()   //ドラッグ中の動き
    {
        //ドラッグが解除されたらフラグをOFF
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Drag = false;
            return;
        }
        Vector3 trPos = this.transform.position;

        //マウスの初期位置のベクトルを求める
        vecA = (pos - (Vector2)trPos).normalized;

        //現マウス位置のベクトルを求める
        vecB = (WorldPos() - trPos).normalized;

        //マウスの初期位置と現マウス位置から角度と外積を求める
        angle = Vector2.Angle(vecA, vecB);  //角度を計算
        AxB = Vector3.Cross(vecA, vecB);    //外積を計算

        bool isLeft = ((AxB.z > 0) && (prevAngle < angle)) || ((AxB.z < 0) && (prevAngle > angle));
        if (isLeft != needLeft)
            return;

        //外積のz成分の正負で回転方向を決める
        if (AxB.z > 0)
        {
            //初期位置との掛け算で相対的に回転させる
            this.transform.localRotation = rotation * Quaternion.Euler(0, 0, angle);
        }
        else
        {
            this.transform.localRotation = rotation * Quaternion.Euler(0, 0, -angle);
        }
        angleDiff += Mathf.Abs(angle - prevAngle);
        if (angleDiff > 360)
        {
            angleDiff = 0;
            health--;
            if (health >= 1)
            {
                AS.PlayOneShot(damageSE);
            }
            if (health <= 0)
            {
                StartCoroutine(BugLoose());
            }
            text.text = needLeft ? "LEFT " + health.ToString() : "RIGHT " + health.ToString();
        }
        prevAngle = angle;
        prevAxB = AxB;
    }

    Vector3 WorldPos()
    {
        return RectTransformUtility.WorldToScreenPoint(Camera.main, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public override void Initialize(int health)
    {
        if (health == 0)
            health = startHealth;
        AS = GetComponent<AudioSource>();
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        image.sprite = normal;
        this.transform.localRotation = Quaternion.Euler(0, 0, 0);
        rotation = this.transform.rotation;

        this.health = health;
        needLeft = Random.Range(0, 2) == 1 ? true : false;
        text.text = needLeft ? "LEFT " + health.ToString() : "RIGHT " + health.ToString();
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
