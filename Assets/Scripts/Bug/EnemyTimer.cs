using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTimer : MonoBehaviour
{
    float bug1Time; //バグが出るまでの時間
    public Bug[] bugs;
    private bool blockStart = true;

    void Start()
    {
        StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(10);
        blockStart = false;
    }

    void Update()
    {
        if (blockStart)
            return;

        foreach (var bug in bugs)
        {
            if (!bug.isDead)
            {
                bug1Time = 0;
                return;
            }
        }

        //5秒後かつバグが出てなければバグを出す
        if (bug1Time >= 5)
        {
            int b = bugs.Length;
            int r = Random.Range(0, b);
            if (r == 2)
            {
                bugs[r].Initialize(10);
                Debug.Log(r);
            }
            else
            {
                bugs[r].Initialize(5);
                Debug.Log(r);
            }
            bug1Time = 0;
        }
        //バグが出てなかったら
        else
        {
            bug1Time += Time.deltaTime;
        }
    }
}
