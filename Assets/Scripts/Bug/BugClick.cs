using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BugClick : Bug
{
    public void OnClickBug1()
    {
        if (health <= 0)
            return;
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
