using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTimer : MonoBehaviour
{
    float bug1Time; //�o�O���o��܂ł̎���
    public Bug[] bugs;
    void Start()
    {
        
    }

    void Update()
    {
        //5�b�ォ�o�O���o�ĂȂ���΃o�O���o��
        if(bug1Time >= 5)
        {
            int b = bugs.Length;
            int r = Random.Range(0, b);
            bugs[r].Initialize(5); 
            bug1Time = 0;
        }
        //�o�O���o�ĂȂ�������
        else
        {
            bug1Time += Time.deltaTime;
        }
    }
}
