using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HighBugTyphoon : Bug
{
    Vector2 pos;   //�}�E�X�N���b�N���̈ʒu
    Quaternion rotation;//�N���b�N�����Ƃ��̃^�[�Q�b�g�̊p�x

    Vector2 vecA;       //�^�[�Q�b�g���S�����pos�ւ̃x�N�g��
    Vector2 vecB;       //�^�[�Q�b�g�̒��S���猻�}�E�X�ʒu�ւ̃x�N�g��

    float angle;        //vecA��vecB�̂Ȃ��p�x
    float prevAngle;        //vecA��vecB�̂Ȃ��p�x
    float angleDiff;
    Vector3 AxB;        //vecA��vecB�̊O��
    Vector3 prevAxB;        //vecA��vecB�̊O��

    public RectTransform canvasRect;
    bool Drag;          //�h���b�O���̃t���O
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
            pos = WorldPos();//�}�E�X�ʒu�����[���h���W�Ŏ擾
        }
    }

    public void OnClickBag2()   //�}�E�X���N���b�N�����Ƃ��̏���
    {
        //�N���b�N���̃}�E�X�̏����ʒu�ƃ^�[�Q�b�g�̌��݂̊p�x���擾
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            rotation = this.transform.rotation;
            Drag = true;
        }
    }

    void Rotate()   //�h���b�O���̓���
    {
        //�h���b�O���������ꂽ��t���O��OFF
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Drag = false;
            return;
        }
        Vector3 trPos = this.transform.position;

        //�}�E�X�̏����ʒu�̃x�N�g�������߂�
        vecA = (pos - (Vector2)trPos).normalized;

        //���}�E�X�ʒu�̃x�N�g�������߂�
        vecB = (WorldPos() - trPos).normalized;

        //�}�E�X�̏����ʒu�ƌ��}�E�X�ʒu����p�x�ƊO�ς����߂�
        angle = Vector2.Angle(vecA, vecB);  //�p�x���v�Z
        AxB = Vector3.Cross(vecA, vecB);    //�O�ς��v�Z

        bool isLeft = ((AxB.z > 0) && (prevAngle < angle)) || ((AxB.z < 0) && (prevAngle > angle));
        if (isLeft != needLeft)
            return;

        //�O�ς�z�����̐����ŉ�]���������߂�
        if (AxB.z > 0)
        {
            //�����ʒu�Ƃ̊|���Z�ő��ΓI�ɉ�]������
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
