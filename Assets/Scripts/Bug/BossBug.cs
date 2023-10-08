using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBug : Bug
{
    public void OnClickBug5()
    {
        if(health <= 0) { return; }

        health--;
        if (health >= 1)
        {
            AS.PlayOneShot(damageSE);
        }
        text.text = health.ToString();
        if (health <= 0)
        {
            StartCoroutine(BugLoose());
        }
    }
}
