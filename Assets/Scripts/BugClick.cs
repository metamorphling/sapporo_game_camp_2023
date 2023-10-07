using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BugClick : Bug
{
    public void OnClickBug1()
    {
        health--;
        text.text = health.ToString();
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
