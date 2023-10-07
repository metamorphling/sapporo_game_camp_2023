using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighBugClick : Bug
{
    public void OnClickBug3()
    {
        if (health <= 0)
            return;
        health--;
        text.text = health.ToString();
        if (health <= 0)
        {
            StartCoroutine(BugLoose());
        }
    }
}
