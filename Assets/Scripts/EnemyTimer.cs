using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTimer : MonoBehaviour
{
    float bug1Time; //バグが出るまでの時間
    public Bug[] bugs;
    void Start()
    {
        
    }

    void Update()
    {
        //5秒後かつバグが出てなければバグを出す
        if(bug1Time >= 5)
        {
            int b = bugs.Length;
            int r = Random.Range(0, b);
            bugs[r].Initialize(5); 
            bug1Time = 0;
        }
        //バグが出てなかったら
        else
        {
            bug1Time += Time.deltaTime;
        }
    }
}
