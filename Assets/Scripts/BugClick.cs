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
        text.text = health.ToString();
        if (health <= 0)
        {
            StartCoroutine(BugLoose());
        }
    }
}
